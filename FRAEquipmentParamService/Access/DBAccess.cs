using DPToleranceMonitorService.Model;
using System.Collections.Generic;
using System.Configuration;

namespace FRAEquipmentParamService.Access
{
    public class DBAccess
    {
        private static IFreeSql freeSql = null;
        static DBAccess()
        {
            freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["DBConnection"])
                .UseAutoSyncStructure(false)
                .Build();
        }

        public static IFreeSql Instance { get; set; }
    }
}
