using ProjectArrow.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectArrow.Models
{
    public class AssemblyHelper
    {
        public List<MES_TM_BAS_ASSEMBLY_LINE> GetAssemblyLine()
        {
            var jsonStr = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductLine.json"));
            var productLine = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductLineEntity[]>(jsonStr);

            var list = DBHelper.Db.Select<MES_TM_BAS_ASSEMBLY_LINE>().
               Where(o => o.VALID_FLAG.Value)
               .ToList();

            if (productLine != null && productLine.Length > 0)
            {
                var result = list.Where(o => productLine.Any(p => o.ASSEMBLY_LINE_NAME.Equals(p.ProductLine, StringComparison.OrdinalIgnoreCase))).ToList();
                return result;
            }
            else
            {
                return list;
            }
        }
    }
}