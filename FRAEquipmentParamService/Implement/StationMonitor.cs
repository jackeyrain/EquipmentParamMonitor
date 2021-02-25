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
    public class StationMonitor : IDisposable, IStation
    {
        public StationEntity Entity { get; set; }

        private ConcurrentDictionary<string, NodeEntity> DicParamSet;
        private Jakware.UaClient.JakwareUaClient jakware;

        public StationMonitor(StationEntity entity)
        {
            this.Entity = entity;
            this.DicParamSet = new ConcurrentDictionary<string, NodeEntity>();
            this.jakware = new Jakware.UaClient.JakwareUaClient();
        }
        public void Dispose()
        {
            this.jakware?.DisConnect();
            this.DicParamSet.Clear();
            GC.Collect();
        }

        public void Initialize()
        {
            this.DicParamSet.TryAdd(Entity.PalletNode.TagAddress, Entity.PalletNode);
            this.DicParamSet.TryAdd(Entity.RunningNode.TagAddress, Entity.RunningNode);
            foreach (var param in Entity.ParamSet)
            {
                foreach (var p in param.TagAddress)
                {
                    this.DicParamSet.TryAdd(p.TagAddress, p);
                }
            }
            this.jakware.connStr = this.Entity.OPCConnectionStr;
            this.jakware.Initialize();
            this.jakware.Connect();

            this.jakware.StartSubscription();
            var tags = this.DicParamSet.Keys.ToArray();
            var nodes = tags.Select(o => NodeId.Parse($"{o}")).ToArray();
            this.jakware.AddMonitorNodeId(nodes);
            this.jakware.JakwareDataChangedEventHandler += Jakware_JakwareDataChangedEventHandler;
        }

        public Task<int> SaveIntoDB()
        {
            return new TaskFactory().StartNew(() =>
            {
                var data = Array.ConvertAll(this.DicParamSet.Values.ToArray(),
                o => new MES_FRAEQUIPPARAMLOG()
                {
                    CARRIERID = Entity.MES_TP_FRA_Pallet.PalletID,
                    WORKORDER = Entity.MES_TP_FRA_Pallet.WorkOrder,
                    SEQUENCE = Entity.MES_TP_FRA_Pallet.Sequence,
                    VINCODE = Entity.MES_TP_FRA_Pallet.Vin,
                    STATION = Entity.Name,
                    PARAMTAG = o.TagAddress,
                    VALUE = o.Value == null ? string.Empty : o.Value.ToString(),
                    CREATEDATETIME = o.CreateDT,
                }
                ).ToList();
                var result = DBAccess.Instance.Insert<MES_FRAEQUIPPARAMLOG>().AppendData(data.OrderBy(o => o.PARAMTAG)).ExecuteAffrows();
                return result;
            });
        }

        public Task<int> SaveIntoToBeRepaired()
        {
            return new TaskFactory().StartNew(() =>
            {
                var toBeRepaired = Entity.GetFailResult();
                if (toBeRepaired.Count <= 0) return 0;
                var data = Array.ConvertAll(toBeRepaired.ToArray(), o => new MES_TR_CIM_TOBE_REPAIRED
                {
                    FID = Guid.NewGuid(),
                    ORDER_CODE = Entity.MES_TP_FRA_Pallet.WorkOrder,
                    ASSEMBLY_LINE = Entity.LineCode,
                    LOCATION = Entity.Name,
                    DESCRIPTION = $"{o.Name} Fail",
                    RESULT = "NOK",
                    INSPECTED = false,
                    REMARK = $"{o.Name}",
                    ADD_BY_INSPECTOR = true,
                    VALID_FLAG = true,
                    CREATE_USER = AppDomain.CurrentDomain.FriendlyName,
                    CREATE_DATE = DateTime.Now,
                    DATA_SOURCE = 1,
                    REPAIRE_TYPE = 2,
                }
                ).ToList();
                var result = DBAccess.Instance.Insert<MES_TR_CIM_TOBE_REPAIRED>().AppendData(data).ExecuteAffrows();
                return result;
            });
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

                if (node.Flag.Equals("result", StringComparison.OrdinalIgnoreCase))
                {
                    if (Convert.ToBoolean(node.Value))
                    {
                        Entity.GetPallet();
                    }
                    else
                    {
                        if (Entity.MES_TP_FRA_Pallet == null) continue;

                        var t1 = SaveIntoDB();
                        var t2 = SaveIntoToBeRepaired();

                        Task.WaitAll(t1, t2);
                        Console.WriteLine(t1.Result);
                        Console.WriteLine(t2.Result);

                        Entity.Initialize();
                        this.Reinitialize();
                    }
                }
            }
        }

        private void Reinitialize()
        {
            foreach (var item in this.DicParamSet.Values)
            {
                item.Value = null;
            }
        }
    }
}
