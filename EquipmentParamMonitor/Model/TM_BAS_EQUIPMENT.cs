using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.Model
{
    [SugarTable("MES.TM_BAS_EQUIPMENT")]
    public class TM_BAS_EQUIPMENT
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string GROUP_NAME { get; set; }
        public string CODE { get; set; }
        public string IP { get; set; }
        public string EQUIP_TYPE { get; set; }
        public string PLANT { get; set; }
        public string ASSEMBLY_LINE { get; set; }
        public string LOCATION { get; set; }
        public bool VALID_FLAG { get; set; }

        public override string ToString()
        {
            return $"{GROUP_NAME}-{CODE}";
        }
    }
}
