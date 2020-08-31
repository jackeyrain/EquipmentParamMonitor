using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TM_BAS_SHIPPING_PART")]
    public class PART_SHIPPING_DETAIL
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long ID { get; set; }
        public int SEQ { get; set; }
        public string PART_NO { get; set; }
        public string CUST_PART_NO { get; set; }
        public string FRAME_AGREEMENT_CODE { get; set; }
        public bool ENABLE_FLAG { get; set; }
        public bool VALID_FLAG { get; set; }
        public Guid FID { get; set; }
        public Guid PART_SHIPPING_FID { get; set; }
        public string BARCODE_RULE { get; set; }
        public string ROAD_PROCESS { get; set; }
        public PART_SHIPPING PART_SHIPPING { get; set; }
    }
}
