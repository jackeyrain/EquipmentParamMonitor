﻿using EquipmentParamMonitor.ACCESS;
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
        private FCEQUIPPARAMLOG_MANAGER EQUIPPARAMLOG_MANAGER = new FCEQUIPPARAMLOG_MANAGER();
        private List<EQUIPMENT_VARIABLE> equipParams { get; set; }
        private List<NodeId> nodeIdSet { get; set; }
        private NodeId CarrierID { get; set; }
        private NodeId Complete { get; set; }
        private List<FCEQUIPPARAMLOG> logSet { get; set; }
        private bool logEndable { set; get; }

        private CARRIERWORKORDER_INFO carrierInfo;
        // 缓存变量
        private Dictionary<string, long> cacheParam = new Dictionary<string, long>();
        public EquipmentEntity(string name, List<Model.EQUIPMENT_VARIABLE> equipParams)
        {
            opcClient.connStr = ConfigurationManager.AppSettings["OPCSERVER"];
            nodeIdSet = new List<NodeId>();
            this.equipParams = equipParams;
            Array.ForEach(this.equipParams.ToArray(), o =>
            {
                cacheParam[$"ns=2;s={o.GROUP_NAME}.{o.CODE}.{o.PARAMNAME}"] = o.PARAMID;
            });
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
            CarrierID = nodeSet.FirstOrDefault(o => o.ToString().ToLower().Contains(".palletid"));
            Complete = nodeSet.FirstOrDefault(o => o.ToString().ToLower().Contains(".cyclstat"));
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
                var carrier = e.JakwareDataChanges.FirstOrDefault(o =>
                            o.MonitoredItem.NodeId.ToString().Equals(CarrierID.ToString(), StringComparison.OrdinalIgnoreCase));
                var complete = e.JakwareDataChanges.FirstOrDefault(o =>
                            o.MonitoredItem.NodeId.ToString().Equals(Complete.ToString(), StringComparison.OrdinalIgnoreCase));

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
                        this.logSet = new List<FCEQUIPPARAMLOG>();
                        this.carrierInfo.workOrder = workOrderInfo.ORDER_CODE;
                        this.carrierInfo.workOrderSeq = workOrderInfo.ORDER_SEQ.ToString();
                        this.carrierInfo.workOrderVin = workOrderInfo.VIN_CODE;
                        this.carrierInfo.station = GetLocation(carrier.MonitoredItem.NodeId.ToString());

                        LogHelper.Log.LogInfo($"{this.EntityName}:{this.carrierInfo.carrierIDNumber} is start.");
                    }
                    //else if (this.logEndable)
                    //{
                    //    if (this.logSet != null && this.logSet.Count > 0)
                    //    {
                    //        this.logSet.ForEach(o => Console.WriteLine(o));

                    //        var count = EQUIPPARAMLOG_MANAGER.AddBluk(this.logSet);
                    //        LogHelper.Log.LogInfo($"{EntityName}:{this.carrierInfo.carrierIDNumber} is finish. Count is {count}");
                    //    }
                    //    this.logEndable = false;
                    //    this.carrierInfo = new CARRIERWORKORDER_INFO();
                    //}
                }

                if (complete != null && complete.IsGood && this.logEndable)
                {
                    if (!complete.Value.Value.ToString().Equals("0"))
                    {
                        if (this.logSet != null && this.logSet.Count > 0)
                        {
                            this.logSet.ForEach(o => Console.WriteLine(o));

                            var count = EQUIPPARAMLOG_MANAGER.AddBluk(this.logSet);
                            LogHelper.Log.LogInfo($"{EntityName}:{this.carrierInfo.carrierIDNumber} is finish. Count is {count}");
                        }
                        this.logEndable = false;
                        this.carrierInfo = new CARRIERWORKORDER_INFO();
                    }
                }

                if (!this.logEndable)
                    return;

                this.logSet.AddRange(e.JakwareDataChanges.Where(o => o.IsGood).Select(o => new FCEQUIPPARAMLOG
                {
                    CARRIERID = this.carrierInfo.carrierIDNumber,
                    WORKORDER = this.carrierInfo.workOrder,
                    SEQUENCE = this.carrierInfo.workOrderSeq,
                    VINCODE = this.carrierInfo.workOrderVin,
                    PARAMTAG = o.MonitoredItem.NodeId.ToString(),
                    PARAMID = this.GetParamID(o.MonitoredItem.NodeId.ToString()),
                    STATION = this.carrierInfo.station,
                    VALUE = o.Value.Value != null ? o.Value.Value.ToString() : string.Empty,
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
                int a_index = context.IndexOf("WSFC_CN");
                int b_index = context.IndexOf(".", a_index);
                var station = context.Substring(a_index, b_index - a_index);
                return station;
            }
            catch
            {
                return string.Empty;
            }
        }
        public long GetParamID(string key)
        {
            if (this.cacheParam.ContainsKey(key))
            {
                return this.cacheParam[key];
            }
            return 0;
        }
        public void Dispose()
        {
            opcClient.StopSubscription();
            opcClient.DisConnect();
            opcClient.Dispose();
        }
    }
}
