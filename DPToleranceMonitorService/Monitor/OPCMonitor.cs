using DPToleranceMonitorService.Enum;
using DPToleranceMonitorService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnifiedAutomation.UaBase;

namespace DPToleranceMonitorService.Monitor
{
    public partial class OPCMonitor
    {
        private string URI = string.Empty;
        private Jakware.UaClient.JakwareUaClient jakwareUaClient = null;
        private CancellationTokenSource cts;
        public MonitorStatus MonitorStatus { get; set; }
        public ToleranceInstance ToleranceInstance;
        private List<ToleranceEntity> _toleranceEntities;
        public OPCMonitor()
        {
            _toleranceEntities = new List<ToleranceEntity>();
        }
        public OPCMonitor(ToleranceInstance toleranceInstance)
            : this()
        {
            this.ToleranceInstance = toleranceInstance;

            Array.ForEach(ToleranceInstance.Data, o =>
            {
                _toleranceEntities.AddRange(o.Tolerances);
            });

            jakwareUaClient = new Jakware.UaClient.JakwareUaClient(ToleranceInstance.OPCServer);
            jakwareUaClient.Initialize();
            jakwareUaClient.JakwareDataChangedEventHandler += JakwareUaClient_JakwareDataChangedEventHandler;
        }

        private void JakwareUaClient_JakwareDataChangedEventHandler(
            UnifiedAutomation.UaClient.Subscription subscription,
            Jakware.UaClient.JakwareDataChangedEventArgs e)
        {
            foreach (var item in e.JakwareDataChanges)
            {
                // 写入队列组
                var toleranceSet = _toleranceEntities.Where(o => o.TagAddress.Equals(item.MonitoredItem.NodeId.ToString())).ToList();
                // 在参数列表中找到了，直接赋值
                if (toleranceSet.Count() > 0)
                    toleranceSet.ForEach(o => { o.Value = item.Value; });
                // 如果没找到，执行接收命令
                else
                    this.Command(item);
            }
        }

        public void Initialize()
        {
            cts = new CancellationTokenSource();
            cts.Token.Register(() => jakwareUaClient?.DisConnect());

            jakwareUaClient.Connect();
            jakwareUaClient.StartSubscription();

            var nodeIds = Array.ConvertAll(_toleranceEntities.ToArray(), o => NodeId.Parse(o.TagAddress));
            jakwareUaClient.AddMonitorNodeId(nodeIds);
        }
        public void Stop()
        {
            cts.Cancel();
        }
        public void Show(string area = "")
        {
            ToleranceInstance.Show(area);
        }

        internal void AreaList()
        {
            Console.WriteLine(string.Join(",", Array.ConvertAll(ToleranceInstance.Data, o => o.Area)));
        }
    }
}
