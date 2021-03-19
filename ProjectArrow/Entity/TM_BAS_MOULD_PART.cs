using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace ProjectArrow.Entity
{

    /// <summary>
    /// 基础数据_一模多腔
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_MOULD_PART", DisableSyncStructure = true)]
    public partial class MES_TM_BAS_MOULD_PART
    {

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }

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
        [JsonProperty, Column(StringLength = 32, IsNullable = false)]
        public string CREATE_USER { get; set; }

        /// <summary>
        /// 设备模具代码
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string EQUIP_MOULD_NO { get; set; }

        /// <summary>
        /// 设备FID
        /// </summary>
        [JsonProperty]
        public Guid? EQUIPMENT_FID { get; set; }

        /// <summary>
        /// FID
        /// </summary>
        [JsonProperty]
        public Guid? FID { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string PART_NO { get; set; }

        /// <summary>
        /// 工厂代码
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
        /// 有效标记
        /// </summary>
        [JsonProperty]
        public bool? VALID_FLAG { get; set; }

    }

}
