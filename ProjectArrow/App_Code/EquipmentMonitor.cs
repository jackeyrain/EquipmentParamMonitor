using Jakware.UaClient;
using ProjectArrow.Entity;
using ProjectArrow.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using UnifiedAutomation.UaBase;

namespace ProjectArrow.App_Code
{
    public class EquipmentMonitor
    {
        private static JakwareUaClient uaClient = null;
        private static List<Barrer> hoses = null;

        public static void StartMonitor()
        {
            new TaskFactory().StartNew(() =>
                {
                    var config = LoadXml();
                    uaClient = new JakwareUaClient(config);
                    hoses = new RowConfigHelper().GetBarrers();
                    uaClient.Initialize();
                    uaClient.Connect();
                    uaClient.JakwareDataChangedEventHandler += UaClient_JakwareDataChangedEventHandler;
                    uaClient.StartSubscription();
                    var nodes = new List<NodeId>();
                    foreach (var h in hoses)
                    {
                        nodes.Add(NodeId.Parse(h.TagAddress));
                    }
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

        private static void UaClient_JakwareDataChangedEventHandler(UnifiedAutomation.UaClient.Subscription subscription, JakwareDataChangedEventArgs e)
        {
            foreach (var dataChange in e.JakwareDataChanges)
            {
                try
                {
                    var hose = hoses.FirstOrDefault(o => o.TagAddress.Equals(dataChange.MonitoredItem.NodeId.ToString(), StringComparison.OrdinalIgnoreCase));
                    hose?.BarrelExecute(dataChange.Value);
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
            }
        }

        public static string LoadXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EquipmentConfig.xml"));
            string url = xml.SelectSingleNode("/Equipment/Url").InnerText;
            return url;
        }
    }
}