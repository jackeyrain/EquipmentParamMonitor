using FRAEquipmentParamService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService.Implement
{
    interface IStation : IDisposable
    {
        void Initialize();

        string WriteValue(string nodeId, int value);
    }
}
