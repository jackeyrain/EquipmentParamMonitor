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
	/// 生产控制_加料
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TT_PC_RAW_PART_CHARGING", DisableSyncStructure = true)]
	public partial class MES_TT_PC_RAW_PART_CHARGING {

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 批号
		/// </summary>
		[JsonProperty, Column(StringLength = 128)]
		public string BATCH_NO { get; set; }

		/// <summary>
		/// COMMON_CREATE_DATE
		/// </summary>
		[JsonProperty]
		public DateTime CREATE_DATE { get; set; }

		[JsonProperty]
		public DateTime? CREATE_DATE_UTC { get; set; }

		/// <summary>
		/// COMMON_CREATE_USER
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string CREATE_USER { get; set; }

		/// <summary>
		/// FID
		/// </summary>
		[JsonProperty]
		public Guid? FID { get; set; }

		/// <summary>
		/// 加料时间
		/// </summary>
		[JsonProperty]
		public DateTime? LOAD_PART_TIME { get; set; }

		[JsonProperty]
		public DateTime? LOAD_PART_TIME_UTC { get; set; }

		/// <summary>
		/// 容器条码
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string PACKAGE_BARCODE { get; set; }

		/// <summary>
		/// PACKAGE_DETAIL_FID
		/// </summary>
		[JsonProperty]
		public Guid? PACKAGE_DETAIL_FID { get; set; }

		/// <summary>
		/// 包装FID
		/// </summary>
		[JsonProperty]
		public Guid? PACKAGE_FID { get; set; }

		/// <summary>
		/// 包装名称
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string PACKAGE_NAME { get; set; }

		/// <summary>
		/// PACKAGE_PART_FID
		/// </summary>
		[JsonProperty]
		public Guid? PACKAGE_PART_FID { get; set; }

		/// <summary>
		/// 父包装条码
		/// </summary>
		[JsonProperty, Column(DbType = "varchar(255)")]
		public string PARENT_PACKAGE_BARCODE { get; set; }

		/// <summary>
		/// 父包装明细FID
		/// </summary>
		[JsonProperty]
		public Guid? PARENT_PACKAGE_DETAIL_FID { get; set; }

		/// <summary>
		/// 材料条码
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string PART_BARCODE { get; set; }

		/// <summary>
		/// 材料零件号
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string PART_NO { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		[JsonProperty, Column(DbType = "decimal(18,4)")]
		public decimal? PART_QTY { get; set; }

		/// <summary>
		/// 计量单位
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PART_UOM { get; set; }

		/// <summary>
		/// 工厂编号
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

		/// <summary>
		/// COMMON_UPDATE_DATE
		/// </summary>
		[JsonProperty]
		public DateTime? UPDATE_DATE { get; set; }

		[JsonProperty]
		public DateTime? UPDATE_DATE_UTC { get; set; }

		/// <summary>
		/// COMMON_UPDATE_USER
		/// </summary>
		[JsonProperty, Column(StringLength = 32)]
		public string UPDATE_USER { get; set; }

		/// <summary>
		/// 是否有效
		/// </summary>
		[JsonProperty]
		public bool? VALID_FLAG { get; set; }

	}

}
