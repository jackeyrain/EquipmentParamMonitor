using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace DPToleranceMonitorService.Model.DB
{

    /// <summary>
    /// 基础数据_设备数据定义
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TM_BAS_EQUIPMENT_VARIABLE", DisableSyncStructure = true)]
    public partial class MES_TM_BAS_EQUIPMENT_VARIABLE
    {

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }

        [JsonProperty, Column(StringLength = 36)]
        public string CATEGORY_LEVEL_TWO { get; set; }

        /// <summary>
        /// OPC注册ID
        /// </summary>
        [JsonProperty]
        public int? CLIENT_HANDLE { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [JsonProperty, Column(StringLength = 512)]
        public string CODE { get; set; }

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
        /// 数据类型
        /// </summary>
        [JsonProperty]
        public int? DATA_TYPE { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty, Column(StringLength = 512)]
        public string DESCRIPTION { get; set; }

        /// <summary>
        /// 设备FID
        /// </summary>
        [JsonProperty]
        public Guid? EQUIP_FID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [JsonProperty]
        public long? EQUIP_ID { get; set; }

        /// <summary>
        /// 设备参数匹配代码
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string EQUIP_PARAM_CODE { get; set; }

        [JsonProperty]
        public Guid? FID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty, Column(StringLength = 128)]
        public string NAME { get; set; }

        /// <summary>
        /// 读写类型
        /// </summary>
        [JsonProperty]
        public int? READ_WRITE { get; set; }

        /// <summary>
        /// 轮询间隔
        /// </summary>
        [JsonProperty]
        public int? SCAN_INTERVAL { get; set; }

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

        /// <summary>
        /// 变量类型
        /// </summary>
        [JsonProperty]
        public int? VARIABLE_TYPE { get; set; }

    }

}
