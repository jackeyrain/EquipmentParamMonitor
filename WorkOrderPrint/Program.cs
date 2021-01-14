using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkOrderPrint
{
    class Program
    {
        private static IFreeSql freeSql = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.ConnectionStrings["PiscesDB"].ConnectionString)
                 // .UseMonitorCommand(null, (o, p) => Console.WriteLine(o.CommandText))
                 .Build();
        private static List<ProductionGroup> groups = null;
        static void Main(string[] args)
        {
            var interval = int.Parse(ConfigurationManager.AppSettings["Interval"]);

            LogHelper.Log.LogInfo("Work order printing application is initialize.", LogHelper.LogType.Information);
            groups = ConfigurationManager.GetSection("Group") as List<ProductionGroup>;
            var groupCodes = Array.ConvertAll(groups.ToArray(), o => o.GroupCode);
            var product_groups = freeSql.Select<MES_TM_BAS_PART_PRODUCT_GROUP>().Where(o => o.VALID_FLAG.Value && groupCodes.Contains(o.GROUP_CODE)).ToList();
            while (interval > 0)
            {
                foreach (var group in product_groups)
                {
                    LogHelper.Log.LogInfo($"Execute {group.GROUP_NAME} work flow.", LogHelper.LogType.Information);
                    ExecuteFlow(group);
                    LogHelper.Log.LogInfo($"Execute {group.GROUP_NAME} completed.");
                }
                System.Threading.Thread.Sleep(interval);
            }

            LogHelper.Log.LogInfo("Work order printing application is closing.", LogHelper.LogType.Information);
        }

        static void ExecuteFlow(MES_TM_BAS_PART_PRODUCT_GROUP group)
        {
            var descTop1WO = freeSql.Select<MES_TT_APS_WORK_ORDER>()
                .WithLock(SqlServerLock.NoLock)
                .OrderBy(o => o.PRODUCT_SEQ)
                .Where(o => o.VALID_FLAG.Value && !o.PRINT_TIME.HasValue && o.STATUS == 20 && o.PRODUCT_GROUP_ID == group.ID)
                .First();

            if (descTop1WO == null) return;
            LogHelper.Log.LogInfo($"{descTop1WO.ORDER_CODE} printing.", LogHelper.LogType.Warn);
            var relatedGroup = groups.FirstOrDefault(o => o.GroupCode.Equals(group.GROUP_CODE, StringComparison.OrdinalIgnoreCase));

            _ = new PrinterHelper().SetDefault(relatedGroup.Printer);
            Thread.Sleep(1000);
            LogHelper.Log.LogInfo($"Default printer switch to {relatedGroup.Printer}.", LogHelper.LogType.Information);
            var bartender = new BartenderPrintHelper(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", relatedGroup.Template), 1);
            bartender.Print(new List<string>
            {
                descTop1WO.ORDER_CODE,
                descTop1WO.SORT_FLAG,
                descTop1WO.ORDER_SEQ.ToString(),
                descTop1WO.VIN_CODE,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            }, 1);
            LogHelper.Log.LogInfo($"{descTop1WO.ORDER_CODE} print completed.", LogHelper.LogType.Information);
            freeSql.Update<MES_TT_APS_WORK_ORDER>().SetSource(descTop1WO).Set(o => o.PRINT_TIME, DateTime.Now).ExecuteAffrows();
            LogHelper.Log.LogInfo($"{descTop1WO.ORDER_CODE} print status update completed.", LogHelper.LogType.Information);
        }
    }
}
