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
	/// 仓储_发运单明细
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TT_WM_SHIPPING_DETAIL", DisableSyncStructure = true)]
	public partial class MES_TT_WM_SHIPPING_DETAIL {

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 实际发运数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? ACTUAL_QTY { get; set; }

		/// <summary>
		/// 条码
		/// </summary>
		[JsonProperty, Column(StringLength = -2)]
		public string BARCODE { get; set; }

		/// <summary>
		/// 条码ID
		/// </summary>
		[JsonProperty]
		public long? BARCODE_ID { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string CALLOFF_NO { get; set; }

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
		/// 客户排序信息序号
		/// </summary>
		[JsonProperty]
		public long? CUST_INFO_SEQ { get; set; }

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
		/// 客户排序信息ID
		/// </summary>
		[JsonProperty]
		public long? CUST_SORT_INFO_ID { get; set; }

		/// <summary>
		/// FID
		/// </summary>
		[JsonProperty]
		public Guid? FID { get; set; }

		/// <summary>
		/// 排序逻辑序号
		/// </summary>
		[JsonProperty]
		public long? LOGIC_SEQ { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string LZ_VIN_CODE { get; set; }

		/// <summary>
		/// 计量单位
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string MEASURING_UNIT_NO { get; set; }

		/// <summary>
		/// 结算数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? PAID_QTY { get; set; }

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
		/// 零件家族
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PARTS_FAMILY { get; set; }

		/// <summary>
		/// 计划发运数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? PLAN_QTY { get; set; }

		/// <summary>
		/// 零件加工记录ID
		/// </summary>
		[JsonProperty]
		public long? PRODUCTION_LOG_ID { get; set; }

		/// <summary>
		/// 发运单FID
		/// </summary>
		[JsonProperty]
		public Guid? SHIPPING_FID { get; set; }

		/// <summary>
		/// 发运ID
		/// </summary>
		[JsonProperty]
		public long? SHIPPING_ID { get; set; }

		[JsonProperty]
		public long? SOURCE_ID { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		[JsonProperty]
		public int? STATUS { get; set; }

		[JsonProperty, Column(StringLength = 8)]
		public string SUPPLY_GROUP { get; set; }

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
		public string VEHICLE_CATEGORY_CODE { get; set; }

	}

}
