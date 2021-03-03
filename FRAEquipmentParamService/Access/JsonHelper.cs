using FRAEquipmentParamService.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService.Access
{
    public class JsonHelper
    {
        public string FilePath { get; set; }

        public JsonHelper(string filePath)
        {
            FilePath = filePath;
        }

        public StationEntity Parse()
        {
            var content = File.ReadAllText(this.FilePath);
            var entity = JsonConvert.DeserializeObject<StationEntity>(content);
            return entity;
        }
        public string Serial(StationEntity entity)
        {
            var str = JsonConvert.SerializeObject(entity);
            return str;
        }
    }
}
