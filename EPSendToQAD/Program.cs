using FreeSql;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace EPSendToQAD
{
    class Program
    {
        private static IFreeSql fsql = null;
        static void Main(string[] args)
        {
            fsql = new FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
                 // .UseMonitorCommand(o => Console.WriteLine(o.CommandText), (o, p) => Console.WriteLine(o.CommandText))
                 .Build();
            try
            {
                Implement();
            }
            catch (Exception ex)
            {
                Log("EP TO QAD", ex.Message, 4, "InternalError");
            }
        }
        private static void Log(string key, string xml, int result, string error)
        {
            var log = fsql.Insert<MES_TL_SYS_QAD_OUTBOUND_LOG>().AppendData(new MES_TL_SYS_QAD_OUTBOUND_LOG
            {
                FID = Guid.NewGuid(),
                METHORD_NAME = "EP TO QAD",
                EXECUTE_START_TIME = DateTime.Now,
                EXECUTE_END_TIME = DateTime.Now,
                EXECUTE_RESULT = result,
                KEY_VALUE = key,
                SOURCE_XML = xml,
                ERROR_DESCRIPTION = error,
                EXECUTE_TIMES = 1,
                VALID_FLAG = true,
                CREATOR = "EPSERVICE",
                CREATE_TIME = DateTime.Now,
            }).ExecuteInserted();
        }

    private static void Implement()
    {
        var epInfoSet = fsql.Select<MES_TI_CIM_FCA_MPAB_EP>().Where(o => !o.STATUS.HasValue || o.STATUS == 0)
            .IncludeMany(o => o.mES_TI_CIM_FCA_MPAB_EP_PARTs.Where(p => p.EP_ID == o.ID))
            .OrderBy(o => o.ID)
            .ToList();

        StringBuilder content = new StringBuilder();
        epInfoSet.ForEach(o =>
        {
            content.AppendLine(GenerateString(o));
        });
        // EDI_EP_22501_10142020_01.txt
        var index = GetFileIndex(DateTime.Now);
        var fileName = $"EDI_EP_22501_{DateTime.Now.ToString("MMddyyyy")}_{index}.txt";
        if (!Directory.Exists("temp"))
        {
            Directory.CreateDirectory("temp");
        }
        File.WriteAllText(Path.Combine("temp", fileName), content.ToString());
        try
        {
            File.Move(Path.Combine("temp", fileName), Path.Combine(ConfigurationManager.AppSettings["dispath_folder"], fileName));
            using (var uoin = fsql.CreateUnitOfWork())
            {
                fsql.Update<MES_TI_CIM_FCA_MPAB_EP>()
                    .SetSource(epInfoSet)
                    .Set(o => o.STATUS, 1)
                    .ExecuteAffrows();

                Log($"{DateTime.Now.ToString("MMddyyyy")}_{index}", content.ToString(), 1, string.Empty);
                uoin.Commit();
            }
        }
        catch (Exception ex)
        {
            Log($"{DateTime.Now.ToString("MMddyyyy")}_{index}", content.ToString(), 1, ex.Message);
        }
    }

    private static string GenerateString(MES_TI_CIM_FCA_MPAB_EP ep)
    {
        // EPS22501  0S283X8JA0990248MS500248000005202010102020100900047EN542X7AA001.00007EN552X7AA001.00007EN562X7AA001.00007EN572X7AA001.0000
        var content = $"{ep.MESSAGE_TYPE}{ep.PLANT_CODE}{ep.SUPPLIER_CODE}{ep.INVOICE}{ep.SEQUENCE_NUMBER.ToString().PadLeft(7, '0')}{ep.VIN}{ep.RUN_NUMBER.ToString().PadLeft(6, '0')}{ep.CREATE_DATE}{ep.PRODUCTION_DATE}{ep.OF_PART_NUMBERS.ToString().PadLeft(4, '0')}";
        var partsContent = new StringBuilder();
        foreach (var part in ep.mES_TI_CIM_FCA_MPAB_EP_PARTs)
        {
            partsContent.Append($"{part.PART_NO}{part.QUANTITY.ToString().PadLeft(3, '0')}.0000");
        }
        return content + partsContent.ToString();
    }

    private static string GetFileIndex(DateTime reference)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.Load("FileName.xml");
        var node = xmlDoc.SelectSingleNode("//File/DT");
        if (node != null && node.Attributes["datetime"].Value.Equals(reference.ToString("MMddyyyy")))
        {
            var index = 1;
            int.TryParse(node.Attributes["index"].Value, out index);
            node.Attributes["index"].Value = (++index).ToString();
            xmlDoc.Save("FileName.xml");
            return index.ToString().PadLeft(2, '0');
        }
        else
        {
            xmlDoc.SelectSingleNode("//File").RemoveChild(node);
            XmlNode dt = xmlDoc.CreateElement("DT");
            XmlAttribute datetime = xmlDoc.CreateAttribute("datetime");
            dt.Attributes.Append(datetime);
            datetime.Value = reference.ToString("MMddyyyy");
            XmlAttribute index = xmlDoc.CreateAttribute("index");
            dt.Attributes.Append(index);
            index.Value = "1";
            xmlDoc.SelectSingleNode("//File").AppendChild(dt);
            xmlDoc.Save("FileName.xml");
            return "01";
        }
    }
}
}
