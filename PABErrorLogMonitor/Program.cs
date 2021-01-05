using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace PABErrorLogMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
                .Build();
            var AlarmValue = int.Parse(ConfigurationManager.AppSettings["AlarmValue"]);

            while (true)
            {
                var cust_info = freeSql.Select<MES_TT_CI_CUST_SORT_INFO>()
                    .WithLock(SqlServerLock.NoLock)
                    .OrderByDescending(o => o.CREATE_DATE)
                    .First();

                var oo = DateTime.Now - cust_info.CREATE_DATE;

                var error_code = freeSql.Select<MES_TS_SYS_SAP_ERROR_CODE>()
                    .Where(o => o.ERROR_CODE.Equals("PAB", StringComparison.OrdinalIgnoreCase)).ToList().FirstOrDefault() ??
                    new MES_TS_SYS_SAP_ERROR_CODE
                    {
                        ERROR_CODE = "PAB",
                        ALLOW_RESEND = false,
                        REMARK = string.Empty,
                        PLANT = "HLP",
                        VALID_FLAG = false,
                        CREATE_USER = "PAB",
                        CREATE_DATE = DateTime.Now,
                    };

                if (oo.TotalSeconds > AlarmValue)
                {
                    error_code.REMARK = "ERROR";
                    error_code.VALID_FLAG = true;
                    freeSql.InsertOrUpdate<MES_TS_SYS_SAP_ERROR_CODE>().SetSource(error_code).ExecuteAffrows();
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Create New Error.");
                    // new MailHelper().Send("PFCS ALERT", message, System.Net.Mail.MailPriority.High);
                }
                else
                {
                    error_code.REMARK = string.Empty;
                    error_code.VALID_FLAG = false;
                    freeSql.InsertOrUpdate<MES_TS_SYS_SAP_ERROR_CODE>().SetSource(error_code).ExecuteAffrows();
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Clear Error.");
                }

                Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["Interval"]) * 1000);
            }
        }
    }
}
