using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ProjectArrow.Models
{
    public static class DBHelper
    {
        private static IFreeSql freeSql = null;
        private static object _lock = new object();
        public static IFreeSql Db
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
                                    .UseConnectionString(FreeSql.DataType.SqlServer, WebConfigurationManager.AppSettings["db_connectionstring"])
                                    .UseMonitorCommand(o => Console.WriteLine(o.CommandText), null)
                                    .Build();
                        }
                    }
                }
                return freeSql;
            }
        }
    }
}