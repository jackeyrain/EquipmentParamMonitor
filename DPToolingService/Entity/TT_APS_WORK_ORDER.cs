using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace DPToolingService
{

	/// <summary>
	/// 排产_工单
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TT_APS_WORK_ORDER", DisableSyncStructure = true)]
	public partial class MES_TT_APS_WORK_ORDER
	{

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 产线代码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string ASSEMBLY_LINE { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string ASSY_CATEGORY { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string ASSY_CODE { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string ASSY_COLOR_NAME { get; set; }

		/// <summary>
		/// BOM编号
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string BOM_CODE { get; set; }

		[JsonProperty, Column(StringLength = 500)]
		public string BREAKPOINT_REMARK { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string CCR_NO { get; set; }

		[JsonProperty, Column(StringLength = 512)]
		public string COMMENTS { get; set; }

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
		/// 客户条码
		/// </summary>
		[JsonProperty, Column(StringLength = 1024)]
		public string CUST_BARCODE { get; set; }

		/// <summary>
		/// 客户订单号
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string CUST_ORDER_CODE { get; set; }

		/// <summary>
		/// 客户零件号
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string CUST_PART_NO { get; set; }

		/// <summary>
		/// 需求数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? DEMAND_QTY { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string ERP_ROUTING_CODE { get; set; }

		/// <summary>
		/// 扩展标志1
		/// </summary>
		[JsonProperty]
		public bool? EXTEND_FLAG1 { get; set; }

		[JsonProperty]
		public bool? FDOK_DONE { get; set; }

		/// <summary>
		/// FID
		/// </summary>
		[JsonProperty]
		public Guid? FID { get; set; }

		[JsonProperty]
		public long? INC_SEQ { get; set; }

		[JsonProperty]
		public bool? IS_DISMANTLED { get; set; } = false;

		[JsonProperty]
		public bool? IS_EMERGENCY { get; set; }

		/// <summary>
		/// 打印BTO包装条码
		/// </summary>
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

		/// <summary>
		/// LIBRA同步状态
		/// </summary>
		[JsonProperty]
		public bool? LIBRA_SYNC_STATUS { get; set; }

		/// <summary>
		/// 工位代码
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string LOCATION { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string LOT_NUMBER { get; set; }

		[JsonProperty, Column(StringLength = 8)]
		public string MOULD_VALUE { get; set; }

		/// <summary>
		/// 工单号
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string ORDER_CODE { get; set; }

		/// <summary>
		/// 工单序号
		/// </summary>
		[JsonProperty]
		public long? ORDER_SEQ { get; set; }

		/// <summary>
		/// 工单类型
		/// </summary>
		[JsonProperty]
		public int? ORDER_TYPE { get; set; }

		/// <summary>
		/// 工单版本
		/// </summary>
		[JsonProperty]
		public int? ORDER_VERSION { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string ORIGINAL_CODE { get; set; }

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
		/// 零件打印状态
		/// </summary>
		[JsonProperty]
		public int? PART_NO_PRINT_STATUS { get; set; }

		/// <summary>
		/// 计划结束时间
		/// </summary>
		[JsonProperty]
		public DateTime? PLAN_END_TIME { get; set; }

		[JsonProperty]
		public DateTime? PLAN_END_TIME_UTC { get; set; }

		/// <summary>
		/// 计划开始时间
		/// </summary>
		[JsonProperty]
		public DateTime? PLAN_START_TIME { get; set; }

		[JsonProperty]
		public DateTime? PLAN_START_TIME_UTC { get; set; }

		/// <summary>
		/// 工厂代码
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

		/// <summary>
		/// 工单打印时间
		/// </summary>
		[JsonProperty]
		public DateTime? PRINT_TIME { get; set; }

		[JsonProperty]
		public DateTime? PRINT_TIME_UTC { get; set; }

		/// <summary>
		/// 生产日期
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string PRODUCT_DATE { get; set; }

		/// <summary>
		/// 生产组ID
		/// </summary>
		[JsonProperty]
		public long? PRODUCT_GROUP_ID { get; set; }

		/// <summary>
		/// 生产序号
		/// </summary>
		[JsonProperty]
		public long? PRODUCT_SEQ { get; set; }

		/// <summary>
		/// 排序信息PTR信息
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string PTR_INFO { get; set; }

		/// <summary>
		/// 拉动状态
		/// </summary>
		[JsonProperty]
		public int? PULLING_STATUS { get; set; }

		/// <summary>
		/// 实际结束时间
		/// </summary>
		[JsonProperty]
		public DateTime? REAL_END_TIME { get; set; }

		[JsonProperty]
		public DateTime? REAL_END_TIME_UTC { get; set; }

		/// <summary>
		/// 实际开始时间
		/// </summary>
		[JsonProperty]
		public DateTime? REAL_START_TIME { get; set; }

		[JsonProperty]
		public DateTime? REAL_START_TIME_UTC { get; set; }

		/// <summary>
		/// 剩余需求数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? REMAIN_QTY { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(StringLength = -2)]
		public string REMARK { get; set; }

		/// <summary>
		/// 已汇报数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? REPORT_QTY { get; set; }

		[JsonProperty]
		public bool REPRINT_FLAG { get; set; } = false;

		[JsonProperty]
		public int? REWORK_TIMES { get; set; }

		/// <summary>
		/// 班次
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string SHIFT_NAME { get; set; }

		/// <summary>
		/// 出货时间
		/// </summary>
		[JsonProperty]
		public DateTime? SHIPPING_TIME { get; set; }

		[JsonProperty]
		public DateTime? SHIPPING_TIME_UTC { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string SHOW_REMARK { get; set; }

		/// <summary>
		/// 排序工单标记
		/// </summary>
		[JsonProperty, Column(StringLength = 1)]
		public string SORT_FLAG { get; set; }

		/// <summary>
		/// 来源关联ID
		/// </summary>
		[JsonProperty]
		public long? SOURCE_ID { get; set; }

		/// <summary>
		/// 数据来源
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string SOURCE_TYPE { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		[JsonProperty]
		public int? STATUS { get; set; }

		[JsonProperty, Column(DbType = "varchar(8)")]
		public string STEER_POSITION { get; set; }

		/// <summary>
		/// 计量单位
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string UOM { get; set; }

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

		[JsonProperty, Column(StringLength = 64)]
		public string VEHICLE_CATEGORY { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string VEHICLE_GRADE { get; set; }

		/// <summary>
		/// 车型描述
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string VEHICLE_NAME { get; set; }

		/// <summary>
		/// 车型代码
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string VEHICLE_NO { get; set; }

		/// <summary>
		/// 生产版本
		/// </summary>
		[JsonProperty, Column(StringLength = 4)]
		public string VERID { get; set; }

		/// <summary>
		/// VIN号
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string VIN_CODE { get; set; }

	}

}
