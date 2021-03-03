using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService.Model
{
    public class LoadStationEntity
    {
        public string OPCConnectionStr { get; set; }
        public string Name { get; set; }
        public NodeEntity Ready { get; set; }
        public NodeEntity PalletID { get; set; }
        public NodeEntity WorkOrder { get; set; }
    }
}
