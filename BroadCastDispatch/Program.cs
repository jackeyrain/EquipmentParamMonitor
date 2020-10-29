using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BroadCastDispatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var broadcastSections = ConfigurationManager.GetSection("Broadcast") as List<Broadcast>;
            Log("Applicaton is start...");

            Monitor(broadcastSections);

            while (true)
            {
                try
                {
                    Implement(broadcastSections);
                }
                catch (FormatException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    Log(ex.Message, LogLevel.ERROR);
                }
                finally
                {
                    Thread.Sleep(5000);
                }
            }
        }

        private static async void Monitor(List<Broadcast> broadcasts)
        {
            await new TaskFactory().StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        StringBuilder message = new StringBuilder();
                        broadcasts.ForEach(o =>
                        {
                            var files = Directory.GetFiles(o.Source, "*.txt");
                            var fileInfos = new List<FileInfo>();
                            if (files.Length > 0)
                            {
                                fileInfos.AddRange(Array.ConvertAll(files, p => new FileInfo(p)).ToArray());
                                var last = fileInfos.OrderByDescending(p => p.CreationTime).FirstOrDefault();
                                if ((DateTime.Now - last.CreationTime).Minutes > 5)
                                {
                                    message.AppendLine($"{last.FullName} doesn't parse.");
                                }
                            }
                        });
                        if (!string.IsNullOrWhiteSpace(message.ToString()))
                        {
                            new MailHelper("liuxiao@yizit.cn;jibo@yizit.cn;shenmq@yizit.cn;lingyh@yizit.cn;yi.yu-ext@yfai.com;jie.zhou03@yfai.com;gang.lv@yfai.com")
                            .Send("报文卡住啦！！！！！！", message.ToString(), System.Net.Mail.MailPriority.High);
                        }
                    }
                    finally
                    {
                        await Task.Delay(300 * 1000);
                    }
                }
            });
        }

        private static void Implement(List<Broadcast> broadcasts)
        {
            broadcasts.ForEach(o =>
            {
                if (!Directory.Exists(o.Source))
                {
                    Log($"{o.Source} doesn't exit...", LogLevel.WARNING);
                    return;
                }

                Log($"Start to {o.BroadcastType} dispatch...", LogLevel.WARNING);
                var files = Directory.GetFiles(o.Source, "*.txt");
                Log($"There are {files.Length} files...", LogLevel.LOG);
                var fileInfos = Array.ConvertAll(files, f => new FileInfo(f));
                var valFileInfos = fileInfos.OrderBy(fi => fi.CreationTime).ToList();
                Log($"There are {valFileInfos.Count} available files...", LogLevel.LOG);
                foreach (var fi in valFileInfos)
                {
                    // 获取文件后，delay 3s，确保文件写入完成
                    Thread.Sleep(3 * 1000);
                    var content = File.ReadAllText(fi.FullName);
                    var key = content.Substring(10, 2).Trim();
                    if (!o.BroadcastType.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        Log($"{fi.FullName} doesn't match...", LogLevel.WARNING);
                        continue;
                    }
                    // 如果是G点报文，判断文件长度是否满足需求
                    if (o.BroadcastType.Equals("G", StringComparison.OrdinalIgnoreCase))
                    {
                        string partsContent = content.Substring(53, content.Length - 53);
                        List<string> partSet = new List<string>();
                        for (int i = 0; i < partsContent.Length;)
                        {
                            partSet.Add(partsContent.Substring(i, 12));
                            i += 12;
                        }
                        if (partSet.Count != 31)
                        {
                            new MailHelper().Send("Jelisoft PAB ERROR.", $"<h1><b>{fi.Name} doesn't match the count of operation.<br />When you correct it, open the transfer application again.</b></h1>", System.Net.Mail.MailPriority.High);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{fi.FullName} doesn't match the count of operation.");
                            Console.WriteLine($"When you correct it, open the transfer application again.");
                            Console.ReadKey(false);
                            Console.ResetColor();
                            throw new FormatException();
                        }
                    }

                    try
                    {
                        Log($"{fi.Name} move to {o.Dispatch} done...", LogLevel.LOG);
                        fi.MoveTo(Path.Combine(o.Dispatch, fi.Name));
                    }
                    catch (Exception ex)
                    {
                        Log($"{fi.Name} -- {ex.Message}", LogLevel.ERROR);
                    }
                }

                Thread.Sleep(1000);
            });
        }

        static void Log(string msg, LogLevel logLevel = LogLevel.NORMAL)
        {
            if (!Directory.Exists("Log")) Directory.CreateDirectory("Log");
            var message = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {msg}";
            switch (logLevel)
            {
                case LogLevel.NORMAL:
                    Console.WriteLine(message);
                    break;
                case LogLevel.LOG:
                    Console.WriteLine(message);
                    File.AppendAllText($"Log\\{DateTime.Now:yyyy-MM-dd}_log.txt", message + Environment.NewLine);
                    break;
                case LogLevel.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(message);
                    Console.ResetColor();
                    File.AppendAllText($"Log\\{DateTime.Now:yyyy-MM-dd}_warning.txt", message + Environment.NewLine);
                    break;
                case LogLevel.ERROR:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Console.ResetColor();
                    File.AppendAllText($"Log\\{DateTime.Now:yyyy-MM-dd}_error.txt", message + Environment.NewLine);
                    break;
            }

        }
    }

    public enum LogLevel : byte
    {
        NORMAL = 0,
        LOG = 1,
        WARNING = 2,
        ERROR = 3,
    }
}
