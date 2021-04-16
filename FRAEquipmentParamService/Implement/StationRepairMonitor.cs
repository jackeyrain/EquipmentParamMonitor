using DPToleranceMonitorService.Model.DB;
using FRAEquipmentParamService.Access;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UnifiedAutomation.UaBase;

namespace FRAEquipmentParamService.Implement
{
    public class WorkOrderInfo
    {
        public string WorkOrder { get; set; }
        public string Sequence { get; set; }
        public string Vin { get; set; }
    }

    public class StationRepairMonitor : IDisposable
    {
        private string repairStation = string.Empty;
        private Jakware.UaClient.JakwareUaClient jakware = null;
        private List<MES_FRAEQUIPPARAMLOG> traceDataSet = new List<MES_FRAEQUIPPARAMLOG>();
        private WorkOrderInfo workOrderInfo = null;
        private volatile bool recordEnable = false;
        private string completeSignal = string.Empty;
        public StationRepairMonitor()
        {
            this.repairStation = ConfigurationManager.AppSettings["RepairStation"];
            LogHelper.Log.LogInfo($"Get repair staton is {this.repairStation}.");
            var repairStation = DBAccess.Instance.Select<MES_TM_BAS_EQUIPMENT>()
                .Where(o => o.NAME.Equals(this.repairStation))
               .IncludeMany(o => o.mES_TM_BAS_EQUIPMENT_VARIABLEs.Where(p => p.EQUIP_FID == o.FID))
               .First();
            if (repairStation == null) throw new Exception("Doesn't maintain repair station.");

            LogHelper.Log.LogInfo($"Tags' quantity of repair station is {repairStation.mES_TM_BAS_EQUIPMENT_VARIABLEs.Count}.");

            var nodeSet = repairStation.mES_TM_BAS_EQUIPMENT_VARIABLEs.Select(o => NodeId.Parse($"ns=2;s={repairStation.GROUP_NAME}.{repairStation.CODE}.{o.NAME}")).ToList();
            completeSignal = nodeSet.Where(o => o.ToString().ToLower().Contains("cycendupdatecompleteack")).First().ToString();

            jakware = new Jakware.UaClient.JakwareUaClient();
            jakware.JakwareDataChangedEventHandler += Jakware_JakwareDataChangedEventHandler;
            jakware.connStr = $"opc.tcp://{repairStation.IP}:49320";
            jakware.Initialize();
            jakware.Connect();
            jakware.StartSubscription();
            jakware.AddMonitorNodeId(nodeSet);

            LogHelper.Log.LogInfo($"Repair station initialize completed.");
        }

        public bool StartRecord(WorkOrderInfo workOrderInfo)
        {
            this.workOrderInfo = workOrderInfo;
            recordEnable = true;
            return recordEnable;
        }

        public bool StopRecord()
        {
            recordEnable = false;
            return recordEnable;
        }

        private void Jakware_JakwareDataChangedEventHandler(UnifiedAutomation.UaClient.Subscription subscription, Jakware.UaClient.JakwareDataChangedEventArgs e)
        {
            if (recordEnable == true)
            {
                foreach (var item in e.JakwareDataChanges)
                {
                    if (item.MonitoredItem.NodeId.ToString().Equals(this.completeSignal, StringComparison.OrdinalIgnoreCase))
                    {
                        CompleteImplement();
                        break;
                    }

                    if (item.IsGood)
                    {
                        this.traceDataSet.Add(new MES_FRAEQUIPPARAMLOG
                        {
                            CARRIERID = string.Empty,
                            WORKORDER = this.workOrderInfo.WorkOrder,
                            SEQUENCE = this.workOrderInfo.Sequence,
                            VINCODE = this.workOrderInfo.Vin,
                            STATION = repairStation,
                            PARAMTAG = item.MonitoredItem.NodeId.ToString(),
                            VALUE = item.Value.Value.ToString(),
                            CREATEDATETIME = DateTime.Now,
                        });
                    }
                }
            }
        }

        private void CompleteImplement()
        {
            if (this.recordEnable &&
                this.workOrderInfo != null &&
                this.traceDataSet.Count > 0)
            {
                var result = DBAccess.Instance.Insert<MES_FRAEQUIPPARAMLOG>().AppendData(this.traceDataSet).ExecuteAffrows();
                this.recordEnable = false;
                this.traceDataSet.Clear();
                this.workOrderInfo = null;
            }
        }

        public void Dispose()
        {
            this.recordEnable = false;
            this.traceDataSet.Clear();
            this.workOrderInfo = null;
            this.jakware.DisConnect();
        }
    }
}
