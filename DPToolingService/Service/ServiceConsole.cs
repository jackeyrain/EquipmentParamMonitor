using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace DPToolingService
{
    public class ServiceConsole
    {
        private OrderQueue orderQueue = null;
        private Jakware.UaClient.JakwareUaClient jakware = null;
        private System.Threading.CancellationTokenSource cts;
        private readonly List<OrderTag> orderTags;

        public ServiceConsole(List<OrderTag> orderTags)
        {
            cts = new System.Threading.CancellationTokenSource();
            cts.Token.Register(() => LogHelper.Log.LogInfo("Service is stoping.", LogHelper.LogType.Warn));
            orderQueue = new OrderQueue(orderTags);
            jakware = new Jakware.UaClient.JakwareUaClient();
            jakware.connStr = ConfigurationManager.AppSettings["OPCServer"];
            jakware.Initialize();
            jakware.Connect();
            LogHelper.Log.LogInfo("OPC service connected.", LogHelper.LogType.Information);
            this.orderTags = orderTags;
        }

        public async void Start()
        {
            LogHelper.Log.LogInfo("Service is running.", LogHelper.LogType.Information);

            await new TaskFactory().StartNew(async () =>
             {
                 while (!cts.IsCancellationRequested)
                 {
                     await Task.Delay(int.Parse(ConfigurationManager.AppSettings["Interval"]));

                     orderQueue.GetWorkOrderBuffer();

                     var entity = orderQueue.GetOrderQueue();
                     if (entity != null)
                     {
                         var handShake = jakware.Read(NodeId.Parse(entity.OrderTag.HandShake)).FirstOrDefault();
                         if (handShake != null &&
                            Convert.ToInt16(handShake.Value) == 1)
                         {
                             var sendEntity = orderQueue.RemoveOrderQueue();
                             jakware.Write(
                                 new[] {
                                  NodeId.Parse(entity.OrderTag.TagAddress),
                                  NodeId.Parse(entity.OrderTag.HandShake),
                                 },
                                 new dynamic[]
                                 {
                                     (UInt16)sendEntity.Value,
                                     (Int16)0,
                                 });
                         }
                     }
                 }

                 if (cts.IsCancellationRequested)
                     return;
             });
        }

        internal void SendByManual(string param, string value)
        {
            var toolValue = 0;
            if (param.Equals("order", StringComparison.OrdinalIgnoreCase))
            {
                toolValue = orderQueue.CalcToolingMouldValue(value);
            }
            else if (param.Equals("tool", StringComparison.OrdinalIgnoreCase))
            {
                toolValue = Convert.ToInt32(value);
            }
            var result = jakware.Write(new[] {
                                NodeId.Parse(orderTags.First().TagAddress),
                                NodeId.Parse(orderTags.First().HandShake),
                                },
                                new dynamic[]
                                {
                                (UInt16)toolValue,
                                (Int16)0,
                                });
        }

        internal void Show()
        {
            var message = orderQueue.ToString();
            if (string.IsNullOrEmpty(orderQueue.ToString()))
                LogHelper.Log.LogInfo("Order's queue is emtpy.", LogHelper.LogType.Warn);
            else
                LogHelper.Log.LogInfo(message, LogHelper.LogType.Information);
        }

        public void Stop()
        {
            cts.Cancel();
            jakware.DisConnect();
        }
    }
}
