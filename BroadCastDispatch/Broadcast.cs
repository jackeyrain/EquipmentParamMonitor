using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BroadCastDispatch
{
    public class Broadcast : IConfigurationSectionHandler
    {
        public string BroadcastType { get; set; }
        public string Source { get; set; }
        public string Dispatch { get; set; }
        public Broadcast()
        {
        }
        public Broadcast(string broadcastType, string source, string dispatch)
        {
            BroadcastType = broadcastType;
            Source = source;
            Dispatch = dispatch;
        }
        public object Create(object parent, object configContext, XmlNode section)
        {
            List<Broadcast> data = new List<Broadcast>();
            foreach (XmlNode item in section.ChildNodes)
            {
                data.Add(new Broadcast(
                    item.Attributes["BroadcastType"].Value,
                    item.Attributes["Source"].Value,
                    item.Attributes["Dispatch"].Value));
            }
            return data;
        }
    }
}
