using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace EPSendToQAD
{

    /// <summary>
    /// 结算信息
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.TI_CIM_FCA_MPAB_EP", DisableSyncStructure = true)]
    public partial class MES_TI_CIM_FCA_MPAB_EP
    {

        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 报文传过来的字段
        /// </summary>
        [JsonProperty]
        public int CREATE_DATE { get; set; }

        [JsonProperty, Column(InsertValueSql = "getdate()")]
        public DateTime CREATE_TIME { get; set; }

        [JsonProperty, Column(DbType = "varchar(50)")]
        public string CREATE_USER { get; set; }

        /// <summary>
        /// 发票信息
        /// </summary>
        [JsonProperty, Column(DbType = "varchar(50)")]
        public string INVOICE { get; set; }

        /// <summary>
        /// EP
        /// </summary>
        [JsonProperty, Column(DbType = "char(2)")]
        public string MESSAGE_TYPE { get; set; }

        [JsonProperty]
        public int OF_PART_NUMBERS { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        [JsonProperty, Column(DbType = "char(1)")]
        public string PLANT_CODE { get; set; }

        [JsonProperty]
        public int PRODUCTION_DATE { get; set; }

        [JsonProperty]
        public int RUN_NUMBER { get; set; }

        [JsonProperty]
        public int SEQUENCE_NUMBER { get; set; }

        [JsonProperty]
        public int? STATUS { get; set; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        [JsonProperty, Column(DbType = "varchar(50)")]
        public string SUPPLIER_CODE { get; set; }

        [JsonProperty, Column(DbType = "varchar(50)")]
        public string VIN { get; set; }

        public List<MES_TI_CIM_FCA_MPAB_EP_PART> mES_TI_CIM_FCA_MPAB_EP_PARTs { get; set; }
    }

}
