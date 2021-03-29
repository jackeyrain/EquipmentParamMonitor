using DPToleranceMonitorService.Model.DB;
using FRAEquipmentParamService.Access;
using FRAEquipmentParamService.Model;
using Jakware.UaClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace FRAEquipmentParamService.Implement
{
    public class StationLoadMonitor : IDisposable, IStation
    {
        public LoadStationEntity Entity { get; set; }

        private Jakware.UaClient.JakwareUaClient jakware;
        private ConcurrentDictionary<string, NodeEntity> DicParamSet;
        public StationLoadMonitor(LoadStationEntity loadStationEntity)
        {
            LogHelper.Log.LogInfo($"{loadStationEntity.Name} createad...", LogHelper.LogType.Information, false);
            Entity = loadStationEntity;
            this.jakware = new Jakware.UaClient.JakwareUaClient();
            DicParamSet = new ConcurrentDictionary<string, NodeEntity>();
        }

        public void Dispose()
        {
            jakware?.DisConnect();
        }

        public void Initialize()
        {
            DicParamSet.TryAdd(Entity.PalletID.TagAddress, Entity.PalletID);
            DicParamSet.TryAdd(Entity.WorkOrder.TagAddress, Entity.WorkOrder);
            DicParamSet.TryAdd(Entity.Ready.TagAddress, Entity.Ready);

            List<NodeId> tags = new List<NodeId>();
            foreach (var item in DicParamSet.Values)
            {
                tags.Add(NodeId.Parse($"{item.TagAddress}"));
            }
            LogHelper.Log.LogInfo($"{Entity.Name} collection data {this.DicParamSet.Count}.", LogHelper.LogType.Information, false);

            jakware.connStr = Entity.OPCConnectionStr;
            jakware.Initialize();
            jakware.Connect();
            LogHelper.Log.LogInfo($"{Entity.Name} connect to OPC.", LogHelper.LogType.Information, false);

            jakware.StartSubscription();
            jakware.AddMonitorNodeId(tags);
            jakware.JakwareDataChangedEventHandler += Jakware_JakwareDataChangedEventHandler;
            LogHelper.Log.LogInfo($"{Entity.Name} initialize completed.", LogHelper.LogType.Warn, false);
        }

        private async void Jakware_JakwareDataChangedEventHandler(UnifiedAutomation.UaClient.Subscription subscription, Jakware.UaClient.JakwareDataChangedEventArgs e)
        {
            await new TaskFactory().StartNew(() =>
            {
                foreach (var item in e.JakwareDataChanges)
                {
                    if (!item.IsGood) continue;

                    var tagAddress = item.MonitoredItem.NodeId.ToString();
                    this.DicParamSet.TryGetValue(tagAddress, out var node);
                    if (node == null)
                    {
                        LogHelper.Log.LogInfo($"{Entity.Name}-{tagAddress} wasn't registed.", LogHelper.LogType.Error, false);
                        continue;
                    }
                    node.Value = item.Value.Value;
                    node.CreateDT = DateTime.Now;
                    LogHelper.Log.LogInfo($"{Entity.Name}-{tagAddress}-{node.Value?.ToString()}", LogHelper.LogType.Information, false);

                    if (node.Flag.Equals("WORK_REQUIRE", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Convert.ToBoolean(node.Value))
                        {
                            var palletId = Entity.PalletID;
                            var workOrder = Entity.WorkOrder;

                            var workInfo = DBAccess.Instance.
                                Select<MES_TT_APS_WORK_ORDER>()
                                .Where(o => o.ORDER_CODE.Equals(Convert.ToString(workOrder.Value), StringComparison.OrdinalIgnoreCase) && o.VALID_FLAG.Value)
                                .First();
                            if (workInfo == null)
                            {
                                LogHelper.Log.LogInfo($"{Entity.Name}-Paller {palletId.Value.ToString()} without binding Workorder.", LogHelper.LogType.Exception, false);
                                continue;
                            }
                            if (palletId.Value != null && workOrder.Value != null)
                            {
                                MES_TP_FRA_Pallet pallet = null;
                                pallet = DBAccess.Instance.Select<MES_TP_FRA_Pallet>().Where(o => o.PalletID.Equals(palletId.Value.ToString())).First();
                                if (pallet == null)
                                    pallet = new MES_TP_FRA_Pallet
                                    {
                                        PalletID = Convert.ToString(palletId.Value),
                                    };

                                pallet.LineCode = ConfigurationManager.AppSettings["LineCode"]; // workInfo.ASSEMBLY_LINE;
                                pallet.WorkOrder = workInfo.ORDER_CODE;
                                pallet.Sequence = Convert.ToString(workInfo.ORDER_SEQ);
                                pallet.Vin = workInfo.VIN_CODE;
                                pallet.CreateDT = DateTime.Now;
                                pallet.CreateUser = AppDomain.CurrentDomain.FriendlyName;

                                DBAccess.Instance.InsertOrUpdate<MES_TP_FRA_Pallet>().SetSource(pallet).ExecuteAffrows();
                                LogHelper.Log.LogInfo($"{Entity.Name} Pallet {palletId.Value.ToString()} binding Work order {workInfo.ORDER_CODE}-{workInfo.ORDER_SEQ}-{workInfo.VIN_CODE}", LogHelper.LogType.Warn, false);
                            }
                        }
                        else
                        {

                        }
                    }
                }
            });
        }

        public string WriteValue(string nodeId, dynamic value)
        {
            var result = jakware.Write(new WriteDataValue { NodeId = NodeId.Parse(nodeId), Value = value });
            return result?.First().Error;
        }
    }
}
