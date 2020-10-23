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
	/// 系统_CII异常代号
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TS_SYS_CII_ERROR_CODE", DisableSyncStructure = true)]
	public partial class MES_TS_SYS_CII_ERROR_CODE {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 允许重发
		/// </summary>
		[JsonProperty]
		public int? ALLOW_RESEND { get; set; }

		[JsonProperty]
		public DateTime? CREATE_DATE { get; set; }

		[JsonProperty]
		public DateTime? CREATE_DATE_UTC { get; set; }

		[JsonProperty, Column(StringLength = 62)]
		public string CREATE_USER { get; set; }

		/// <summary>
		/// 错误代号
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string ERROR_CODE { get; set; }

		[JsonProperty]
		public DateTime? MODIFY_DATE { get; set; }

		[JsonProperty]
		public DateTime? MODIFY_DATE_UTC { get; set; }

		[JsonProperty, Column(StringLength = 62)]
		public string MODIFY_USER { get; set; }

		/// <summary>
		/// 工厂代号
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string PLANT { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(StringLength = -2)]
		public string REMARK { get; set; }

		[JsonProperty]
		public bool? VALID_FLAG { get; set; }

	}

}
