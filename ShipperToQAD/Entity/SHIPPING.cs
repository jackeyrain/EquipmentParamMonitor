using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TT_WM_SHIPPING")]
    public class SHIPPING
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long ID { get; set; }
        public long SEQ { get; set; }
        public string SHIPPING_CODE { get; set; }
        public int SHIPPING_TYPE { get; set; }
        public int STATUS { get; set; }
        public Guid FID { get; set; }
        public Guid? PART_SHIPPING_FID { get; set; }
        public string CHECK_CRAFT { get; set; }
        public string ROAD_PROCESS { get; set; }

        public List<SHIPPING_DETAIL> SHIPPING_DETAILs { get; set; }
    }
}
