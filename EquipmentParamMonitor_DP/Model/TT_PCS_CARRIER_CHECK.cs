using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.Model
{
    [SugarTable("MES.TT_PCS_CARRIER_CHECK")]
    public class TT_PCS_CARRIER_CHECK
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public Int64 ID { get; set; }
        public string CARRIER_NO { get; set; }
        public string WORK_ORDER { get; set; }
        public bool IS_EMPTY { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public DateTime CREATE_DATE_UTC { get; set; }
        public DateTime UPDATE_DATE_UTC { get; set; }
    }
}
