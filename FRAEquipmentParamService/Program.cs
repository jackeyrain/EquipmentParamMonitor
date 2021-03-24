using DPToleranceMonitorService.Model.DB;
using FRAEquipmentParamService.Access;
using FRAEquipmentParamService.Implement;
using FRAEquipmentParamService.Interface;
using FRAEquipmentParamService.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService
{
    class Program
    {
        // 任务集合
        public static List<IStation> stationMonitors = new List<IStation>();
        static List<StationEntity> stationEntity = new List<StationEntity>();

        static void Main(string[] args)
        {
            LogHelper.Log.LogInfo("FRA Equipment Monitor is Starting.", LogHelper.LogType.Information, false);
            var configFile = Directory.GetFiles("Data", "*.xlsx");
            var configFileInfo = new List<FileInfo>();
            foreach (var config in configFile)
            {
                configFileInfo.Add(new FileInfo(config));
            }
            configFileInfo.ForEach(o =>
            {
                LogHelper.Log.LogInfo($"Get configuration file {o.Name}", LogHelper.LogType.Information, false);
            });

            var loadConfig = configFileInfo.FirstOrDefault(o => o.Name.Equals("load.xlsx", StringComparison.OrdinalIgnoreCase));
            var loadData = new StationDataConvert(loadConfig.FullName).LoadDataParse();
            var loadStation = new StationLoadMonitor(loadData);
            // 上线工位绑定任务
            stationMonitors.Add(loadStation);
            configFileInfo.Remove(loadConfig);

            foreach (var file in configFileInfo)
            {
                // 站点参数收集任务
                var data = new StationDataConvert(file.FullName).DataParse();
                stationEntity.Add(data);
                var stationMonitor = new StationMonitor(data);
                stationMonitors.Add(stationMonitor);
            }
            // 开始执行任务
            stationMonitors.ForEach(o => o.Initialize());
            new ServiceHostServer(stationEntity).Start();

            var command = string.Empty;
            do
            {
                command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case "exit":
                        stationMonitors.ForEach(o => o.Dispose());
                        return;
                }
            }
            while (true);
        }
    }
}
