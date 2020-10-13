using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KanBanDataService
{
    public class TagConfig : IConfigurationSectionHandler
    {
        public string Categary { get; set; }
        public string TagAddress { get; set; }
        public int Detal { get; set; }

        public TagConfig()
        {

        }

        public TagConfig(string categray, string tagAddress, int detail)
        {
            this.Categary = categray;
            this.TagAddress = tagAddress;
            this.Detal = detail;
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            List<TagConfig> data = new List<TagConfig>();
            foreach (XmlNode node in section.ChildNodes)
            {
                data.Add(new TagConfig
                {
                    Categary = node.Attributes["Categary"].Value,
                    TagAddress = node.Attributes["TagAddress"].Value,
                    Detal = Convert.ToInt32(node.Attributes["Detal"].Value),
                });
            }
            return data;
        }

        public override string ToString()
        {
            return $"{Categary} - {TagAddress} - {Detal}";
        }
    }

    public static class TagConfigExtend
    {
        public static decimal GetDetailValue(this List<TagConfig> source, string categary)
        {
            var tagConfig = source.Find(o => o.Categary.Equals(categary, StringComparison.OrdinalIgnoreCase));
            if (tagConfig == null)
            {
                return 0;
            }
            return tagConfig.Detal;
        }
    }
}
