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
	/// 北美EDI数据表
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TI_CIM_FCA_MPAB_SP", DisableSyncStructure = true)]
	public partial class MES_TI_CIM_FCA_MPAB_SP {

		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public int ID { get; set; }

		[JsonProperty]
		public bool CREATE_CUST_SORT_INFO { get; set; } = false;

		[JsonProperty]
		public DateTime? CREATE_CUST_SORT_TIME { get; set; }

		/// <summary>
		/// 数据写入时间
		/// </summary>
		[JsonProperty, Column(InsertValueSql = "getdate()")]
		public DateTime CREATE_TIME { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string CREATE_USER { get; set; }

		[JsonProperty]
		public bool CREATE_WORK_ORDER { get; set; } = false;

		[JsonProperty]
		public DateTime? CREATE_WORK_ORDER_TIME { get; set; }

		[JsonProperty]
		public Guid FID { get; set; }

		/// <summary>
		///  0 正常 10 Overrite 20  duplicate 30 SKIP 40 auto skip
		/// </summary>
		[JsonProperty]
		public int INTERVENTION { get; set; } = 0;

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string INTERVENTION_DESC { get; set; } = "-";

		/// <summary>
		/// 消息类型 SP EP等等
		/// </summary>
		[JsonProperty, Column(DbType = "char(2)")]
		public string MESSAGE_TYPE { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string MODEL_CODE { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string MODULE_TYPE { get; set; }

		[JsonProperty]
		public int OF_PART_NUMBERS { get; set; }

		/// <summary>
		/// 消息到达或传送时间
		/// </summary>
		[JsonProperty]
		public int OF_SALES_CODES { get; set; }

		/// <summary>
		/// Plant Code from the VIN
		/// </summary>
		[JsonProperty, Column(DbType = "char(1)")]
		public string PLANT_CODE { get; set; }

		[JsonProperty]
		public long SEQUENCE_NUMBER { get; set; }

		[JsonProperty]
		public int SEQUENCE_YEAR { get; set; }

		/// <summary>
		/// 状态 10 正常 20 Overrite 30 SKIP 40 duplicate
		/// </summary>
		[JsonProperty]
		public int STATUS { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string STATUS_CODE { get; set; }

		[JsonProperty, Column(DbType = "varchar(500)")]
		public string STATUS_DESC { get; set; }

		/// <summary>
		/// Supplier Code – last
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(7)")]
		public string SUPPLIER_CODE { get; set; }

		[JsonProperty, Column(DbType = "char(1)")]
		public string TRANSMISSION_TYPE { get; set; }

		/// <summary>
		/// Last 8 of VIN
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(8)")]
		public string VIN { get; set; }

		[JsonProperty, Column(DbType = "varchar(50)")]
		public string VIN17 { get; set; }

	}

}
