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
        public ServiceConsole(List<OrderTag> orderTags)
        {
            cts = new System.Threading.CancellationTokenSource();
            orderQueue = new OrderQueue(orderTags);
            jakware = new Jakware.UaClient.JakwareUaClient();
            jakware.connStr = ConfigurationManager.AppSettings["OPCServer"];
            jakware.Initialize();
            jakware.Connect();

            orderQueue.StartWork();
        }

        public async void Start()
        {
            await new TaskFactory().StartNew(async () =>
             {
                 while (!cts.IsCancellationRequested)
                 {
                     await Task.Delay(int.Parse(ConfigurationManager.AppSettings["Interval"]));

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
                                     (uint)sendEntity.Value,
                                     (uint)0,
                                 });
                         }
                     }
                 }

                 if (cts.IsCancellationRequested)
                     return;
             });
        }

        public void Stop()
        {
            cts.Cancel();
            jakware.DisConnect();
            orderQueue.StopWork();
        }
    }
}
