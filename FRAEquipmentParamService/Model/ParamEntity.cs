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
    }
}
