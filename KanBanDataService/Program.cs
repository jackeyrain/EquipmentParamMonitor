using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace KanBanDataService
{
    static class Program
    {
        public static System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            var model = ConfigurationManager.AppSettings["MODEL"];
            if (model.Equals("service", StringComparison.OrdinalIgnoreCase))
            {
                Jakware.LogHelper.LogHelper.Log.LogInfo("Start service by Service Model.", Jakware.LogHelper.LogHelper.LogType.Information);
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service()
                };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Jakware.LogHelper.LogHelper.Log.LogInfo("Start service by Application Model.", Jakware.LogHelper.LogHelper.LogType.Information);
                new KanbanData().Start();

                Console.WriteLine("Input Exit to exit application");
                while (!Console.ReadLine().Equals("exit", StringComparison.OrdinalIgnoreCase) && !Program.cts.IsCancellationRequested)
                {

                }
            }
        }
    }
}
