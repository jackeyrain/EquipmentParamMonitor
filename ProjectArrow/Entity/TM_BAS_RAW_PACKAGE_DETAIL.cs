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
	/// 基础数据_原材料容器_实体
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_RAW_PACKAGE_DETAIL", DisableSyncStructure = true)]
	public partial class MES_TM_BAS_RAW_PACKAGE_DETAIL {

		/// <summary>
		/// ID
		/// </summary>
		[JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
		public long ID { get; set; }

		/// <summary>
		/// 容器条码
		/// </summary>
		[JsonProperty, Column(StringLength = 64)]
		public string BARCODE { get; set; }

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
		/// 设备FID
		/// </summary>
		[JsonProperty]
		public Guid? EQUIP_FID { get; set; }

		/// <summary>
		/// FID
		/// </summary>
		[JsonProperty]
		public Guid? FID { get; set; }

		/// <summary>
		/// PACKAGE_FID
		/// </summary>
		[JsonProperty]
		public Guid? PACKAGE_FID { get; set; }

		/// <summary>
		/// 工厂编号
		/// </summary>
		[JsonProperty, Column(StringLength = 8)]
		public string PLANT { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[JsonProperty, Column(StringLength = 512)]
		public string REMARK { get; set; }

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
