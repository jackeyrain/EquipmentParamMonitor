using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace ShippingEmergency {

	/// <summary>
	/// 基础数据_零件发运组
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_PART_SHIPPING", DisableSyncStructure = true)]
	public partial class MES_TM_BAS_PART_SHIPPING {

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 是否自动创建发运单
		/// </summary>
		[JsonProperty]
		public bool? AUTO_FLAG { get; set; }

		/// <summary>
		/// 是否自动打印发运单
		/// </summary>
		[JsonProperty]
		public bool? AUTO_PRINT_FLAG { get; set; }

		[JsonProperty]
		public bool? AUTO_SAVE_FLAG { get; set; }

		/// <summary>
		/// 是否自动确认发运单
		/// </summary>
		[JsonProperty]
		public bool? AUTO_SUBMIT { get; set; }

		/// <summary>
		/// 承运方代码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string CARRIER_CODE { get; set; }

		[JsonProperty]
		public bool? CBR_FLAG { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string CHECK_ORDER { get; set; }

		[JsonProperty]
		public int? CHECK_SEQ_COUNT { get; set; } = 0;

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

		[JsonProperty]
		public bool? CUST_ORDER_NO_FLAG { get; set; }

		/// <summary>
		/// 客户工厂代码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string CUST_PLANT_CODE { get; set; }

		/// <summary>
		/// 信息延迟时间
		/// </summary>
		[JsonProperty]
		public int? DELAY_CREATE_TIME { get; set; }

		[JsonProperty]
		public bool END_SCAN { get; set; } = false;

		[JsonProperty, Column(StringLength = 200)]
		public string EQUIPMENT_FID { get; set; } = "NULL";

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

		/// <summary>
		/// FID
		/// </summary>
		[JsonProperty]
		public Guid? FID { get; set; }

		/// <summary>
		/// 客户信息点
		/// </summary>
		[JsonProperty]
		public long? INFO_POINT_ID { get; set; }

		/// <summary>
		/// 是否自动发送ASN
		/// </summary>
		[JsonProperty]
		public bool? IS_AUTO_ASN_SEND { get; set; }

		/// <summary>
		/// 是否自动统计
		/// </summary>
		[JsonProperty]
		public bool? IS_AUTO_STATISTICS { get; set; }

		[JsonProperty]
		public bool? IS_CHECK_SEQ { get; set; } = false;

		[JsonProperty]
		public bool? IS_REVERSE { get; set; }

		[JsonProperty]
		public bool IS_SCAN_SUPPLIER_CODE { get; set; } = false;

		/// <summary>
		/// 是否需要扫描防错
		/// </summary>
		[JsonProperty]
		public bool? IS_SCAN_VALIDATION_SEQ { get; set; }

		[JsonProperty]
		public bool? IS_SEQUENTIAL_SCAN { get; set; }

		[JsonProperty]
		public bool? IS_TRANSFER { get; set; }

		[JsonProperty]
		public bool? IS_TRANSFER_WITH_LOT { get; set; } = false;

		[JsonProperty]
		public DateTime? LAST_HANDLE_TIME { get; set; }

		[JsonProperty]
		public DateTime? LAST_HANDLE_TIME_UTC { get; set; }

		[JsonProperty]
		public int? LASTSHIPTIME_INTERVAL { get; set; }

		/// <summary>
		/// 最晚创建时间
		/// </summary>
		[JsonProperty]
		public int? LATEST_CREATE_TIME { get; set; }

		[JsonProperty]
		public long? LOADING_DEFAULT_NUMBER { get; set; }

		/// <summary>
		/// 是否启用装车防错
		/// </summary>
		[JsonProperty]
		public bool? LOADING_FLAG { get; set; }

		[JsonProperty]
		public int? LOADING_SEQ { get; set; }

		[JsonProperty, Column(StringLength = 16)]
		public string LOADING_TAG { get; set; }

		/// <summary>
		/// 装车料架最大数量
		/// </summary>
		[JsonProperty]
		public int? MAX_RACK_QTY { get; set; }

		/// <summary>
		/// 发运料架代码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string PACKAGE_TYPE_CODE { get; set; }

		/// <summary>
		/// 零件发运组代码
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string PART_SHIPPING_CODE { get; set; }

		/// <summary>
		/// 零件发运组名称
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string PART_SHIPPING_NAME { get; set; }

		/// <summary>
		/// 高
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? PKG_HEIGHT { get; set; }

		/// <summary>
		/// 长
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? PKG_LENGTH { get; set; }

		/// <summary>
		/// 宽
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? PKG_WIDTH { get; set; }

		/// <summary>
		/// 工厂代码
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

		[JsonProperty]
		public int? PRINT_COLUMN_COUNT { get; set; }

		/// <summary>
		/// 打印分数
		/// </summary>
		[JsonProperty]
		public int? PRINT_COPIES { get; set; }

		[JsonProperty, Column(StringLength = 200)]
		public string PRINT_DETAIL_PRINTER { get; set; }

		[JsonProperty, Column(StringLength = 200)]
		public string PRINT_DETAIL_TEMPLATE { get; set; }

		[JsonProperty]
		public int? PRINT_List_COUNT { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string PRINT_NAME { get; set; }

		[JsonProperty]
		public int? PRINT_SUM_COUNT { get; set; }

		/// <summary>
		/// 打印模板
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string PRINT_TEMPLATE { get; set; }

		/// <summary>
		/// 打印机
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string PRINTER { get; set; }

		/// <summary>
		/// 拉收时间间隔
		/// </summary>
		[JsonProperty]
		public int? RECEIVEDTIME_INTERVAL { get; set; }

		[JsonProperty]
		public long? REVERSE_NUMBER { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string ROAD_PROCESS { get; set; } = "";

		/// <summary>
		/// 圆整包装数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? ROUNDNESS_QTY { get; set; }

		/// <summary>
		/// 圆整方式
		/// </summary>
		[JsonProperty]
		public int? ROUNDNESS_TYPE { get; set; }

		/// <summary>
		/// 扫描确认零件方式
		/// </summary>
		[JsonProperty]
		public int? SCAN_PART_WAY { get; set; }

		/// <summary>
		/// 扫描确认顺序方式
		/// </summary>
		[JsonProperty]
		public int? SCAN_SEQ_WAY { get; set; }

		[JsonProperty]
		public long? SEQ_DIFF_VALUE { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string SHIPPING_ASSOCIATION { get; set; }

		/// <summary>
		/// 发运确认
		/// </summary>
		[JsonProperty]
		public int? SHIPPING_CONFIRMATION { get; set; }

		/// <summary>
		/// 发运组类型
		/// </summary>
		[JsonProperty]
		public int? SHIPPING_TYPE { get; set; }

		/// <summary>
		/// 发运单数据来源
		/// </summary>
		[JsonProperty]
		public int? SOURCE_TYPE { get; set; }

		[JsonProperty]
		public int? TO_LOT_SOURCE { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string TRANSFER_FROM { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string TRANSFER_TARGET { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string TRANSFER_TO { get; set; }

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
