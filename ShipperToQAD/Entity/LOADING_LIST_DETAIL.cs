using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TT_WM_LOADING_LIST_DETAIL")]
    public class LOADING_LIST_DETAIL
    {
        public long ID { get; set; }
        public Guid FID { get; set; }
        public int STATUS { get; set; }
        public bool VALID_FLAG { get; set; }
        public int SCAN_SEQ { get; set; }

        public Guid LOADING_LIST_FID { get; set; }
        [Navigate(nameof(LOADING_LIST_FID))]
        public LOADING_LIST LOADING_LIST { get; set; }


        public Guid SHIPPING_FID { get; set; }
    }
}
