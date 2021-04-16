using Jakware.UaClient;
using ProjectArrow.Entity;
using ProjectArrow.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using UnifiedAutomation.UaBase;

namespace ProjectArrow.App_Code
{
    public class ProjectArrowMonitor
    {
        private static JakwareUaClient uaClient = null;
        private static readonly ProductLineEntity[] productionLine;

        static ProjectArrowMonitor()
        {
            productionLine = LoadProductLineJson();
        }

        public static void StartMonitor()
        {
            var nodeSet = productionLine.Where(o => !string.IsNullOrEmpty(o.MoldTagAddress)).ToList();
            if (nodeSet.Count <= 0) return;

            new TaskFactory().StartNew(() =>
            {
                var nodes = nodeSet.Select(o => NodeId.Parse(o.MoldTagAddress.Trim())).ToList();
                var config = LoadConfigXml();
                uaClient = new JakwareUaClient(config);
                uaClient.Initialize();
                uaClient.Connect();
                uaClient.JakwareDataChangedEventHandler += UaClient_JakwareDataChangedEventHandler;

                uaClient.StartSubscription();
                try
                {
                    uaClient.AddMonitorNodeId(nodes);
                }
                catch (Exception e)
                {
                    _ = e.Message;
                }
            });
        }

        private async static void UaClient_JakwareDataChangedEventHandler(UnifiedAutomation.UaClient.Subscription subscription, JakwareDataChangedEventArgs e)
        {
            foreach (var item in e.JakwareDataChanges)
            {
                if (!item.IsGood ||
                    !item.MonitoredItem.NodeId.ToString().ToLower().Contains("moldnumber") ||
                    string.IsNullOrEmpty(item.Value.Value as string))
                    continue;

                var line = productionLine.FirstOrDefault(o => o.MoldTagAddress.Equals(item.MonitoredItem.NodeId.ToString(), StringComparison.OrdinalIgnoreCase));
                if (line == null) continue;

                await new TaskFactory().StartNew(() =>
                {
                    var lastTask = new TaskHelper().GetLastMoldNumber(line.Id) ?? new MES_ProjectArrow();
                    if (string.IsNullOrEmpty(lastTask.MOLDNUMBER) ||
                    !lastTask.MOLDNUMBER.Equals(item.Value.Value.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        var equip = DBHelper.Db.Select<MES_TM_BAS_EQUIPMENT>(line.Id).First();
                        if (equip == null) return;

                        var moldPart = DBHelper.Db.Select<MES_TM_BAS_MOULD_PART>()
                        .Where(o => o.EQUIPMENT_FID == equip.FID && o.EQUIP_MOULD_NO.Equals(item.Value.Value.ToString(), StringComparison.OrdinalIgnoreCase))
                        .ToList();
                        if (moldPart.Count <= 0) return;

                        var taskGuid = Guid.NewGuid();
                        foreach (var part in moldPart)
                        {
                            var partInfo = DBHelper.Db.Select<MES_TM_BAS_PART>()
                            .Where(o => o.PART_NO.Equals(part.PART_NO, StringComparison.OrdinalIgnoreCase) && o.VALID_FLAG.Value)
                            .First();
                            new TaskHelper().AddProjectArrow(new MES_ProjectArrow
                            {
                                ORDERNUMBER = $"{item.Value.Value}-{DateTime.Now:yyMMddHHmm}",
                                SERIAL_ID = taskGuid,
                                ASSEMBLYLINE = line.ProductLine,
                                STATION = line.ProductLine,
                                PARTNUMBER = partInfo.PART_NO,
                                PARTNAME = partInfo.PART_NAME,
                                EQUIPID = line.Id,
                                EQUIPPARAMID = 10,
                                PARAMVALUE = "10",
                                STATUS = 0,
                                VALID_FLAG = true,
                                MOLDNUMBER = item.Value.Value.ToString()
                            });
                        }
                    }
                });
            }
        }

        public static string LoadConfigXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EquipmentConfig.xml"));
            string url = xml.SelectSingleNode("/Equipment/Url").InnerText;
            return url;
        }
        public static ProductLineEntity[] LoadProductLineJson()
        {
            string json_str = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductLine.json"));
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductLineEntity[]>(json_str);
            return result;
        }
    }
}