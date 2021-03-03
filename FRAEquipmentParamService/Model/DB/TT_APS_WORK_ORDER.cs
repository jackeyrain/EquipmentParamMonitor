using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DPToleranceMonitorService.Model.DB
{

    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TT_APS_WORK_ORDER", DisableSyncStructure = true)]
    public partial class MES_TT_APS_WORK_ORDER
    {

        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }

        [JsonProperty, Column(StringLength = 16)]
        public string ASSEMBLY_LINE { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string ASSY_CATEGORY { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string ASSY_CODE { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string ASSY_COLOR_NAME { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string BOM_CODE { get; set; }

        [JsonProperty, Column(StringLength = 500)]
        public string BREAKPOINT_REMARK { get; set; }

        [JsonProperty, Column(DbType = "varchar(50)")]
        public string CCR_NO { get; set; }

        [JsonProperty, Column(StringLength = 512)]
        public string COMMENTS { get; set; }

        [JsonProperty]
        public DateTime CREATE_DATE { get; set; }

        [JsonProperty]
        public DateTime? CREATE_DATE_UTC { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string CREATE_USER { get; set; }

        [JsonProperty, Column(StringLength = 1024)]
        public string CUST_BARCODE { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string CUST_ORDER_CODE { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string CUST_PART_NO { get; set; }

        [JsonProperty, Column(DbType = "decimal(18,4)")]
        public decimal? DEMAND_QTY { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string ERP_ROUTING_CODE { get; set; }

        [JsonProperty]
        public bool? EXTEND_FLAG1 { get; set; }

        [JsonProperty]
        public bool? FDOK_DONE { get; set; }

        [JsonProperty]
        public Guid? FID { get; set; }

        [JsonProperty]
        public long? INC_SEQ { get; set; }

        [JsonProperty]
        public bool? IS_DISMANTLED { get; set; }

        [JsonProperty]
        public bool? IS_EMERGENCY { get; set; }

        [JsonProperty]
        public bool? IS_PRINT_BTO_PACKAGE_BARCODE { get; set; }

        [JsonProperty]
        public bool? IS_REPORT_PRODUCTNO_ASSYNO { get; set; }

        [JsonProperty]
        public bool? IS_SPAREPARTS { get; set; }

        [JsonProperty]
        public bool? IS_TO_PRODUCT { get; set; }

        [JsonProperty, Column(StringLength = 16)]
        public string KITTING_GROUP { get; set; }

        [JsonProperty]
        public bool? LIBRA_SYNC_STATUS { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string LOCATION { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string LOT_NUMBER { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string ORDER_CODE { get; set; }

        [JsonProperty]
        public long? ORDER_SEQ { get; set; }

        [JsonProperty]
        public int? ORDER_TYPE { get; set; }

        [JsonProperty]
        public int? ORDER_VERSION { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string ORIGINAL_CODE { get; set; }

        [JsonProperty, Column(StringLength = 128)]
        public string PART_NAME { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string PART_NO { get; set; }

        [JsonProperty]
        public int? PART_NO_PRINT_STATUS { get; set; }

        [JsonProperty]
        public DateTime? PLAN_END_TIME { get; set; }

        [JsonProperty]
        public DateTime? PLAN_END_TIME_UTC { get; set; }

        [JsonProperty]
        public DateTime? PLAN_START_TIME { get; set; }

        [JsonProperty]
        public DateTime? PLAN_START_TIME_UTC { get; set; }

        [JsonProperty, Column(StringLength = 8)]
        public string PLANT { get; set; }

        [JsonProperty]
        public DateTime? PRINT_TIME { get; set; }

        [JsonProperty]
        public DateTime? PRINT_TIME_UTC { get; set; }

        [JsonProperty, Column(StringLength = 16)]
        public string PRODUCT_DATE { get; set; }

        [JsonProperty]
        public long? PRODUCT_GROUP_ID { get; set; }

        [JsonProperty]
        public long? PRODUCT_SEQ { get; set; }

        [JsonProperty, Column(StringLength = 128)]
        public string PTR_INFO { get; set; }

        [JsonProperty]
        public int? PULLING_STATUS { get; set; }

        [JsonProperty]
        public DateTime? REAL_END_TIME { get; set; }

        [JsonProperty]
        public DateTime? REAL_END_TIME_UTC { get; set; }

        [JsonProperty]
        public DateTime? REAL_START_TIME { get; set; }

        [JsonProperty]
        public DateTime? REAL_START_TIME_UTC { get; set; }

        [JsonProperty, Column(DbType = "decimal(18,4)")]
        public decimal? REMAIN_QTY { get; set; }

        [JsonProperty, Column(StringLength = -2)]
        public string REMARK { get; set; }

        [JsonProperty, Column(DbType = "decimal(18,4)")]
        public decimal? REPORT_QTY { get; set; }

        [JsonProperty]
        public bool REPRINT_FLAG { get; set; } = false;

        [JsonProperty]
        public int? REWORK_TIMES { get; set; }

        [JsonProperty, Column(StringLength = 16)]
        public string SHIFT_NAME { get; set; }

        [JsonProperty]
        public DateTime? SHIPPING_TIME { get; set; }

        [JsonProperty]
        public DateTime? SHIPPING_TIME_UTC { get; set; }

        [JsonProperty, Column(DbType = "varchar(50)")]
        public string SHOW_REMARK { get; set; }

        [JsonProperty, Column(StringLength = 1)]
        public string SORT_FLAG { get; set; }

        [JsonProperty]
        public long? SOURCE_ID { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string SOURCE_TYPE { get; set; }

        [JsonProperty]
        public int? STATUS { get; set; }

        [JsonProperty, Column(DbType = "varchar(8)")]
        public string STEER_POSITION { get; set; }

        [JsonProperty, Column(StringLength = 8)]
        public string UOM { get; set; }

        [JsonProperty]
        public DateTime? UPDATE_DATE { get; set; }

        [JsonProperty]
        public DateTime? UPDATE_DATE_UTC { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string UPDATE_USER { get; set; }

        [JsonProperty]
        public bool? VALID_FLAG { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string VEHICLE_CATEGORY { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string VEHICLE_GRADE { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string VEHICLE_NAME { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string VEHICLE_NO { get; set; }

        [JsonProperty, Column(StringLength = 4)]
        public string VERID { get; set; }

        [JsonProperty, Column(StringLength = 64)]
        public string VIN_CODE { get; set; }

        public List<MES_TT_APS_WORK_ORDER_ASSEMBLY> mES_TT_APS_WORK_ORDER_ASSEMBLies { get; set; }

    }

}
