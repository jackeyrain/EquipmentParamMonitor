using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace EPSendToQAD
{

	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TL_SYS_QAD_OUTBOUND_LOG", DisableSyncStructure = true)]
	public partial class MES_TL_SYS_QAD_OUTBOUND_LOG {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty]
		public DateTime? CREATE_TIME { get; set; }

		[JsonProperty]
		public DateTime? CREATE_TIME_UTC { get; set; }

		/// <summary>
		/// 创建人
		/// </summary>
		[JsonProperty, Column(StringLength = 50)]
		public string CREATOR { get; set; }

		/// <summary>
		/// 数据接口配置FID
		/// </summary>
		[JsonProperty]
		public Guid? DATA_INTERFACE_FID { get; set; }

		/// <summary>
		/// 错误代号
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string ERROR_CODE { get; set; }

		/// <summary>
		/// 错误描述
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(MAX)")]
		public string ERROR_DESCRIPTION { get; set; }

		/// <summary>
		/// 结束执行时间
		/// </summary>
		[JsonProperty]
		public DateTime? EXECUTE_END_TIME { get; set; }

		[JsonProperty]
		public DateTime? EXECUTE_END_TIME_UTC { get; set; }

		/// <summary>
		/// 执行结果
		/// </summary>
		[JsonProperty]
		public int? EXECUTE_RESULT { get; set; }

		/// <summary>
		/// 执行开始时间
		/// </summary>
		[JsonProperty]
		public DateTime? EXECUTE_START_TIME { get; set; }

		[JsonProperty]
		public DateTime? EXECUTE_START_TIME_UTC { get; set; }

		/// <summary>
		/// 执行次数
		/// </summary>
		[JsonProperty]
		public int? EXECUTE_TIMES { get; set; }

		[JsonProperty]
		public Guid? FID { get; set; }

		/// <summary>
		/// 接口数据FID
		/// </summary>
		[JsonProperty]
		public Guid? HANDLE_FID { get; set; }

		/// <summary>
		/// 关键字
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(256)")]
		public string KEY_VALUE { get; set; }

		/// <summary>
		/// 方法名称
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string METHORD_NAME { get; set; }

		/// <summary>
		/// 最后修改人
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(16)")]
		public string MODIFIER { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		[JsonProperty]
		public DateTime? MODIFY_TIME { get; set; }

		[JsonProperty]
		public DateTime? MODIFY_TIME_UTC { get; set; }

		/// <summary>
		/// 来源XML
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(MAX)")]
		public string SOURCE_XML { get; set; }

		/// <summary>
		/// 同步ID
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string TRANS_ID { get; set; }

		[JsonProperty]
		public bool? VALID_FLAG { get; set; }

	}

}
