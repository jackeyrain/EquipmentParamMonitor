using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

namespace PABErrorLogMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["path"];
            if (!Directory.Exists("temp"))
            {
                Directory.CreateDirectory("temp");
            }
            var preValue = string.Empty;
            long preCount = 0;

            var freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
                .Build();

            preCount = freeSql.Select<MES_TI_CIM_USN_OPCS>()
                 .WithLock(SqlServerLock.NoLock)
                 .Where(o => o.HEADER.Equals("G", StringComparison.OrdinalIgnoreCase))
                 .GroupBy(o => o.CARSEQUENCE)
                 .Count();

            while (true)
            {
                var files = Directory.GetFiles(path, "info.log.*");
                var fileInfos = new List<FileInfo>();
                foreach (var file in files)
                {
                    fileInfos.Add(new FileInfo(file));
                }
                var lastInfo = fileInfos.OrderByDescending(o => o.LastWriteTime).First();
                lastInfo.CopyTo(Path.Combine("temp", lastInfo.Name), true);
                var content = File.ReadLines(Path.Combine("temp", lastInfo.Name));
                var message = content.Last();
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {message}");
                var currentValue = message.Substring(0, 14);

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


                var opcs_count = freeSql.Select<MES_TI_CIM_USN_OPCS>()
                      .WithLock(SqlServerLock.NoLock)
                      .Where(o => o.HEADER.Equals("G", StringComparison.OrdinalIgnoreCase))
                      .GroupBy(o => o.CARSEQUENCE)
                      .Count();

                if (preValue.Equals(currentValue, StringComparison.OrdinalIgnoreCase))
                {
                    // 相同，判断报文数量是否有新增，如果是取消上一次报警
                    if (preCount < opcs_count)
                    {
                        error_code.REMARK = string.Empty;
                        error_code.VALID_FLAG = false;
                        freeSql.InsertOrUpdate<MES_TS_SYS_SAP_ERROR_CODE>().SetSource(error_code).ExecuteAffrows();
                        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Cancel Last Error.");
                    }
                    else
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Noting To Do.");
                    }
                }
                else
                {
                    // 不相同，直接报警
                    error_code.REMARK = "ERROR";
                    error_code.VALID_FLAG = true;
                    freeSql.InsertOrUpdate<MES_TS_SYS_SAP_ERROR_CODE>().SetSource(error_code).ExecuteAffrows();
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Create New Error.");
                    preValue = currentValue;
                    new MailHelper().Send("PFCS ALERT", message, System.Net.Mail.MailPriority.High);
                }
                preCount = opcs_count;

                Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["Interval"]) * 1000);
            }
        }
    }
}
