using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace DPToleranceMonitorService.Model.DB
{

    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TP_FRA_Pallet", DisableSyncStructure = false)]
	public partial class MES_TP_FRA_Pallet {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		[JsonProperty]
		public DateTime? CreateDT { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string CreateUser { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string LineCode { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string PalletID { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string Sequence { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string Vin { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string WorkOrder { get; set; }

	}

}
