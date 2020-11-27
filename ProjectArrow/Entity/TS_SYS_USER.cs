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
	/// 系统_用户
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TS_SYS_USER", DisableSyncStructure = true)]
	public partial class MES_TS_SYS_USER {

		/// <summary>
		/// 用户ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public int USER_ID { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(StringLength = 256)]
		public string COMMENTS { get; set; }

		/// <summary>
		/// 公司
		/// </summary>
		[JsonProperty, Column(StringLength = 100)]
		public string COMPANY { get; set; }

		[JsonProperty]
		public DateTime CREATE_DATE { get; set; }

		[JsonProperty]
		public DateTime? CREATE_DATE_UTC { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string CREATE_USER { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string DEPARTMENT { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string EMAIL { get; set; }

		/// <summary>
		/// 员工名
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string EMPLOYEE_NAME { get; set; }

		[JsonProperty]
		public int FAIL_LOGIN { get; set; }

		[JsonProperty]
		public Guid? FID { get; set; }

		[JsonProperty]
		public DateTime? LAST_LOGIN_TIME { get; set; }

		[JsonProperty]
		public DateTime? LAST_LOGIN_TIME_UTC { get; set; }

		/// <summary>
		/// 手机号
		/// </summary>
		[JsonProperty, Column(StringLength = 100)]
		public string MOBILE { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string OFFICE_PHONE { get; set; }

		/// <summary>
		/// 密码过期时间
		/// </summary>
		[JsonProperty]
		public DateTime? PASSWORD_EXPIRE_TIME { get; set; }

		[JsonProperty]
		public DateTime? PASSWORD_EXPIRE_TIME_UTC { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string PASSWORD1 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string PASSWORD2 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string PASSWORD3 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string PASSWORD4 { get; set; }

		[JsonProperty, Column(StringLength = 100)]
		public string PASSWORD5 { get; set; }

		/// <summary>
		/// 班次号
		/// </summary>
		[JsonProperty]
		public int? SHIFT { get; set; }

		[JsonProperty]
		public DateTime? UPDATE_DATE { get; set; }

		[JsonProperty]
		public DateTime? UPDATE_DATE_UTC { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string UPDATE_USER { get; set; }

		/// <summary>
		/// 登录用户名
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string USER_LOGIN_NAME { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string USER_PASSWORD { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		[JsonProperty]
		public int USER_STATUS { get; set; }

		/// <summary>
		/// 类别
		/// </summary>
		[JsonProperty]
		public int USER_TYPE { get; set; }

	}

}
