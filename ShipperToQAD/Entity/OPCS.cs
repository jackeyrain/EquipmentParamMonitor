using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TI_CIM_USN_OPCS")]
    public class OPCS
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }
        public string HEADER { get; set; }
        public string MODELYEAR { get; set; }
        public string VIN { get; set; }
        public long CARSEQUENCE { get; set; }
        public string CUSTCODE { get; set; }
        public DateTime MSGDATETIME { get; set; }

        [Navigate(nameof(OPCS_DETAIL.USNID))]
        public List<OPCS_DETAIL> OPCS_DETAILs { get; set; }
    }
}
