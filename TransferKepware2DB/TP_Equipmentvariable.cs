using FreeSql.DatabaseModel;using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace TransferKepware2DB
{

	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TP_Equipmentvariable", DisableSyncStructure = true)]
	public partial class MES_TP_Equipmentvariable {

		[JsonProperty]
		public string Address { get; set; }

		[JsonProperty, Column(Name = "Clamp High")]
		public string Clamp_High { get; set; }

		[JsonProperty, Column(Name = "Clamp Low")]
		public string Clamp_Low { get; set; }

		[JsonProperty, Column(Name = "Client Access")]
		public string Client_Access { get; set; }

		[JsonProperty, Column(Name = "Data Type")]
		public string Data_Type { get; set; }

		[JsonProperty]
		public string Description { get; set; }

		[JsonProperty, Column(Name = "Eng Units")]
		public string Eng_Units { get; set; }

		[JsonProperty, Column(Name = "Negate Value")]
		public string Negate_Value { get; set; }

		[JsonProperty, Column(Name = "Raw High")]
		public string Raw_High { get; set; }

		[JsonProperty, Column(Name = "Raw Low")]
		public string Raw_Low { get; set; }

		[JsonProperty, Column(Name = "Respect Data Type")]
		public double? Respect_Data_Type { get; set; }

		[JsonProperty, Column(Name = "Scaled Data Type")]
		public string Scaled_Data_Type { get; set; }

		[JsonProperty, Column(Name = "Scaled High")]
		public string Scaled_High { get; set; }

		[JsonProperty, Column(Name = "Scaled Low")]
		public string Scaled_Low { get; set; }

		[JsonProperty]
		public string Scaling { get; set; }

		[JsonProperty, Column(Name = "Scan Rate")]
		public double? Scan_Rate { get; set; }

		[JsonProperty, Column(Name = "Tag Name")]
		public string Tag_Name { get; set; }

	}

}
