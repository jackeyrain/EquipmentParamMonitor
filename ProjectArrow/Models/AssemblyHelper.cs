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
            var productLine = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductLineFilter[]>(jsonStr);

            var list = DBHelper.Db.Select<MES_TM_BAS_ASSEMBLY_LINE>().
               Where(o => o.VALID_FLAG.Value)
               .ToList();

            if (productLine != null && productLine.Length > 0)
            {
                return list.Where(o => productLine.Any(p => o.ASSEMBLY_LINE_NAME.Equals(p.ProductLine, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            else
            {
                return list;
            }
        }
    }

    public class ProductLineFilter
    {
        public string ProductLine { get; set; }
    }
}