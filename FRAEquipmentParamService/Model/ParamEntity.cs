using System;
using System.Collections.Generic;
using System.Linq;

namespace FRAEquipmentParamService.Model
{
    public class ParamEntity
    {
        public ParamEntity()
        {

        }
        public ParamEntity(string name, List<NodeEntity> tagaddress)
        {
            Name = name;
            TagAddress = tagaddress;
        }

        public string Name { get; set; }
        public List<NodeEntity> TagAddress { get; set; }
        /// <summary>
        /// 结果判断
        /// </summary>
        /// <returns></returns>
        public bool ResultCheck()
        {
            var _resultTag = TagAddress.FirstOrDefault(o => o.Flag.Equals("result", System.StringComparison.OrdinalIgnoreCase));
            var _value = Convert.ToInt32(_resultTag?.Value);
            // 如果结果为2则为失败
            if (_value == 2)
                return false;

            return true;
        }
        /// <summary>
        /// 判断是否为空值
        /// </summary>
        /// <returns></returns>
        public bool IsEmtpy()
        {
            var _resultTag = TagAddress.FirstOrDefault(o => o.Flag.Equals("result", System.StringComparison.OrdinalIgnoreCase));
            var _value = Convert.ToString(_resultTag?.Value);
            // 如果为空或者0则为空
            if (_value.Equals("0") || string.IsNullOrEmpty(_value))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Remark
        {
            get
            {
                var tags = TagAddress.Where(o => !string.IsNullOrEmpty(o.Value.ToString())).ToList();
                var remark = string.Join(", ",
                    tags.Select(o => $"{o.TagAddress.Substring(o.TagAddress.LastIndexOf(".") + 1, o.TagAddress.Length - o.TagAddress.LastIndexOf(".") - 1)}:[{o.Value.ToString()}]"));
                return remark;
            }
        }
    }
}
