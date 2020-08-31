using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TL_SYS_QAD_OUTBOUND_LOG_DETAIL")]
    public class SYS_QAD_OUTBOUND_LOG_DETAIL
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long ID { get; set; }
        public Guid LOG_FID { get; set; }
        public int EXECUTE_RESULT { get; set; }
        public string SOURCE_XML { get; set; }
        public DateTime EXECUTE_START_TIME { get; set; }
        public DateTime EXECUTE_END_TIME { get; set; }
        public bool VALID_FLAG { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string ERROR_CODE { get; set; }
        public string ERROR_DESCRIPTION { get; set; }
    }
}
