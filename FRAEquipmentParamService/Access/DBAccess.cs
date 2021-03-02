using DPToleranceMonitorService.Model;
using System;
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
                .UseAutoSyncStructure(true)
                .UseMonitorCommand(o =>
                {
                    LogHelper.Log.LogInfo($"{o.CommandText}", LogHelper.LogType.Information);
                })
                //,
                //(o, p) =>
                //{
                //    Console.WriteLine(o.CommandText);
                //    Console.WriteLine(p);
                //})
                .Build();
        }

        public static IFreeSql Instance { get { return freeSql; } }
    }
}
