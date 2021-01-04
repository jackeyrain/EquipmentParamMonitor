using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace PABErrorLogMonitor
{

	/// <summary>
	/// 客户接口_客户排序信息
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TT_CI_CUST_SORT_INFO", DisableSyncStructure = true)]
	public partial class MES_TT_CI_CUST_SORT_INFO {

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string ASSY_CATEGORY { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string ASSY_CODE { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string ASSY_COLOR_CODE { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string ASSY_COLOR_NAME { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string CALLOFF_NO { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string CCR_NO { get; set; }

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
		/// 客户信息序号
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
		/// 客户工厂代码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string CUST_PLANT_CODE { get; set; }

		/// <summary>
		/// 处理状态
		/// </summary>
		[JsonProperty]
		public int? DEAL_STATUS { get; set; }

		[JsonProperty]
		public Guid? FID { get; set; }

		/// <summary>
		/// 客户信息点
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string INFO_POINT_CODE { get; set; }

		/// <summary>
		/// 客户信息时间
		/// </summary>
		[JsonProperty]
		public DateTime? INFO_POINT_TIME { get; set; }

		[JsonProperty]
		public DateTime? INFO_POINT_TIME_UTC { get; set; }

		/// <summary>
		/// 匹配完成时间
		/// </summary>
		[JsonProperty]
		public DateTime? MATCH_TIME { get; set; }

		[JsonProperty]
		public DateTime? MATCH_TIME_UTC { get; set; }

		/// <summary>
		/// 装配工位
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string MOUNTING_POSITION_CODE { get; set; }

		/// <summary>
		/// 零件家族
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PARTS_FAMILY { get; set; }

		/// <summary>
		/// 工厂代码
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

		/// <summary>
		/// 拉动状态
		/// </summary>
		[JsonProperty]
		public int? PULLING_ORDER_STATUS { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string QAD_PART_NO { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? QTY { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(StringLength = -2)]
		public string REMARK { get; set; }

		/// <summary>
		/// 排序逻辑序号
		/// </summary>
		[JsonProperty]
		public long? SEQ { get; set; }

		/// <summary>
		/// 运单状态
		/// </summary>
		[JsonProperty]
		public int? SHIPPING_ORDER_STATUS { get; set; }

		/// <summary>
		/// 信息来源ID
		/// </summary>
		[JsonProperty]
		public long? SOURCE_ID { get; set; }

		/// <summary>
		/// 信息来源类型
		/// </summary>
		[JsonProperty]
		public int? SOURCE_TYPE { get; set; }

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
		public string VEHICLE_CATEGORY { get; set; }

		/// <summary>
		/// 车型大类代码
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string VEHICLE_CLASS_CODE { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string VEHICLE_NAME { get; set; }

		/// <summary>
		/// 车型代码
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string VEHICLE_NO { get; set; }

		/// <summary>
		/// VIN号
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string VIN_CODE { get; set; }

		/// <summary>
		/// 工单状态
		/// </summary>
		[JsonProperty]
		public int? WORK_ORDER_STATUS { get; set; }

	}

}
