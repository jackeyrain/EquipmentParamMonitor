using EquipmentParamMonitor.ACCESS;
using EquipmentParamMonitor.Model;
using Jakware.UaClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace EquipmentParamMonitor.Service
{
    public class EquipmentEntity : IDisposable
    {
        public string EntityName { get; set; }
        private Jakware.UaClient.JakwareUaClient opcClient = new Jakware.UaClient.JakwareUaClient();
        private TT_PCS_CARRIER_CHECK_MANAGER PCS_CARRIER_CHECK_MANAGER = new TT_PCS_CARRIER_CHECK_MANAGER();
        private TT_APS_WORK_ORDER_MANAGER APS_WORK_ORDER_MANAGER = new TT_APS_WORK_ORDER_MANAGER();
        private EQUIPPARAMLOG_MANAGER EQUIPPARAMLOG_MANAGER = new EQUIPPARAMLOG_MANAGER();
        private List<EQUIPMENT_VARIABLE> equipParams { get; set; }
        private List<NodeId> nodeIdSet { get; set; }
        private NodeId CarrierID { get; set; }
        private List<EQUIPPARAMLOG> logSet { get; set; }
        private bool logEndable { set; get; }

        private CARRIERWORKORDER_INFO carrierInfo;

        private volatile bool EMPTYFLAGRUNNING = false;
        private string STATION110INSTATION = ConfigurationManager.AppSettings["STATION110INSTATION"];
        private string EMTPYCARRIERSIGNAL = ConfigurationManager.AppSettings["EMTPYCARRIERSIGNAL"];
        #region

        #endregion

        private string StartTag { get; set; }
        public EquipmentEntity(string name, List<Model.EQUIPMENT_VARIABLE> equipParams)
        {
            opcClient.connStr = ConfigurationManager.AppSettings["OPCSERVER"];
            nodeIdSet = new List<NodeId>();
            this.equipParams = equipParams;
            this.EntityName = name;
        }

        public void Initialize()
        {
            opcClient.Initialize();
            opcClient.Connect();
            opcClient.JakwareDataChangedEventHandler += OpcClient_JakwareDataChangedEventHandler;
            opcClient.StartSubscription();
            // ns=2;s=CKPT_IP_LINE.CKPT_IP_LINE.Global
            var nodeSet = this.equipParams.Select(o => NodeId.Parse($"ns=2;s={o.GROUP_NAME}.{o.CODE}.{o.PARAMNAME}"));
            CarrierID = nodeSet.FirstOrDefault(o => o.ToString().ToLower().Contains("tomes.carrierid"));
            opcClient.AddMonitorNodeId(nodeSet.ToArray());
            //nodeSet.ToList().ForEach(o =>
            //{
            //    LogHelper.Log.LogInfo($"{string.Join("-", this.equipParams)} - {o.ToString()}");
            //});
        }

        private void OpcClient_JakwareDataChangedEventHandler(UnifiedAutomation.UaClient.Subscription subscription, Jakware.UaClient.JakwareDataChangedEventArgs e)
        {
            DataChangeEvent(e);
        }

        private async void DataChangeEvent(JakwareDataChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(STATION110INSTATION) && !string.IsNullOrEmpty(EMTPYCARRIERSIGNAL))
                {
                    var _110_instation = e.JakwareDataChanges.FirstOrDefault(o => o.MonitoredItem.NodeId.ToString().Equals(STATION110INSTATION, StringComparison.OrdinalIgnoreCase));
                    this.Execute1stStationVerify(_110_instation);
                }

                var carrier = e.JakwareDataChanges.FirstOrDefault(o =>
                            o.MonitoredItem.NodeId.ToString().Equals(CarrierID.ToString(), StringComparison.OrdinalIgnoreCase));
                if (carrier != null && carrier.IsGood)
                {
                    if (!carrier.Value.Value.ToString().Equals("0"))
                    {
                        this.carrierInfo = new CARRIERWORKORDER_INFO();

                        this.carrierInfo.carrierIDNumber = carrier.Value.Value.ToString();
                        int count = 691200;
                        // var workOrderNumber = PCS_CARRIER_CHECK_MANAGER.GetList(o => o.CARRIER_NO.Equals(this.carrierInfo.carrierIDNumber, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        TT_PCS_CARRIER_CHECK carrier_check_info = null;

                        await new TaskFactory().StartNew(() =>
                        {
                            while (carrier_check_info == null && count-- > 0)
                            {
                                carrier_check_info = PCS_CARRIER_CHECK_MANAGER.GetList(o => o.CARRIER_NO.Equals(this.carrierInfo.carrierIDNumber, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                LogHelper.Log.LogInfo($"{this.EntityName}:{this.carrierInfo.carrierIDNumber} is WAITTING.");

                                Thread.Sleep(1000);
                            }
                        });

                        if (carrier_check_info == null || carrier_check_info.IS_EMPTY)
                        {
                            LogHelper.Log.LogInfo($"{this.EntityName}:{this.carrierInfo.carrierIDNumber} is EMPTY.");
                            return;
                        }

                        var workOrderInfo = APS_WORK_ORDER_MANAGER.GetList(o => o.ORDER_CODE.Equals(carrier_check_info.WORK_ORDER, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (workOrderInfo == null)
                        {
                            return;
                        }
                        this.logEndable = true;
                        this.logSet = new List<EQUIPPARAMLOG>();
                        this.carrierInfo.workOrder = workOrderInfo.ORDER_CODE;
                        this.carrierInfo.workOrderSeq = workOrderInfo.ORDER_SEQ.ToString();
                        this.carrierInfo.workOrderVin = workOrderInfo.VIN_CODE;
                        this.carrierInfo.station = GetLocation(carrier.MonitoredItem.NodeId.ToString());

                        LogHelper.Log.LogInfo($"{this.EntityName}:{this.carrierInfo.carrierIDNumber} is start.");
                    }
                    else if (this.logEndable)
                    {
                        if (this.logSet != null && this.logSet.Count > 0)
                        {
                            var count = EQUIPPARAMLOG_MANAGER.AddBluk(this.logSet);
                            LogHelper.Log.LogInfo($"{EntityName}:{this.carrierInfo.carrierIDNumber} is finish. Count is {count}");
                        }
                        this.logEndable = false;
                        this.carrierInfo = new CARRIERWORKORDER_INFO();
                    }
                }

                if (!this.logEndable)
                    return;

                this.logSet.AddRange(e.JakwareDataChanges.Where(o => o.IsGood).Select(o => new EQUIPPARAMLOG
                {
                    CARRIERID = this.carrierInfo.carrierIDNumber,
                    WORKORDER = this.carrierInfo.workOrder,
                    SEQUENCE = this.carrierInfo.workOrderSeq,
                    VINCODE = this.carrierInfo.workOrderVin,
                    PARAMTAG = o.MonitoredItem.NodeId.ToString(),
                    STATION = this.carrierInfo.station,
                    VALUE = o.Value.Value.ToString(),
                    CREATEDATETIME = DateTime.Now,
                }));
            }
            catch (Exception ex)
            {
                LogHelper.Log.LogInfo(ex, LogHelper.LogType.Exception);
            }
        }

        public static string GetLocation(string context)
        {
            try
            {
                int a_index = context.IndexOf("Station");
                int b_index = context.IndexOf(".", a_index);
                var station = context.Substring(a_index, b_index - a_index);
                return station;
            }
            catch
            {
                return string.Empty;
            }
        }

        private async void Execute1stStationVerify(JakwareDataChange jakwareDataChange)
        {
            await new TaskFactory().StartNew(() =>
            {
                try
                {
                    if (EMPTYFLAGRUNNING) return;
                    EMPTYFLAGRUNNING = true;
                    if (jakwareDataChange == null) return;

                    if (Convert.ToBoolean(jakwareDataChange.Value.Value))
                        if (!WOExist())
                        {
                            opcClient.Write(new WriteDataValue { NodeId = NodeId.Parse(EMTPYCARRIERSIGNAL), Value = true });
                        }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.LogInfo(ex, LogHelper.LogType.Exception);
                }
                finally
                {
                    EMPTYFLAGRUNNING = false;
                }
            });
        }

        public bool WOExist()
        {
            string sql = $@"SELECT top 1 1 from MES.TT_APS_WORK_ORDER a with(NOLOCK)
                              INNER join MES.TT_APS_WORK_ORDER_ASSEMBLY b with(NOLOCK) on a.ID = b.ORDER_ID
                              where(b.LOCATION = 'CP110' and b.STATUS = 0)";

            var conn = APS_WORK_ORDER_MANAGER.Db.Ado;
            var command = conn.GetCommand(sql, null);
            try
            {
                conn.Open();
                var result = command.ExecuteScalar();
                if (result == null || Convert.ToInt32(result) <= 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
            return true;
        }

        public void Dispose()
        {
            opcClient.StopSubscription();
            opcClient.DisConnect();
            opcClient.Dispose();
        }
    }
}
