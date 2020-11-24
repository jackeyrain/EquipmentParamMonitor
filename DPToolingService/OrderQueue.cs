using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace DPToolingService
{
    public class OrderQueue
    {
        private System.Threading.CancellationTokenSource cts = null;
        private readonly List<OrderTag> orderTags;

        public ConcurrentQueue<OrderEntity> OrderSet { get; set; }
        public OrderQueue(List<OrderTag> orderTags)
        {
            cts = new System.Threading.CancellationTokenSource();
            cts.Token.Register(() =>
            {
                LogHelper.Log.LogInfo("Tooling is stop.", LogHelper.LogType.Information);
            });
            this.orderTags = orderTags;
        }
        public void StartWork()
        {
            new TaskFactory().StartNew(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    GetWorkOrderBuffer();
                    if (cts.IsCancellationRequested)
                        return;
                    await Task.Delay(int.Parse(ConfigurationManager.AppSettings["Interval"]));
                }
            });
        }
        public OrderEntity GetOrderQueue()
        {
            if (OrderSet.IsEmpty)
                return null;

            if (OrderSet.TryPeek(out var entity))
            {
                return entity;
            }

            return null;
        }
        public OrderEntity RemoveOrderQueue()
        {
            OrderSet.TryDequeue(out var entity);
            return entity;
        }

        public void StopWork()
        {
            cts?.Cancel();
        }
        public void GetWorkOrderBuffer()
        {
            List<MES_TT_APS_WORK_ORDER> orders = null;
            var groupIdSet = Array.ConvertAll(orderTags.ToArray(), p => p.ProductGroupId.ToString());
            var lastGroupId = orderTags.OrderByDescending(o => o.ID).First().ProductGroupId;
            var orderCondition = $"CASE a.PRODUCT_GROUP_ID WHEN {lastGroupId} THEN a.PRODUCT_GROUP_ID * 100 ELSE a.PRODUCT_GROUP_ID END";
            string sqlCmd = string.Empty;
            if (orderTags.Any(o => string.IsNullOrEmpty(o.MonitorStation)))
                sqlCmd = $@"SELECT top 1 a.* FROM MES.TT_APS_WORK_ORDER a WITH(NOLOCK)
                              inner join MES.TT_APS_WORK_ORDER_ASSEMBLY b on a.ID = b.ORDER_ID
                              where a.PRODUCT_GROUP_ID in ({string.Join(",", groupIdSet.ToArray())}) and a.STATUS <> 10
                              order by a.ORDER_SEQ, {orderCondition} ASC";
            else
                sqlCmd = "";

            orders = DBHelper.DB.Select<MES_TT_APS_WORK_ORDER>()
           .WithSql(sqlCmd)
           .ToList();
        }
    }
}
