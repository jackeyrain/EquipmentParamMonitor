using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "mes.TT_WM_SHIPPING_DETAIL")]
    public class SHIPPING_DETAIL
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long ID { get; set; }
        public long SHIPPING_ID { get; set; }
        public long CUST_SORT_INFO_ID { get; set; }
        public string PART_NO { get; set; }
        public string PART_NAME { get; set; }
        public long LOGIC_SEQ { get; set; }
        public long CUST_INFO_SEQ { get; set; }
        public long PRODUCTION_LOG_ID { get; set; }
        public long BARCODE_ID { get; set; }
        public string BARCODE { get; set; }
        public string CUST_PART_NO { get; set; }
        public decimal PLAN_QTY { get; set; }
        public decimal ACTUAL_QTY { get; set; }
        public decimal PAID_QTY { get; set; }
        public bool VALID_FLAG { get; set; }
        public Guid FID { get; set; }
        public Guid SHIPPING_FID { get; set; }
        public string CUST_ORDER_CODE { get; set; }
        public int STATUS { get; set; }
        public long SOURCE_ID { get; set; }

    }
}
