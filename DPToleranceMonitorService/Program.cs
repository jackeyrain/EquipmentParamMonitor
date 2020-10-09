using Autofac;
using DPToleranceMonitorService.Extend;
using DPToleranceMonitorService.Model;
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


            Console.WriteLine(File.ReadAllText("Readme.txt"));
            // 获取输入命令
            string[] command = null;
            do
            {
                command = Console.ReadLine().Split(new[] { ' ' });
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
            } while (!command[0].Equals("exit", StringComparison.OrdinalIgnoreCase));
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
