using System;
using System.Configuration;
using System.Xml;

namespace KanBanDataService
{
    public class ShiftConfig : IConfigurationSectionHandler
    {
        public DateTime[] dateTimes = new DateTime[4];
        public ShiftConfig()
        {

        }

        public (DateTime, DateTime) CalcDT(DateTime dateTime)
        {
            // shift2
            if (DateTime.Parse(dateTime.ToShortTimeString()) <= DateTime.Parse(dateTimes[0].ToShortTimeString()))
            {
                return (DateTime.Parse(dateTime.AddDays(-1).ToShortDateString() + " " + dateTimes[2].ToShortTimeString()),
                        DateTime.Parse(dateTime.ToShortDateString() + " " + dateTimes[0].ToShortTimeString())
                    );
            }
            // shift2
            if (DateTime.Parse(dateTime.ToShortTimeString()) >= DateTime.Parse(dateTimes[2].ToShortTimeString()))
            {
                return (DateTime.Parse(dateTime.ToShortDateString() + " " + dateTimes[2].ToShortTimeString()),
                        DateTime.Parse(dateTime.AddDays(1).ToShortDateString() + " " + dateTimes[0].ToShortTimeString())
                      );
            }
            // shift1
            if (DateTime.Parse(dateTime.ToShortTimeString()) >= DateTime.Parse(dateTimes[0].ToShortTimeString()) &&
                DateTime.Parse(dateTime.ToShortTimeString()) < DateTime.Parse(dateTimes[1].ToShortTimeString())
                )
            {
                return (DateTime.Parse(dateTime.ToShortDateString() + " " + dateTimes[0].ToShortTimeString()),
                        DateTime.Parse(dateTime.ToShortDateString() + " " + dateTimes[1].ToShortTimeString())
                      );
            }

            // 默认shift2
            return (DateTime.Parse(dateTime.AddDays(-1).ToShortDateString() + " " + dateTimes[2].ToShortTimeString()),
                        DateTime.Parse(dateTime.ToShortDateString() + " " + dateTimes[0].ToShortTimeString())
                    );
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            int index = 0;
            foreach (XmlNode node in section.ChildNodes)
            {
                dateTimes[index++] = Convert.ToDateTime(node.Attributes["begin"].Value);
                dateTimes[index++] = Convert.ToDateTime(node.Attributes["end"].Value);
            }
            return this;
        }
    }
}
