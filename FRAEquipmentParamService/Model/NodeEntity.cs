using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService.Model
{
    public class NodeEntity
    {
        public NodeEntity()
        {
        }

        public NodeEntity(string tagAddress, string flag = "")
        {
            this.TagAddress = tagAddress;
            this.Flag = flag;
        }

        public string TagAddress { get; set; }
        public string Flag { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public object Value { get; set; }
        public DateTime CreateDT { get; set; }

        public override string ToString()
        {
            return TagAddress + "-" + Flag;
        }
    }
}
