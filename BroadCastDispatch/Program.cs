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

            while (true)
            {
                try
                {
                    Implement(broadcastSections);
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
                var files = Directory.GetFiles(o.Source);
                Log($"There are {files.Length} files...", LogLevel.LOG);
                var fileInfos = Array.ConvertAll(files, f => new FileInfo(f));
                var valFileInfos = fileInfos.Where(fi => fi.CreationTime <= DateTime.Now.AddSeconds(5)).OrderBy(fi => fi.CreationTime).ToList();
                Log($"There are {valFileInfos.Count} available files...", LogLevel.LOG);
                foreach (var fi in valFileInfos)
                {
                    var content = File.ReadAllText(fi.FullName);
                    var key = content.Substring(10, 2).Trim();
                    if (!o.BroadcastType.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        Log($"{fi.FullName} doesn't match...", LogLevel.WARNING);
                        continue;
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
