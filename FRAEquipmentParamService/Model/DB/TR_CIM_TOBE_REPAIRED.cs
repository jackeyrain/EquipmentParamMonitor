using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace DPToleranceMonitorService.Model.DB
{

    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TR_CIM_TOBE_REPAIRED", DisableSyncStructure = true)]
    public partial class MES_TR_CIM_TOBE_REPAIRED
    {

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }

        /// <summary>
        /// 添加者标记
        /// </summary>
        [JsonProperty]
        public bool? ADD_BY_INSPECTOR { get; set; }

        [JsonProperty]
        public long? Assembly_ID { get; set; }

        /// <summary>
        /// 产线
        /// </summary>
        [JsonProperty, Column(StringLength = 16)]
        public string ASSEMBLY_LINE { get; set; }

        [JsonProperty, Column(StringLength = 100)]
        public string AssemblyBarcode { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [JsonProperty, Column(StringLength = 10)]
        public string CLASSIFICATION { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public DateTime CREATE_DATE { get; set; }

        [JsonProperty]
        public DateTime? CREATE_DATE_UTC { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [JsonProperty, Column(StringLength = 32, IsNullable = false)]
        public string CREATE_USER { get; set; }

        [JsonProperty]
        public int DATA_SOURCE { get; set; } = 1;

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty, Column(DbType = "varchar(1000)")]
        public string DESCRIPTION { get; set; }

        /// <summary>
        /// FID
        /// </summary>
        [JsonProperty]
        public Guid? FID { get; set; }

        /// <summary>
        /// 是否判定过
        /// </summary>
        [JsonProperty]
        public bool? INSPECTED { get; set; }

        [JsonProperty, Column(StringLength = 100)]
        public string INSPECTION_TYPE { get; set; }

        /// <summary>
        /// 工位
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string LOCATION { get; set; }

        /// <summary>
        /// 加工单号
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string ORDER_CODE { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        [JsonProperty, Column(StringLength = 128)]
        public string PART_NAME { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        [JsonProperty, Column(StringLength = 32)]
        public string PART_NO { get; set; }

        [JsonProperty, Column(StringLength = 16)]
        public string PART_TYPE_CODE { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [JsonProperty, Column(StringLength = 10)]
        public string POSITION { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty, Column(DbType = "varchar(1000)")]
        public string REMARK { get; set; }

        [JsonProperty]
        public int REPAIRE_TYPE { get; set; } = 0;

        /// <summary>
        /// 责任
        /// </summary>
        [JsonProperty, Column(StringLength = 10)]
        public string RESPONSIBILITY { get; set; }

        /// <summary>
        /// 判断结果
        /// </summary>
        [JsonProperty, Column(StringLength = 5)]
        public string RESULT { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty]
        public DateTime? UPDATE_DATE { get; set; }

        [JsonProperty, Column(InsertValueSql = "")]
        public DateTime? UPDATE_DATE_UTC { get; set; }

        /// <summary>
        /// 更新用户
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
