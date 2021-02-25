using DPToleranceMonitorService.Model.DB;
using FRAEquipmentParamService.Access;
using FRAEquipmentParamService.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace FRAEquipmentParamService.Implement
{
    public class StationLoadMonitor : IDisposable, IStation
    {
        private Jakware.UaClient.JakwareUaClient jakware;
        private ConcurrentDictionary<string, NodeEntity> DicParamSet;
        public StationLoadMonitor(LoadStationEntity loadStationEntity)
        {
            LoadStationEntity = loadStationEntity;
            this.jakware = new Jakware.UaClient.JakwareUaClient();
            DicParamSet = new ConcurrentDictionary<string, NodeEntity>();
        }

        public LoadStationEntity LoadStationEntity { get; }

        public void Dispose()
        {
            jakware?.DisConnect();
        }

        public void Initialize()
        {
            DicParamSet.TryAdd(LoadStationEntity.PalletID.TagAddress, LoadStationEntity.PalletID);
            DicParamSet.TryAdd(LoadStationEntity.WorkOrder.TagAddress, LoadStationEntity.WorkOrder);
            DicParamSet.TryAdd(LoadStationEntity.Ready.TagAddress, LoadStationEntity.Ready);

            List<NodeId> tags = new List<NodeId>();
            foreach (var item in DicParamSet.Values)
            {
                tags.Add(NodeId.Parse($"{item.TagAddress}"));
            }

            jakware.connStr = LoadStationEntity.OPCConnectionStr;
            jakware.Initialize();
            jakware.Connect();
            jakware.StartSubscription();
            jakware.AddMonitorNodeId(tags);
            jakware.JakwareDataChangedEventHandler += Jakware_JakwareDataChangedEventHandler;
        }

        private void Jakware_JakwareDataChangedEventHandler(UnifiedAutomation.UaClient.Subscription subscription, Jakware.UaClient.JakwareDataChangedEventArgs e)
        {
            foreach (var item in e.JakwareDataChanges)
            {
                if (!item.IsGood) continue;

                var tagAddress = item.MonitoredItem.NodeId.ToString();
                this.DicParamSet.TryGetValue(tagAddress, out var node);
                if (node == null) continue;
                node.Value = item.Value.Value;
                node.CreateDT = DateTime.Now;

                if (node.Flag.Equals("WORK_REQUIRE", StringComparison.OrdinalIgnoreCase))
                {
                    if (Convert.ToBoolean(node.Value))
                    {
                        var palletId = LoadStationEntity.PalletID;
                        var workOrder = LoadStationEntity.WorkOrder;

                        var workInfo = DBAccess.Instance.
                            Select<MES_TT_APS_WORK_ORDER>()
                            .Where(o => o.ORDER_CODE.Equals(Convert.ToString(workOrder.Value), StringComparison.OrdinalIgnoreCase) && o.VALID_FLAG.Value)
                            .First();

                        if (palletId.Value != null && workOrder.Value != null)
                        {
                            MES_TP_FRA_Pallet pallet = new MES_TP_FRA_Pallet
                            {
                                PalletID = Convert.ToString(palletId.Value),
                                LineCode = workInfo.ASSEMBLY_LINE,
                                WorkOrder = workInfo.ORDER_CODE,
                                Sequence = Convert.ToString(workInfo.ORDER_SEQ),
                                Vin = workInfo.VIN_CODE,
                                CreateDT = DateTime.Now,
                                CreateUser = AppDomain.CurrentDomain.FriendlyName,
                            };

                            DBAccess.Instance.InsertOrUpdate<MES_TP_FRA_Pallet>().SetSource(pallet).ExecuteAffrows();
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
