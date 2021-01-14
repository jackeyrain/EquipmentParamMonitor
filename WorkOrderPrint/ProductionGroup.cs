using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WorkOrderPrint
{
    public class ProductionGroup : IConfigurationSectionHandler
    {
        public string GroupCode { get; set; }
        public string Template { get; set; }
        public string Printer { get; set; }

        public object Create(object parent, object configContext, XmlNode section)
        {
            List<ProductionGroup> data = new List<ProductionGroup>();
            foreach (XmlNode item in section.ChildNodes)
            {
                data.Add(new ProductionGroup
                {
                    GroupCode = item.Attributes["GroupCode"].Value,
                    Template = item.Attributes["Template"].Value,
                    Printer = item.Attributes["Printer"].Value,
                });
            }
            return data;
        }
    }
}
