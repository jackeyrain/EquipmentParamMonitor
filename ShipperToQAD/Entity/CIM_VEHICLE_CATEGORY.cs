using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipperToQAD.Entity
{
    [Table(Name = "MES.TI_CIM_VEHICLE_CATEGORY")]
    public class CIM_VEHICLE_CATEGORY
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }
        public Guid FID { get; set; }
        public string VEHICLE_CATEGORY_CODE { get; set; }
        public string VEHICLE_CATEGORY_NAME { get; set; }
        public string PLANT { get; set; }
        public string CUST_CODE { get; set; }
        public string CUST_PLANT_CODE { get; set; }
        public bool VALID_FLAG { get; set; }
        public string VEHICLE_YEAR { get; set; }
        public string PART_GROUP { get; set; }
    }
}
