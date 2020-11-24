using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPToolingService
{
    public static class DBHelper
    {
        private static IFreeSql freeSql = null;
        private static object _lock = new object();

        public static IFreeSql DB
        {
            get
            {
                if (freeSql == null)
                {
                    lock (_lock)
                    {
                        if (freeSql == null)
                        {
                            freeSql = new FreeSql.FreeSqlBuilder()
                                .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
                                 .UseMonitorCommand(null, (o, p) => Console.WriteLine(o.CommandText))
                                .UseAutoSyncStructure(false)
                                .Build();
                        }
                    }
                }
                return freeSql;
            }
        }
    }
}
