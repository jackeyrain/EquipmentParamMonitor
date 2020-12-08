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
    /// 基础数据_原材料容器
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_RAW_PACKAGE", DisableSyncStructure = true)]
    public partial class MES_TM_BAS_RAW_PACKAGE
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
        [JsonProperty, Column(StringLength = 32)]
        public string CREATE_USER { get; set; }

        /// <summary>
        /// FID
        /// </summary>
        [JsonProperty]
        public Guid? FID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string NAME { get; set; }

        /// <summary>
        /// 容器类型
        /// </summary>
        [JsonProperty]
        public int? PACKAGE_TYPE { get; set; }

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

        public List<MES_TM_BAS_RAW_PACKAGE_DETAIL> mES_TM_BAS_RAW_PACKAGE_DETAILs { get; set; }
    }

}
