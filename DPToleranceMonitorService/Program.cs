using Autofac;
using DPToleranceMonitorService.Extend;
using DPToleranceMonitorService.Monitor;
using System;
using System.IO;

namespace DPToleranceMonitorService
{
    class Program
    {
        static void Main(string[] args)
        {
            container = BuildContainer();

            var opc = container.Resolve<OPCMonitor>();
            opc.Initialize();

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                opc.StopMonitor();
            };

            ShowMessage();

            // 获取输入命令
            string[] command = null;
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
                    string area = command.Length >= 2 ? command[1] : string.Empty;
                    opc.Show(area);
                }
                else if (command[0].Equals("arealist", StringComparison.OrdinalIgnoreCase))
                {
                    opc.AreaList();
                }
                else if (command[0].Equals("clear", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                }
                else if (command[0].Equals("monitor", StringComparison.OrdinalIgnoreCase))
                {
                    string area = command.Length >= 2 ? command[1] : string.Empty;
                    if (string.IsNullOrWhiteSpace(area))
                    {
                        Console.WriteLine("Please refer to CMD list.");
                        return;
                    }
                    Console.WriteLine("Ctrl + C exit from Monitor.");
                    var cursorLeft = Console.CursorLeft;
                    var cursorTop = Console.CursorTop;
                    opc.Monitor(cursorLeft, cursorTop, area);
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
        }

        private static void ShowMessage()
        {
            Console.WriteLine("".PadRight(Console.WindowWidth, '*'));
            Console.WriteLine(File.ReadAllText("Readme.txt"));
            Console.WriteLine("".PadRight(Console.WindowWidth, '*'));
        }

        private static IContainer BuildContainer()
        {
            var toleranceInstance = ToleranceExtend.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFile", "Tolerance.json"));

            var builder = new ContainerBuilder();
            builder.Register<OPCMonitor>((o, p) => new OPCMonitor(toleranceInstance)).InstancePerLifetimeScope();
            return builder.Build();
        }

        private static IContainer container = null;
    }
}
