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

            while (!Console.ReadLine().Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }

        private static IContainer BuildContainer()
        {
            var toleranceInstance = new ToleranceInstance();
            toleranceInstance.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigFile", "Tolerance.json"));

            var builder = new ContainerBuilder();
            builder.Register<OPCMonitor>((o, p) => new OPCMonitor(toleranceInstance)).InstancePerLifetimeScope();
            return builder.Build();
        }

        private static IContainer container = null;
    }
}
