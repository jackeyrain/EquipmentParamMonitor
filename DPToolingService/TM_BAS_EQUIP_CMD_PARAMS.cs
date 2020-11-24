using FreeSql.DatabaseModel;using System;
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
	/// 设备发送指令规则
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_EQUIP_CMD_PARAMS", DisableSyncStructure = true)]
	public partial class MES_TM_BAS_EQUIP_CMD_PARAMS {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		[JsonProperty]
		public DateTime? CREATE_DATE { get; set; }

		[JsonProperty]
		public DateTime? CREATE_DATE_UTC { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string CREATE_USER { get; set; }

		/// <summary>
		/// 设备FID
		/// </summary>
		[JsonProperty]
		public Guid? EQUIP_FID { get; set; }

		[JsonProperty]
		public Guid? EQUIP_VARIABLE_FID { get; set; }

		[JsonProperty]
		public Guid? FID { get; set; }

		[JsonProperty]
		public DateTime? MODIFY_DATE { get; set; }

		[JsonProperty]
		public DateTime? MODIFY_DATE_UTC { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string MODIFY_USER { get; set; }

		/// <summary>
		/// 发送值
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string SEND_VALUE { get; set; }

		/// <summary>
		/// 来源FID
		/// </summary>
		[JsonProperty]
		public Guid? SOURCE_FID { get; set; }

		/// <summary>
		/// 来源类别
		/// </summary>
		[JsonProperty]
		public int? SOURCE_TYPE { get; set; }

		[JsonProperty]
		public bool? VALID_FLAG { get; set; }

	}

}
