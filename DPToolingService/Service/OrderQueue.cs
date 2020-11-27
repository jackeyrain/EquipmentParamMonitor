using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPToolingService
{
    public class OrderQueue
    {
        private readonly List<OrderTag> orderTags;

        public ConcurrentQueue<OrderEntity> OrderSet { get; set; }

        public OrderQueue(List<OrderTag> orderTags)
        {
            this.orderTags = orderTags;
            OrderSet = new ConcurrentQueue<OrderEntity>();
        }
        public override string ToString()
        {
            StringBuilder message = new StringBuilder();

            foreach (var entity in OrderSet)
            {
                var group = OrderTagHelper.GroupSet.FirstOrDefault(o => o.ID == entity.OrderTag.ID);
                message.AppendLine($"{group?.GROUP_CODE}-{group?.GROUP_NAME}{"",3}{entity.MES_TT_APS_WORK_ORDER.ORDER_CODE}{"",3}Tooling parameter value is {entity.Value}");
            }

            return message.ToString();
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
        public void GetWorkOrderBuffer()
        {
            var groupIdSet = Array.ConvertAll(orderTags.ToArray(), p => p.ProductGroupId.ToString());
            var lastGroupId = orderTags.OrderByDescending(o => o.ID).First().ProductGroupId;
            var orderCondition = $"CASE a.PRODUCT_GROUP_ID WHEN {lastGroupId} THEN a.PRODUCT_GROUP_ID * 100 ELSE a.PRODUCT_GROUP_ID END";
            string sqlCmd = string.Empty;
            if (orderTags.Any(o => string.IsNullOrEmpty(o.MonitorStation)))
                sqlCmd = $@"SELECT a.* FROM MES.TT_APS_WORK_ORDER a WITH(NOLOCK)
                              where a.MOULD_VALUE is null and a.VALID_FLAG = 1 and a.PRODUCT_GROUP_ID in ({string.Join(",", groupIdSet.ToArray())}) and a.STATUS <> 10
                              order by a.ORDER_SEQ, {orderCondition} ASC";
            else
                sqlCmd = $@"SELECT a.* From MES.TT_APS_WORK_ORDER a WITH(NOLOCK)
                              inner join MES.TT_PC_PRODUCTION_LOG b WITH(NOLOCK) on a.ORDER_CODE = b.ORDER_CODE
                              where a.MOULD_VALUE is null and a.VALID_FLAG = 1 and a.PRODUCT_GROUP_ID in ({string.Join(",", groupIdSet.ToArray())}) and b.LOCATION in ('{string.Join("','", OrderTagHelper.MonitorStationSet.ToArray())}') 
                              ORDER by b.ID ASC";

            var orders = DBHelper.DB.Select<MES_TT_APS_WORK_ORDER>()
              .WithSql(sqlCmd)
              .ToList();

            if (orders.Count() <= 0)
                return;

            foreach (var order in orders)
            {
                LogHelper.Log.LogInfo($"Get work order {order.ORDER_CODE}", LogHelper.LogType.Information);
                var value = UpdateWorkOrderMouldValue(order, true);
                if (value > 0)
                {
                    var tagEntity = orderTags.FirstOrDefault(o => o.ID == order.PRODUCT_GROUP_ID);
                    OrderSet.Enqueue(new OrderEntity { Value = value, MES_TT_APS_WORK_ORDER = order, OrderTag = tagEntity });
                    LogHelper.Log.LogInfo($"Enqueue {order.ORDER_CODE} success.", LogHelper.LogType.Information);
                }
            }
        }

        public int UpdateWorkOrderMouldValue(MES_TT_APS_WORK_ORDER order, bool updateOrder)
        {
            var toolValue = CalcToolingMouldValue(order.ORDER_CODE).ToString();
            order.MOULD_VALUE = toolValue;
            if (updateOrder)
            {
                var result = DBHelper.DB.Update<MES_TT_APS_WORK_ORDER>().SetSource(order).ExecuteAffrows();
                if (result > 0)
                    return Convert.ToInt32(order.MOULD_VALUE);
                else
                    return 0;
            }
            else
            {
                return Convert.ToInt32(toolValue);
            }
        }

        public int CalcToolingMouldValue(string orderCode)
        {
            var sqlCmd = $@"SELECT sum(isnull(cast(d.SEND_VALUE as INT), 0)) 'TOOLINGVALUE' From MES.TT_APS_WORK_ORDER a with(NOLOCK)
                              inner join mes.TT_APS_WORK_ORDER_ASSEMBLY b with(NOLOCK) on a.ID = b.ORDER_ID  
                              INNER join MES.TM_BAS_WORK_ORDER_ASSEMBLY_SETTING c with(NOLOCK) on b.SETTING_FID =  c.FID
                              inner join MES.TM_BAS_EQUIP_CMD_PARAMS d with(NOLOCK) on c.FID = d.SOURCE_FID
                              where a.ORDER_CODE = '{orderCode}' and b.LOCATION in ('{string.Join("', '", OrderTagHelper.TargetStationSet.ToArray())}')";
            var value = DBHelper.DB.Ado.ExecuteScalar(sqlCmd);
            LogHelper.Log.LogInfo($"Calculate {value}", LogHelper.LogType.Information);
            return Convert.ToInt32(value);
        }
    }
}
