using FreeSql.DataAnnotations;
using System;

namespace DPToleranceMonitorService.Model
{
#if DEBUG
    [Table(Name = "MES.TT_WSDP_EQUIPMENT_LOG", DisableSyncStructure = false)]
#else
    [Table(Name = "MES.TT_WSDP_EQUIPMENT_LOG", DisableSyncStructure = true)]
#endif

    public class TT_WSDP_EQUIPMENT_LOG
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }
        [Column(DbType = "varchar(50)")]
        public string Area { get; set; }
        [Column(DbType = "varchar(50)")]
        public string Accurate { get; set; }
        [Column(DbType = "varchar(50)")]
        public string Name { get; set; }
        [Column(DbType = "varchar(200)")]
        public string TagAddress { get; set; }
        [Column(DbType = "varchar(50)")]
        public string Value { get; set; }
        [Column(DbType = "varchar(50)")]
        public string CREATE_USER { get; set; }
        [Column(InsertValueSql = "getdate()")]
        public DateTime CREATE_TIME { get; set; }
    }
}
