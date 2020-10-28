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
	/// 系统_SAP_错误代码
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TS_SYS_SAP_ERROR_CODE", DisableSyncStructure = true)]
	public partial class MES_TS_SYS_SAP_ERROR_CODE {

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 允许重发
		/// </summary>
		[JsonProperty]
		public bool? ALLOW_RESEND { get; set; }

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
		/// 错误代码
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string ERROR_CODE { get; set; }

		/// <summary>
		/// 工厂代码
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(StringLength = 512)]
		public string REMARK { get; set; }

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

	}

}
