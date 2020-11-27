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
            GroupSet = new List<MES_TM_BAS_PART_PRODUCT_GROUP>();
            MonitorStationSet = new List<string>();
            TargetStationSet = new List<string>();
        }
        public static List<MES_TM_BAS_PART_PRODUCT_GROUP> GroupSet { get; set; }
        public static List<string> MonitorStationSet { get; set; }
        public static List<string> TargetStationSet { get; set; }

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
                var group = DBHelper.DB.Select<MES_TM_BAS_PART_PRODUCT_GROUP>()
                    .Where(o => o.VALID_FLAG.Value && o.GROUP_CODE.Equals(node.Attributes["ProductGroupCode"].Value)).First();
                if (group == null)
                    continue;
                GroupSet.Add(group);
                MonitorStationSet.Add(node.Attributes["MonitorStation"].Value);
                TargetStationSet.Add(node.Attributes["TargetStation"].Value);

                OrderTags.Add(new OrderTag
                {
                    ID = int.Parse(node.Attributes["ID"].Value),
                    ProductGroupId = group.ID,
                    TagAddress = node.Attributes["TagAddress"].Value,
                    MonitorStation = node.Attributes["MonitorStation"].Value,
                    TargetStation = node.Attributes["TargetStation"].Value,
                    HandShake = node.Attributes["HandShake"].Value,
                });
            }
            return this;
        }
    }

    public class OrderTag
    {
        public int ID { get; set; }
        public long ProductGroupId { get; set; }
        public string TagAddress { get; set; }
        public string MonitorStation { get; set; }
        public string TargetStation { get; set; }
        public string HandShake { get; set; }
    }
}
