using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace DPToolingService
{

    /// <summary>
    /// 排产_装配目视项
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TT_APS_WORK_ORDER_ASSEMBLY", DisableSyncStructure = true)]
    public partial class MES_TT_APS_WORK_ORDER_ASSEMBLY
    {

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }

        /// <summary>
        /// 条码打印方式
        /// </summary>
        [JsonProperty]
        public int? BARCODE_PRINT_TYPE { get; set; }

        /// <summary>
        /// 条码打印机
        /// </summary>
        [JsonProperty, Column(StringLength = 128)]
        public string BARCODE_PRINTER { get; set; }

        /// <summary>
        /// 条码打印模板
        /// </summary>
        [JsonProperty, Column(StringLength = 512)]
        public string BARCODE_TEMPLATE { get; set; }

        /// <summary>
        /// COMMON_CREATE_DATE
        /// </summary>
        [JsonProperty]
        public DateTime CREATE_DATE { get; set; }

        [JsonProperty]
        public DateTime? CREATE_DATE_UTC { get; set; }

        /// <summary>
        /// COMMON_CREATE_USER
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string CREATE_USER { get; set; }

        /// <summary>
        /// 设备FID
        /// </summary>
        [JsonProperty]
        public Guid? EQUIP_FID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [JsonProperty]
        public long? EQUIP_ID { get; set; }

        /// <summary>
        /// 装配爆炸图FID
        /// </summary>
        [JsonProperty]
        public Guid? IMAGE_FID { get; set; }

        [JsonProperty]
        public bool? IS_PRINTED { get; set; }

        /// <summary>
        /// 工位代码
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string LOCATION { get; set; }

        [JsonProperty, Column(DbType = "decimal(10,4)")]
        public decimal? MAX_VALUE { get; set; }

        [JsonProperty, Column(DbType = "decimal(10,4)")]
        public decimal? MIN_VALUE { get; set; }

        /// <summary>
        /// 工单FID
        /// </summary>
        [JsonProperty]
        public Guid? ORDER_FID { get; set; }

        /// <summary>
        /// 工单ID
        /// </summary>
        [JsonProperty]
        public long? ORDER_ID { get; set; }

        /// <summary>
        /// 工单明细FID
        /// </summary>
        [JsonProperty]
        public Guid? ORDER_PART_FID { get; set; }

        /// <summary>
        /// 工单明细ID
        /// </summary>
        [JsonProperty]
        public long? ORDER_PART_ID { get; set; }

        /// <summary>
        /// 目视描述
        /// </summary>
        [JsonProperty, Column(StringLength = 128)]
        public string PART_DESC { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        [JsonProperty, Column(StringLength = 128)]
        public string PART_NAME { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string PART_NO { get; set; }

        /// <summary>
        /// 零件类型代码
        /// </summary>
        [JsonProperty, Column(StringLength = 16)]
        public string PART_TYPE_CODE { get; set; }

        /// <summary>
        /// X座标
        /// </summary>
        [JsonProperty]
        public int? PointX { get; set; }

        /// <summary>
        /// Y座标
        /// </summary>
        [JsonProperty]
        public int? PointY { get; set; }

        /// <summary>
        /// 工序代码
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string PROCESS { get; set; }

        /// <summary>
        /// 工艺顺序号
        /// </summary>
        [JsonProperty]
        public int? PROCESS_SEQ { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty, Column(StringLength = -2)]
        public string REMARK { get; set; }

        /// <summary>
        /// 汇报零件号
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string REPORT_PART_NO { get; set; }

        /// <summary>
        /// 汇报方式
        /// </summary>
        [JsonProperty]
        public int? REPORT_TYPE { get; set; }

        /// <summary>
        /// 装配序号
        /// </summary>
        [JsonProperty]
        public int? ROUTE_SEQ { get; set; }

        /// <summary>
        /// 扫描规则
        /// </summary>
        [JsonProperty, Column(StringLength = 512)]
        public string SCAN_RULE { get; set; }

        /// <summary>
        /// 装配件条码确认方式
        /// </summary>
        [JsonProperty]
        public int? SCAN_TYPE { get; set; }

        /// <summary>
        /// 设置FID
        /// </summary>
        [JsonProperty]
        public Guid? SETTING_FID { get; set; }

        /// <summary>
        /// SAP来源库位
        /// </summary>
        [JsonProperty, Column(StringLength = 16)]
        public string SOURCE_SAP_CODE { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty]
        public int? STATUS { get; set; }

        /// <summary>
        /// SAP目标库位
        /// </summary>
        [JsonProperty, Column(StringLength = 16)]
        public string TARGET_SAP_CODE { get; set; }

        /// <summary>
        /// COMMON_UPDATE_DATE
        /// </summary>
        [JsonProperty]
        public DateTime? UPDATE_DATE { get; set; }

        [JsonProperty]
        public DateTime? UPDATE_DATE_UTC { get; set; }

        /// <summary>
        /// COMMON_UPDATE_USER
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string UPDATE_USER { get; set; }

        /// <summary>
        /// 有效标记
        /// </summary>
        [JsonProperty]
        public bool? VALID_FLAG { get; set; }

        /// <summary>
        /// 目视序号
        /// </summary>
        [JsonProperty]
        public int? VISUAL_SEQ { get; set; }

    }

}
