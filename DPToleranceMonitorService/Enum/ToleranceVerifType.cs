using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DPToleranceMonitorService.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ToleranceVerifType : byte
    {
        /// <summary>
        /// 无须公差校验
        /// </summary>
        NONEED = 0,
        /// <summary>
        /// 范围比较
        /// </summary>
        RANGE = 1,
        /// <summary>
        /// 精确比较
        /// </summary>
        ACCURATE = 2,
        /// <summary>
        /// 是否存在
        /// </summary>
        HASVALUE = 3,
    }
}
