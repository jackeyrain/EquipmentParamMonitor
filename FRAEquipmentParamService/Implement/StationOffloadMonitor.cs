using FRAEquipmentParamService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService.Implement
{
    public class StationOffloadMonitor : IDisposable, IStation
    {
        public StationOffloadMonitor(LoadStationEntity loadStationEntity)
        {
            LoadStationEntity = loadStationEntity;
        }

        public LoadStationEntity LoadStationEntity { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public string WriteValue(string nodeId, int value)
        {
            throw new NotImplementedException();
        }
    }
}
