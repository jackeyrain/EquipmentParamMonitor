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
    /// 基础数据_零件
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_PART", DisableSyncStructure = true)]
    public partial class MES_TM_BAS_PART
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
        /// 数据来源
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string DATA_SOURCE { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string DEFAULT_BOM_CODE { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string DEFAULT_LOCATION { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string DEFAULT_ROUTING { get; set; }

        /// <summary>
        /// 数据生效时间
        /// </summary>
        [JsonProperty]
        public DateTime? EFFECTIVE_TIME { get; set; }

        [JsonProperty]
        public DateTime? EFFECTIVE_TIME_UTC { get; set; }

        /// <summary>
        /// 零件状态
        /// </summary>
        [JsonProperty, Column(StringLength = 8)]
        public string ITEM_STATUS { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
        [JsonProperty, Column(DbType = "decimal(18,4)")]
        public decimal? NET_WEIGHT { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        [JsonProperty, Column(StringLength = 128)]
        public string PART_NAME { get; set; }

        /// <summary>
        /// 零件英文名称
        /// </summary>
        [JsonProperty, Column(StringLength = 128)]
        public string PART_NAME_EN { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string PART_NO { get; set; }

        /// <summary>
        /// 虚零件标记
        /// </summary>
        [JsonProperty]
        public bool? PHANTOM_FLAG { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        [JsonProperty, Column(StringLength = 8)]
        public string PLANT { get; set; }

        /// <summary>
        /// 制造|采购
        /// </summary>
        [JsonProperty, Column(StringLength = 8)]
        public string PM_CODE { get; set; }

        /// <summary>
        /// 采购类别
        /// </summary>
        [JsonProperty, Column(StringLength = 64)]
        public string PURCHASING_TYPE { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [JsonProperty, Column(StringLength = 16)]
        public string UOM { get; set; }

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

        /// <summary>
        /// 重量单位
        /// </summary>
        [JsonProperty, Column(StringLength = 16)]
        public string WEIGHT_UOM { get; set; }

    }

}
