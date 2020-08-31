using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TL_SYS_QAD_OUTBOUND_LOG")]
    public class SYS_QAD_OUTBOUND_LOG
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }
        public Guid FID { get; set; }
        public string TRANS_ID { get; set; }
        public string METHORD_NAME { get; set; }
        public int EXECUTE_RESULT { get; set; }
        public DateTime EXECUTE_START_TIME { get; set; }
        public DateTime EXECUTE_END_TIME { get; set; }
        public int EXECUTE_TIMES { get; set; }
        public string KEY_VALUE { get; set; }
        [Column(DbType = "varchar(-1)")]
        public string SOURCE_XML { get; set; }
        public string ERROR_DESCRIPTION { get; set; }
        public bool VALID_FLAG { get; set; }
        public string CREATOR { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string ERROR_CODE { get; set; }
    }
}
