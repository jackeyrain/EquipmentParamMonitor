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

	/// <summary>
	/// EP 零件信息
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TI_CIM_FCA_MPAB_EP_PART", DisableSyncStructure = true)]
	public partial class MES_TI_CIM_FCA_MPAB_EP_PART {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public int ID { get; set; }

		[JsonProperty]
		public int EP_ID { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string PART_NO { get; set; }

		[JsonProperty]
		public int QUANTITY { get; set; }

	}

}
