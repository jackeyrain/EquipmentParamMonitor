using System;
using System.Configuration;
using System.IO;

namespace DPToolingService
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMessage();
            LogHelper.Log.LogInfo("Tooling service is starting.", LogHelper.LogType.Information);

            var orderTags = ConfigurationManager.GetSection("OrderTagHelper") as OrderTagHelper;

            var service = new ServiceConsole(orderTags.OrderTags);

            string[] command;
            do
            {
                var inputValue = Console.ReadLine();
                if (string.IsNullOrEmpty(inputValue))
                {
                    command = new[] { string.Empty };
                    continue;
                }
                command = inputValue.Split(new[] { ' ' });
                // 如果是现实参数命令
                if (command[0].Equals("show", StringComparison.OrdinalIgnoreCase))
                {
                    service.Show();
                }
                else if (command[0].Equals("start", StringComparison.OrdinalIgnoreCase))
                {
                    service.Start();
                }
                else if (command[0].Equals("stop", StringComparison.OrdinalIgnoreCase))
                {
                    service.Stop();
                }
                else if (command[0].Equals("clear", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                }
                else if (command[0].Equals("send", StringComparison.OrdinalIgnoreCase))
                {
                    if (command.Length != 3)
                    {
                        Console.WriteLine("Please refer to CMD list.");
                    }
                    service.SendByManual(command[1], command[2]);
                }
                else if (command[0].Equals("CMD", StringComparison.OrdinalIgnoreCase))
                {
                    ShowMessage();
                }
                else
                {
                    Console.WriteLine("Please refer to CMD list.");
                }
            } while (!command[0].Equals("exit", StringComparison.OrdinalIgnoreCase));

            LogHelper.Log.LogInfo("Tooling service exits.", LogHelper.LogType.Information);
        }
        private static void ShowMessage()
        {
            Console.WriteLine("".PadRight(Console.WindowWidth, '*'));
            Console.WriteLine(File.ReadAllText("Readme.txt"));
            Console.WriteLine("".PadRight(Console.WindowWidth, '*'));
        }
    }
}
