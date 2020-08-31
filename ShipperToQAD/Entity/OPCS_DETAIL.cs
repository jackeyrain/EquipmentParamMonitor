using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TI_CIM_USN_OPCS_DETAIL")]
    public class OPCS_DETAIL
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }
        public string PARTNO { get; set; }
        public int QTY { get; set; }
        public long SEQUENCE { get; set; }
        public string OPERATION { get; set; }
        public int PARTLOCATION { get; set; }
        public DateTime MSGDATETIME { get; set; }
        public string POINT_TYPE { get; set; }

        public int USNID { get; set; }
        [Navigate(nameof(USNID))]
        public virtual OPCS OPCS { get; set; }
    }
}
