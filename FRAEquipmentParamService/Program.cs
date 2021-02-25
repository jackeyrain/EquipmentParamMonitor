using FRAEquipmentParamService.Implement;
using FRAEquipmentParamService.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService
{
    class Program
    {
        static List<IStation> stationMonitors = new List<IStation>();
        static void Main(string[] args)
        {
            var configFile = Directory.GetFiles("Data", "*.xlsx");
            var configFileInfo = new List<FileInfo>();
            foreach (var config in configFile)
            {
                configFileInfo.Add(new FileInfo(config));
            }
            var loadConfig = configFileInfo.FirstOrDefault(o => o.Name.Equals("load.xlsx", StringComparison.OrdinalIgnoreCase));
            var loadData = new StationDataConvert(loadConfig.FullName).LoadDataParse();
            var loadStation = new StationLoadMonitor(loadData);

            configFileInfo.Remove(loadConfig);
            foreach (var file in configFileInfo)
            {
                var data = new StationDataConvert(file.FullName).DataParse();
                var stationMonitor = new StationMonitor(data);
                stationMonitors.Add(stationMonitor);
            }
            stationMonitors.ForEach(o => o.Initialize());

            Console.ReadKey(false);
        }
    }
}
