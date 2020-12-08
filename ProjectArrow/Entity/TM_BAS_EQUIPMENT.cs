using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace ProjectArrow.Entity {

	/// <summary>
	/// 基础数据_设备
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_EQUIPMENT", DisableSyncStructure = true)]
	public partial class MES_TM_BAS_EQUIPMENT {

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true)]
		public long ID { get; set; }

		/// <summary>
		/// 产线
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string ASSEMBLY_LINE { get; set; }

		/// <summary>
		/// 刷卡总线地址
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string CARD_SLOT_ADDRESS { get; set; }

		[JsonProperty]
		public int? CMD_CFG_TYPE { get; set; }

		/// <summary>
		/// 代码
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string CODE { get; set; }

		[JsonProperty]
		public bool? CONTROL_BY_PASS { get; set; }

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
		/// 设备类型
		/// </summary>
		[JsonProperty]
		public int? EQUIP_TYPE { get; set; }

		/// <summary>
		/// FID
		/// </summary>
		[JsonProperty]
		public Guid? FID { get; set; }

		/// <summary>
		/// 组
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string GROUP_NAME { get; set; }

		/// <summary>
		/// IP地址
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string IP { get; set; }

		[JsonProperty]
		public bool? IS_AUTO_SCRAP { get; set; }

		[JsonProperty]
		public bool? IS_MONITOR_DOWNTIME { get; set; }

		[JsonProperty]
		public bool? IS_READ_VARIABLE_WHEN_FINISHED { get; set; }

		/// <summary>
		/// KEPWARE驱动
		/// </summary>
		[JsonProperty, Column(StringLength = 512)]
		public string KEPWARE_DRIVER { get; set; }

		/// <summary>
		/// KEPWARE标示
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string KEPWARE_FLAG { get; set; }

		/// <summary>
		/// 工位
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string LOCATION { get; set; }

		/// <summary>
		/// 主设备ID
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string MAIN_EQUIPMENT { get; set; }

		/// <summary>
		/// 设备型号
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string MODEL_NO { get; set; }

		[JsonProperty]
		public int? MOLD_CHANGE_TIME_OUT { get; set; }

		[JsonProperty]
		public int? MONITOR_DOWNTIME_DATE { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string NAME { get; set; }

		[JsonProperty]
		public bool? NEED_VERIFY_PROCESS_RESULT { get; set; }

		/// <summary>
		/// 工厂
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

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

		[JsonProperty, Column(DbType = "varchar(8)")]
		public string PORT { get; set; }

		/// <summary>
		/// 质量
		/// </summary>
		[JsonProperty]
		public int? QUALITY { get; set; }

		/// <summary>
		/// 读条码次数
		/// </summary>
		[JsonProperty]
		public int? READ_BARCODE_TIMES { get; set; }

		/// <summary>
		/// SAP代码
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string SAP_CODE { get; set; }

		/// <summary>
		/// 设备加工超时时间
		/// </summary>
		[JsonProperty]
		public int? TIMEOUT_PRODUCT { get; set; }

		/// <summary>
		/// 数据请求超时时间
		/// </summary>
		[JsonProperty]
		public int? TIMEOUT_REQUEST { get; set; }

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

		/// <summary>
		/// 变量配置类型
		/// </summary>
		[JsonProperty, Column(StringLength = 36)]
		public string VARIABLE_CFG_TYPE { get; set; }

	}

}
