using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace ProjectArrow.Entity
{

    [JsonObject(MemberSerialization.OptIn), Table(Name = "MES.ProjectArrow", DisableSyncStructure = true)]
    public partial class MES_ProjectArrow
    {

        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public long ID { get; set; }

        [JsonProperty, Column(StringLength = 16)]
        public string ASSEMBLYLINE { get; set; }

        [JsonProperty]
        public DateTime? CREATE_DATE { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string CREATE_USER { get; set; }

        [JsonProperty]
        public long? EQUIPID { get; set; }

        [JsonProperty]
        public long? EQUIPPARAMID { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string ORDERNUMBER { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string PARAMVALUE { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string PARTNAME { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string PARTNUMBER { get; set; }

        [JsonProperty]
        public Guid? SERIAL_ID { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string STATION { get; set; }

        [JsonProperty]
        public int? STATUS { get; set; }

        [JsonProperty]
        public bool? VALID_FLAG { get; set; }

        [JsonProperty]
        public string MOLDNUMBER { get; set; }

    }

}
