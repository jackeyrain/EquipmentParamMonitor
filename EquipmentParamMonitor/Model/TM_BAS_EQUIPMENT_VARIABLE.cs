using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.Model
{
    [SugarTable("MES.TM_BAS_EQUIPMENT_VARIABLE")]
    public class TM_BAS_EQUIPMENT_VARIABLE
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { set; get; }
        public int EQUIP_ID { set; get; }
        public int CLIENT_HANDLE { set; get; }
        public string NAME { set; get; }
        public string CODE { set; get; }
        public string DESCRIPTION { set; get; }
        public string VARIABLE_TYPE { set; get; }
        public string DATA_TYPE { set; get; }
    }
}
