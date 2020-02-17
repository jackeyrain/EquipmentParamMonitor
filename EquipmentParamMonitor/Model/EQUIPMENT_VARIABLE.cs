using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.Model
{
    public class EQUIPMENT_VARIABLE
    {
        public string NAME { get; set; }
        public string GROUP_NAME { get; set; }
        public string CODE { get; set; }
        public int CLIENT_HANDLE { set; get; }
        public string PARAMNAME { set; get; }
        public string PARAMCODE { set; get; }
        public string DESCRIPTION { set; get; }
        public string VARIABLE_TYPE { set; get; }
        public string DATA_TYPE { set; get; }
    }
}
