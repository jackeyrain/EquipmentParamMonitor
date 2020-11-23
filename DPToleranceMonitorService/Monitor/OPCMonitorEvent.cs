using DPToleranceMonitorService.Access;
using DPToleranceMonitorService.Model;
using Jakware.UaClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace DPToleranceMonitorService.Monitor
{
    public partial class OPCMonitor
    {
        public async void Command(JakwareDataChange jakwareData)
        {
            var nodeId = jakwareData.MonitoredItem.NodeId.ToString();
            Int32.TryParse(jakwareData.Value.Value.ToString(), out var value);

            if (nodeId.Equals(this.ToleranceInstance.Require.TagAddress))
            {
                if (value == this.ToleranceInstance.Require.Value)
                {
                    await RequireEvent();
                }
            }
            else if (nodeId.Equals(this.ToleranceInstance.Ack.TagAddress))
            {
                if (value == this.ToleranceInstance.Ack.Value)
                {
                    await AckEvent();
                }
            }
        }

        private Task RequireEvent()
        {
            return new TaskFactory().StartNew(async () =>
            {
                List<TT_WSDP_EQUIPMENT_LOG> data = new List<TT_WSDP_EQUIPMENT_LOG>();
                foreach (var d in this.ToleranceInstance.Data)
                {
                    data.AddRange(Array.ConvertAll(d.Tolerances, o => new TT_WSDP_EQUIPMENT_LOG
                    {
                        Area = d.Area,
                        Name = o.Name,
                        TagAddress = o.TagAddress,
                        Value = Convert.ToString(o.Value),
                        Accurate = o.Accurate,
                        CREATE_USER = "WSDP_SERVICE",
                    }));
                }
                var result = new DBAccess().InsertDuckData(data);
                Console.WriteLine($"{result} data is stored into database.");
                // 接收到请求信号后，判断值，并发送结果
                if (this.ToleranceInstance.CheckTolerance())
                {
                    SendResult(1);
                }
                else
                {
                    SendResult(2);
                }
                await Task.Delay(3000);
                await AckEvent();
            });
        }

        private Task AckEvent()
        {
            return new TaskFactory().StartNew(() =>
           {
               SendResult(0);
               SendAck(0);
           });
        }

        private void SendResult(int value)
        {
            var result = jakwareUaClient.Write(new[] { new WriteDataValue
            {
                 NodeId = NodeId.Parse(this.ToleranceInstance.Result.TagAddress),
                 Value = (UInt16)value,
            }});
        }

        private void SendAck(int value)
        {
            var result = jakwareUaClient.Write(new[] { new  WriteDataValue
            {
                NodeId = NodeId.Parse(this.ToleranceInstance.Ack.TagAddress),
                Value = value,
            }});
        }
    }
}
