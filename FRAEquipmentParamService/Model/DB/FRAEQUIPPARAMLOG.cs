using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace DPToleranceMonitorService.Model.DB
{

    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.FRAEQUIPPARAMLOG", DisableSyncStructure = false)]
	public partial class MES_FRAEQUIPPARAMLOG {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		[JsonProperty]
		public long? ASSEMBLYID { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string CARRIERID { get; set; }

		[JsonProperty]
		public DateTime? CREATEDATETIME { get; set; }

		[JsonProperty, Column(StringLength = 32)]
		public string DESCRIPTION { get; set; }

		[JsonProperty, Column(StringLength = 1000)]
		public string PARAMTAG { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string SEQUENCE { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string STATION { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string VALUE { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string VINCODE { get; set; }

		[JsonProperty, Column(StringLength = 50)]
		public string WORKORDER { get; set; }

	}

}
