using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TT_WM_LOADING_LIST")]
    public class LOADING_LIST
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long ID { get; set; }
        public long SEQ { get; set; }
        public string LOADING_LIST_CODE { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_PLANT_CODE { get; set; }
        public DateTime? START_SCAN_TIME { get; set; }
        public DateTime? END_SCAN_TIME { get; set; }
        public int STATUS { get; set; }
        public string TRUCK_NO { get; set; }
        public string REMARK { get; set; }
        public bool VALID_FLAG { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public Guid? FID { get; set; }

        public List<LOADING_LIST_DETAIL> LOADING_LIST_DETAILS { get; set; }
    }
}
