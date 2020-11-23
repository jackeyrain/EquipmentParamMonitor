using DPToleranceMonitorService.Model;
using System.Collections.Generic;
using System.Configuration;

namespace DPToleranceMonitorService.Access
{
    public class DBAccess
    {
        private static IFreeSql freeSql = null;
        public DBAccess()
        {
            freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["DBConnection"])
                .UseAutoSyncStructure(true)
                .Build();
        }

        public int InsertDuckData(IList<TT_WSDP_EQUIPMENT_LOG> data)
        {
            var result = freeSql.Insert<TT_WSDP_EQUIPMENT_LOG>(data).ExecuteAffrows();
            return result;
        }
    }
}
