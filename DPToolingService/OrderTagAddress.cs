using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DPToolingService
{
    public class OrderTagAddress : IConfigurationSectionHandler
    {
        public List<OrderTag> OrderTags { get; set; }
        public OrderTagAddress()
        {
            OrderTags = new List<OrderTag>();
        }

        public OrderTag this[int index]
        {
            get
            {
                return this.OrderTags.FirstOrDefault(o => o.ID == index);
            }
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            foreach (XmlNode node in section.ChildNodes)
            {
                OrderTags.Add(new OrderTag
                {
                    ID = int.Parse(node.Attributes["ID"].Value),
                    TagAddress = node.Attributes["TagAddress"].Value,
                });
            }
            return this;
        }
    }

    public class OrderTag
    {
        public int ID { get; set; }
        public string TagAddress { get; set; }
    }
}
