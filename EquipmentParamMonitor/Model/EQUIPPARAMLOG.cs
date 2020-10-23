using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.Model
{
    [SugarTable("MES.EQUIPPARAMLOG")]
    public class EQUIPPARAMLOG
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public Int64 ID { get; set; }
        public string CARRIERID { get; set; }
        public string WORKORDER { get; set; }
        public string SEQUENCE { get; set; }
        public string VINCODE { get; set; }
        public string STATION { get; set; }
        public string PARAMTAG { get; set; }
        public string VALUE { get; set; }
        public DateTime CREATEDATETIME { get; set; }
    }
}
