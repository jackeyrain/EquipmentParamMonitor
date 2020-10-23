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

	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TI_CIM_USN_OPCS", DisableSyncStructure = true)]
	public partial class MES_TI_CIM_USN_OPCS {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public int ID { get; set; }

		[JsonProperty]
		public long? CARSEQUENCE { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string CUSTCODE { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string FILENAME { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string HEADER { get; set; }

		[JsonProperty]
		public DateTime? INSERTDATETIME { get; set; }

		[JsonProperty]
		public DateTime? INSERTDATETIME_UTC { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string MAKERTYPE { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string MODELYEAR { get; set; }

		[JsonProperty]
		public DateTime? MSGDATETIME { get; set; }

		[JsonProperty]
		public DateTime? MSGDATETIME_UTC { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string MVON { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string PAYSTAT { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string PLANTCODE { get; set; }

		[JsonProperty]
		public int? STATUS { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string SUPPCODE { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string VIN { get; set; }

	}

}
