using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace ShippingEmergency
{

    /// <summary>
    /// 仓储_发运单
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TT_WM_SHIPPING", DisableSyncStructure = true)]
    public partial class MES_TT_WM_SHIPPING
    {

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string CHECK_CRAFT { get; set; } = "NULL";

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
        /// 客户代码
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string CUST_CODE { get; set; }

        /// <summary>
        /// 客户工厂代码
        /// </summary>
        [JsonProperty, Column(StringLength = 16)]
        public string CUST_PLANT_CODE { get; set; }

        /// <summary>
        /// 结束扫描时间
        /// </summary>
        [JsonProperty]
        public DateTime? END_SCAN_TIME { get; set; }

        [JsonProperty]
        public DateTime? END_SCAN_TIME_UTC { get; set; }

        /// <summary>
        /// FID
        /// </summary>
        [JsonProperty]
        public Guid? FID { get; set; }

        [JsonProperty]
        public bool? FORCE_FLAG { get; set; }

        [JsonProperty]
        public int? IS_GENERATE_INTERFACE_DATA { get; set; }

        /// <summary>
        /// 最后打印时间
        /// </summary>
        [JsonProperty]
        public DateTime? LAST_PRINT_TIME { get; set; }

        [JsonProperty]
        public DateTime? LAST_PRINT_TIME_UTC { get; set; }

        /// <summary>
        /// 最后打印用户
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string LAST_PRINT_USER { get; set; }

        /// <summary>
        /// 发运组FID
        /// </summary>
        [JsonProperty]
        public Guid? PART_SHIPPING_FID { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        [JsonProperty, Column(StringLength = 8)]
        public string PLANT { get; set; }

        /// <summary>
        /// 累计打印次数
        /// </summary>
        [JsonProperty]
        public int? PRINT_COUNT { get; set; }

        /// <summary>
        /// 发布标记
        /// </summary>
        [JsonProperty]
        public bool? PUBLISH_FLAG { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [JsonProperty]
        public DateTime? PUBLISH_TIME { get; set; }

        [JsonProperty]
        public DateTime? PUBLISH_TIME_UTC { get; set; }

        [JsonProperty]
        public bool? RE_PRINT { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty, Column(StringLength = -2)]
        public string REMARK { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string ROAD_PROCESS { get; set; } = "";

        /// <summary>
        /// 序号
        /// </summary>
        [JsonProperty]
        public long? SEQ { get; set; }

        /// <summary>
        /// 发运单号
        /// </summary>
        [JsonProperty, Column(StringLength = 64)]
        public string SHIPPING_CODE { get; set; }

        /// <summary>
        /// 发运单类型
        /// </summary>
        [JsonProperty]
        public int? SHIPPING_TYPE { get; set; }

        /// <summary>
        /// 开始扫描时间
        /// </summary>
        [JsonProperty]
        public DateTime? START_SCAN_TIME { get; set; }

        [JsonProperty]
        public DateTime? START_SCAN_TIME_UTC { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty]
        public int? STATUS { get; set; }

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

        public List<MES_TT_WM_SHIPPING_DETAIL> mES_TT_WM_SHIPPING_DETAILs { get; set; }

        [JsonIgnore, Column(IsIgnore = true)]
        public string FromSeq { get; set; }
        [JsonIgnore, Column(IsIgnore = true)]
        public string ToSeq { get; set; }

        [JsonIgnore, Column(IsIgnore = true)]
        public int VehicleCount { get; set; }

        [JsonIgnore, Column(IsIgnore = true)]
        public int ItemCount { get; set; }
    }

}
