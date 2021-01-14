using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace WorkOrderPrint
{

    /// <summary>
    /// 基础数据_零件生产组
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_PART_PRODUCT_GROUP", DisableSyncStructure = true)]
	public partial class MES_TM_BAS_PART_PRODUCT_GROUP {

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 生产线
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string ASSEMBLY_LINE { get; set; }

		/// <summary>
		/// 是否自动创建加工单
		/// </summary>
		[JsonProperty]
		public bool? AUTO_FLAG { get; set; }

		/// <summary>
		/// 是否自动打印加工单
		/// </summary>
		[JsonProperty]
		public bool? AUTO_PRINT_FLAG { get; set; }

		/// <summary>
		/// 是否自动发布
		/// </summary>
		[JsonProperty]
		public bool? AUTO_PUBLISH_FLAG { get; set; }

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
		/// 客户代码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string CUST_CODE { get; set; }

		/// <summary>
		/// 客户工厂代码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string CUST_PLANT_CODE { get; set; }

		/// <summary>
		/// 数据来源
		/// </summary>
		[JsonProperty]
		public int? DATA_SOURCE { get; set; }

		/// <summary>
		/// 信息延迟时间
		/// </summary>
		[JsonProperty]
		public int? DELAY_CREATE_TIME { get; set; }

		[JsonProperty]
		public int? DISPLAY_CELL_CONTENT_ROWS { get; set; }

		[JsonProperty]
		public int? DISPLAY_COLUMN_COUNT { get; set; }

		[JsonProperty]
		public bool? DISPLAY_ONE_BY_ONE { get; set; }

		[JsonProperty]
		public int? DISPLAY_TOTAL_COUNT { get; set; }

		[JsonProperty, Column(StringLength = 8)]
		public string DISPLAY_WHEN_EMPTY { get; set; }

		/// <summary>
		/// 扩展属性1
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string EXTEND_FIELD1 { get; set; }

		/// <summary>
		/// 扩展属性2
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string EXTEND_FIELD2 { get; set; }

		/// <summary>
		/// 扩展属性3
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string EXTEND_FIELD3 { get; set; }

		/// <summary>
		/// 扩展属性4
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string EXTEND_FIELD4 { get; set; }

		/// <summary>
		/// 扩展属性5
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string EXTEND_FIELD5 { get; set; }

		[JsonProperty]
		public Guid? FID { get; set; }

		/// <summary>
		/// 生产组代码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string GROUP_CODE { get; set; }

		/// <summary>
		/// 生产组名称
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string GROUP_NAME { get; set; }

		/// <summary>
		/// 客户信息点
		/// </summary>
		[JsonProperty]
		public long? INFO_POINT_ID { get; set; }

		/// <summary>
		/// 是否拆解BOM
		/// </summary>
		[JsonProperty]
		public bool? IS_DISMANTLE_BOM { get; set; }

		/// <summary>
		/// 排序信息是否头零件
		/// </summary>
		[JsonProperty]
		public bool? IS_HEAD_PART { get; set; }

		[JsonProperty]
		public bool? IS_ORDER_IDENTICAL { get; set; }

		/// <summary>
		/// 最晚创建时间
		/// </summary>
		[JsonProperty]
		public int? LATEST_CREATE_TIME { get; set; }

		/// <summary>
		/// 装配位置
		/// </summary>
		[JsonProperty]
		public long? MOUNTING_POSITION_ID { get; set; }

		/// <summary>
		/// 单号生成参数
		/// </summary>
		[JsonProperty, Column(StringLength = 512)]
		public string ORDER_CODE_PARAMS { get; set; }

		/// <summary>
		/// 工单号生成规则
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string ORDER_CODE_RULE { get; set; }

		/// <summary>
		/// 工厂代码
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

		/// <summary>
		/// 打印分数
		/// </summary>
		[JsonProperty]
		public int? PRINT_COPIES { get; set; }

		/// <summary>
		/// 打印模板
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string PRINT_TEMPLATE { get; set; }

		/// <summary>
		/// 打印机
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string PRINTER { get; set; }

		[JsonProperty]
		public int? PROD_TO_WORK_BREAK_TYPE { get; set; }

		[JsonProperty]
		public bool? PROD_TO_WORK_FLAG { get; set; }

		[JsonProperty]
		public int PRODUCT_DAYS { get; set; } = 0;

		/// <summary>
		/// 生产方式
		/// </summary>
		[JsonProperty]
		public int? PRODUCT_TYPE { get; set; }

		[JsonProperty]
		public int? QUEUE_QTY { get; set; } = 0;

		/// <summary>
		/// 生产逆转数
		/// </summary>
		[JsonProperty]
		public int? REVERSAL_COUNT { get; set; }

		/// <summary>
		/// 圆整数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? ROUNDNESS_QTY { get; set; }

		/// <summary>
		/// 圆整方式
		/// </summary>
		[JsonProperty]
		public int? ROUNDNESS_TYPE { get; set; }

		/// <summary>
		/// 同步Libra
		/// </summary>
		[JsonProperty]
		public bool? SYNC_LIBRA { get; set; }

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
		/// 是否有效
		/// </summary>
		[JsonProperty]
		public bool? VALID_FLAG { get; set; }

	}

}
