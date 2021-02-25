using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.Model
{
    [SugarTable("MES.TT_APS_WORK_ORDER")]
    public class TT_APS_WORK_ORDER
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public Int64 ID { get; set; }
        public string ORDER_CODE { get; set; }
        public Int64 ORDER_SEQ { get; set; }
        public string VIN_CODE { get; set; }
    }
}
