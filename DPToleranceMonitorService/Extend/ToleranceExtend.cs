using DPToleranceMonitorService.Model;
using System.IO;

namespace DPToleranceMonitorService.Extend
{
    public static class ToleranceExtend
    {
        public static ToleranceInstance Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            var jsonContent = File.ReadAllText(filePath);
            var instance = Newtonsoft.Json.JsonConvert.DeserializeObject<ToleranceInstance>(jsonContent);
            return instance;
        }
    }
}
