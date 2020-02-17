using EquipmentParamMonitor.ACCESS;
using EquipmentParamMonitor.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace EquipmentParamMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = ConfigurationManager.AppSettings["MODEL"];
            if (model.Equals("service", StringComparison.OrdinalIgnoreCase))
            {
                ServiceBase[] ServicesToRun = new ServiceBase[] { new WinService() };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                Implement();
                while (!Console.ReadLine().Equals("exit", StringComparison.OrdinalIgnoreCase))
                {

                }
            }


            LogHelper.Log.LogInfo("exit...");
        }

        public static void Implement()
        {
            List<EquipmentEntity> entity = new List<EquipmentEntity>();

            EQUIPMENT_PARAM_MANAGER manager = new EQUIPMENT_PARAM_MANAGER();
            var _EQUIPMENT = manager.GetEquipmentSet();
            foreach (var equipment in _EQUIPMENT)
            {
                var equipParams = manager.GetEquipmentVariable(equipment.Select(o => o.NAME).ToArray());
                equipParams.ForEach(o =>
                {
                    LogHelper.Log.LogInfo(o.NAME);
                });
                entity.Add(new EquipmentEntity(string.Join("|", equipment.Select(o => o.NAME)), equipParams));
            }

            LogHelper.Log.LogInfo("Start to Initialize.");
            entity.ForEach(o =>
            {
                o.Initialize();
                LogHelper.Log.LogInfo($"{o.EntityName} Initialize Finish.");
            });
            LogHelper.Log.LogInfo("Initialize finish.");
            LogHelper.Log.LogInfo("Start to Monitor.");
        }
    }
}
