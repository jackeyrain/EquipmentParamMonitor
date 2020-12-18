using System;
using System.Configuration;

namespace KanBanDataService
{
    public class DBHelper
    {
        static IFreeSql fsql = null;
        static object lockObj = new object();
        public static IFreeSql DBInstance
        {
            get
            {
                if (fsql == null)
                {
                    lock (lockObj)
                    {
                        if (fsql == null)
                        {
                            fsql = new FreeSql.FreeSqlBuilder()
                                 .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["PISCES"])
                                 // .UseMonitorCommand(null, (o, p) => Console.WriteLine(o.CommandText))
                                 .Build();
                        }
                    }
                }
                return fsql;
            }
        }

        public int GetWIPCount()
        {
            string sql = $"SELECT COUNT(1) 'COUNT' From MES.TT_APS_WORK_ORDER with(NOLOCK) where STATUS in (20, 25, 120) and ASSEMBLY_LINE = '{ConfigurationManager.AppSettings["PRODUCTIONLINE"]}' and CREATE_DATE > DATEADD(HOUR, -12, GETDATE())";
            var value = DBInstance.Ado.ExecuteScalar(sql);
            return Convert.ToInt32(value ?? 0);
        }

        public int GetWorkOrderCount((DateTime begin, DateTime end) dt)
        {
            string sql = $"SELECT Count(1) 'COUNT' From MES.TT_APS_WORK_ORDER with(NOLOCK) where ASSEMBLY_LINE = '{ConfigurationManager.AppSettings["PRODUCTIONLINE"]}' and CREATE_DATE BETWEEN '{dt.begin}' and '{dt.end}'";
            var value = DBInstance.Ado.ExecuteScalar(sql);
            return Convert.ToInt32(value ?? 0);
        }

        public int GetRebuildCount((DateTime begin, DateTime end) dt)
        {
            string sql = $"SELECT COUNT(1) 'COUNT' from (SELECT Count(1) 'COUNT' From MES.TT_APS_WORK_ORDER with(NOLOCK) where SORT_FLAG = 'B' AND ASSEMBLY_LINE = '{ConfigurationManager.AppSettings["PRODUCTIONLINE"]}' and CREATE_DATE BETWEEN '{dt.begin}' and '{dt.end}') as P";
            var value = DBInstance.Ado.ExecuteScalar(sql);
            return Convert.ToInt32(value ?? 0);
        }

        public int GetRepairCount((DateTime begin, DateTime end) dt)
        {
            string sql = $"SELECT COUNT(1) 'COUNT' From mes.TT_APS_REWORK_LOG with(NOLOCK) where ASSEMBLY_LINE = '{ConfigurationManager.AppSettings["PRODUCTIONLINE"]}' and CREATE_DATE BETWEEN '{dt.begin}' and '{dt.end}' GROUP BY ORDER_CODE";
            var value = DBInstance.Ado.ExecuteScalar(sql);
            return Convert.ToInt32(value ?? 0);
        }
    }
}
