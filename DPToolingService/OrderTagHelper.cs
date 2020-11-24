using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DPToolingService
{
    public class OrderTagHelper : IConfigurationSectionHandler
    {
        public List<OrderTag> OrderTags { get; set; }
        public OrderTagHelper()
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
                var groupId = DBHelper.DB.Select<MES_TM_BAS_PART_PRODUCT_GROUP>()
                    .Where(o => o.VALID_FLAG.Value && o.GROUP_CODE.Equals(node.Attributes["ProductGroupCode"].Value)).First()?.ID;
                if (!groupId.HasValue)
                    continue;
                OrderTags.Add(new OrderTag
                {
                    ID = int.Parse(node.Attributes["ID"].Value),
                    Buffer = int.Parse(node.Attributes["Buffer"].Value),
                    ProductGroupId = groupId.Value,
                    TagAddress = node.Attributes["TagAddress"].Value,
                    MonitorStation = node.Attributes["MonitorStation"].Value,
                    HandShake = node.Attributes["HandShake"].Value,
                });
            }
            return this;
        }
    }

    public class OrderTag
    {
        public int ID { get; set; }
        public int Buffer { get; set; }
        public long ProductGroupId { get; set; }
        public string TagAddress { get; set; }
        public string MonitorStation { get; set; }
        public string HandShake { get; set; }
    }
}
