using DPToleranceMonitorService.Enum;
using Newtonsoft.Json;

namespace DPToleranceMonitorService.Model
{
    public class ToleranceEntity
    {
        public ToleranceEntity()
        {

        }
        public ToleranceEntity(string name, string tagaddress, decimal min, decimal max, dynamic accurate = null)
        {
            Name = name;
            TagAddress = tagaddress;
            Min = min;
            Max = max;
            Accurate = accurate;
        }

        public string Name { get; set; }
        public string TagAddress { get; set; }
        [JsonIgnore]
        public dynamic Value { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public dynamic Accurate { get; set; }
        /// <summary>
        /// 公差范围校验
        /// </summary>
        /// <returns></returns>
        public bool ToleranceRangeCheck()
        {
            decimal.TryParse(Value?.ToString(), out decimal _value);
            if (_value >= Min && _value <= Max) return true;
            else return false;

        }
        /// <summary>
        /// 公差精确校验
        /// </summary>
        /// <returns></returns>
        public bool ToleranceAccurateCheck()
        {
            if (Value.Equals(Accurate)) return true;
            else return false;
        }
        /// <summary>
        /// 判断是否有赋值
        /// </summary>
        /// <returns></returns>
        public bool HasValue()
        {
            return Value != null;
        }
    }
}
