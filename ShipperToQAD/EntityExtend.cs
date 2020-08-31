using ShipperToQAD.QADShipper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ShipperToQAD
{
    public static class EntityExtend
    {
        public static string ToXml(this INSEQShipper shipper)
        {
            var xml = Serialize<INSEQShipper>(shipper);
            return xml;
        }

        public static string Serialize<T>(T obj)
            where T : class
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            StringBuilder strBuilder = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(strBuilder, settings))
            {
                serializer.Serialize(writer, obj, ns);
                writer.Close();
            }
            return strBuilder.ToString();
        }

        public static T Deserial<T>(string xmlStr)
            where T : class
        {
            T r = default(T);
            MemoryStream memStream = new MemoryStream();
            byte[] data = Encoding.UTF8.GetBytes(xmlStr);
            memStream.Write(data, 0, data.Length);
            memStream.Position = 0;
            using (XmlReader reader = XmlReader.Create(memStream))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                r = serializer.Deserialize(reader) as T;
            }
            return r;
        }
    }
}
