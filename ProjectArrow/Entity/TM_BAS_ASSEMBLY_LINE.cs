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
	/// 基础数据_产线
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_ASSEMBLY_LINE", DisableSyncStructure = true)]
	public partial class MES_TM_BAS_ASSEMBLY_LINE {

		/// <summary>
		/// 流水线
		/// </summary>
		[JsonProperty, Column(StringLength = 16, IsPrimary = true)]
		public string ASSEMBLY_LINE { get; set; }

		/// <summary>
		/// COMMON_地址
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string ADDRESS { get; set; }

		/// <summary>
		/// 流水线名称
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string ASSEMBLY_LINE_NAME { get; set; }

		/// <summary>
		/// 流水线缩写
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string ASSEMBLY_LINE_NICKNAME { get; set; }

		/// <summary>
		/// 流水线节拍
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string ASSEMBLY_LINE_PULSE { get; set; }

		[JsonProperty]
		public int? ASSEMBLY_LINE_SEQ_TYPE { get; set; }

		/// <summary>
		/// 流水线类型
		/// </summary>
		[JsonProperty]
		public int? ASSEMBLY_LINE_TYPE { get; set; }

		/// <summary>
		/// Barcode模板
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string BARCODE_PRINT_TEMPLATE { get; set; }

		/// <summary>
		/// 条码打印机
		/// </summary>
		[JsonProperty, Column(StringLength = 512)]
		public string BARCODE_PRINTER { get; set; }

		/// <summary>
		/// 中文协调员
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string CMANAGER { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
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
		[JsonProperty, Column(StringLength = 64)]
		public string CREATE_USER { get; set; }

		/// <summary>
		/// 英文协调员
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string EMANAGER { get; set; }

		/// <summary>
		/// 设备code
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string EQUIPMENTCODE { get; set; }

		/// <summary>
		/// FIS流水线编码
		/// </summary>
		[JsonProperty, Column(StringLength = 16)]
		public string FIS_LINE_CODE { get; set; }

		/// <summary>
		/// JPH
		/// </summary>
		[JsonProperty]
		public int? JPH { get; set; }

		[JsonProperty, Column(StringLength = 64)]
		public string MAXIMO_EQUIPMENT_CODE { get; set; }

		/// <summary>
		/// 工厂
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

		/// <summary>
		/// 厂区
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT_ZONE { get; set; }

		/// <summary>
		/// 打印机名称
		/// </summary>
		[JsonProperty, Column(StringLength = 100)]
		public string PRINTERNAME { get; set; }

		/// <summary>
		/// 拉动创建工单状态
		/// </summary>
		[JsonProperty]
		public int? PULLING_CREATE_WORKORDER_STATUS { get; set; }

		/// <summary>
		/// SAP流水线
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string SAP_ASSEMBLY_LINE { get; set; }

		/// <summary>
		/// 是否参照工作日历
		/// </summary>
		[JsonProperty]
		public int TIME_REFERENCE_TYPE { get; set; }

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
		[JsonProperty, Column(StringLength = 64)]
		public string UPDATE_USER { get; set; }

		/// <summary>
		/// 是否有效
		/// </summary>
		[JsonProperty]
		public bool? VALID_FLAG { get; set; }

		/// <summary>
		/// 时间调度类型
		/// </summary>
		[JsonProperty]
		public int WORK_SCHEDULE_TYPE { get; set; }

		/// <summary>
		/// 车间
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string WORKSHOP { get; set; }

	}

}
