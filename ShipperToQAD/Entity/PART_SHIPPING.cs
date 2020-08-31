using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TM_BAS_PART_SHIPPING")]
    public class PART_SHIPPING
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long ID { get; set; }
        public string PART_SHIPPING_CODE { get; set; }
        public string PLANT { get; set; }
        public int SOURCE_TYPE { get; set; }
        public int SHIPPING_TYPE { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_PLANT_CODE { get; set; }
        public long INFO_POINT_ID { get; set; }
        public int ROUNDNESS_TYPE { get; set; }
        public decimal ROUNDNESS_QTY { get; set; }
        public Guid FID { get; set; }
        public List<PART_SHIPPING_DETAIL> pART_SHIPPING_DETAILs { get; set; }
    }
}
