using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace WSDataTransferSuper
{

	/// <summary>
	/// mpab SP零件表
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TI_CIM_FCA_MPAB_SP_PART", DisableSyncStructure = true)]
	public partial class MES_TI_CIM_FCA_MPAB_SP_PART {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public int ID { get; set; }

		/// <summary>
		/// 关联主表
		/// </summary>
		[JsonProperty]
		public int SP_ID { get; set; }

		/// <summary>
		/// 零件号
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(50)")]
		public string PART_NO { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string QAD_PART_NO { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		[JsonProperty]
		public int QUANTITY { get; set; }

	}

}
