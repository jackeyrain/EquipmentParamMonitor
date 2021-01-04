using Jakware.UaClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;

namespace KanBanDataService
{
    public class KanbanData
    {
        private List<TagConfig> tagConfigs = null;
        private ShiftConfig shiftConfig = null;
        private int INTERVAL = 60;
        private JakwareUaClient ua;
        public KanbanData()
        {
            // GetDataFromInterface();

            tagConfigs = ConfigurationManager.GetSection("TagConfig") as List<TagConfig>;
            shiftConfig = ConfigurationManager.GetSection("ShiftConfig") as ShiftConfig;

            int.TryParse(ConfigurationManager.AppSettings["INTERVAL"], out INTERVAL);

            ua = new JakwareUaClient(ConfigurationManager.AppSettings["OPCSERVER"]);
            ua.Initialize();
            ua.Connect();
        }

        public void Start()
        {
            new TaskFactory().StartNew(async () =>
            {
                while (!Program.cts.IsCancellationRequested)
                {
                    Implement();

                    await Task.Delay(INTERVAL * 1000);
                }
            });
        }

        public void Implement()
        {
            try
            {
                this.Send2Kepware(this.CalcCount());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<(string name, dynamic value)> CalcCount()
        {
            List<(string name, dynamic value)> data = new List<(string name, dynamic value)>();

            // 获取看板数
            //var dataTable = DBHelper.DBInstance.Ado.ExecuteDataTable(System.Data.CommandType.StoredProcedure, "PROC_GET_BUFFER_DISPLAY");
            //var dataRow = dataTable.Select($"{nameof(RealData.ASSEMBLY_LINE)} = 'Cockpit'").FirstOrDefault();
            var dataRow = this.GetDataFromInterface();

            var realData = new RealData
            {
                ASSEMBLY_LINE = Convert.ToString(dataRow[0] ?? string.Empty),
                ASSEMBLY_LINE_NAME = Convert.ToString(dataRow[1] ?? string.Empty),
                LAST_RECEIVED = Convert.ToDateTime(dataRow[2] ?? DateTime.MinValue),
                INFO_POINT_CODE = Convert.ToString(dataRow[3] ?? string.Empty),
                Current_Sequence = Convert.ToInt64(dataRow[4] ?? 0),
                Queue_Count = Convert.ToInt64(dataRow[5] ?? 0),
                Last_Shipped = Convert.ToInt64(dataRow[6] ?? 0),
                Local_Bank = Convert.ToInt32(dataRow[7] ?? 0),
                OEM_Bank = Convert.ToInt32(dataRow[8] ?? 0),
            };

            // 获取Queue值
            data.Add(("Queue", realData.Queue_Count + tagConfigs.GetDetailValue("Queue")));
            // 获取LocalBank值
            data.Add(("LocalBank", realData.Local_Bank + tagConfigs.GetDetailValue("LocalBank")));
            // 获取WIP值
            data.Add(("WIP", new DBHelper().GetWIPCount() + tagConfigs.GetDetailValue("WIP")));
            // 获取OEMBank值
            data.Add(("OEMBank", realData.OEM_Bank + tagConfigs.GetDetailValue("OEMBank")));

            var dt = shiftConfig.CalcDT(DateTime.Now);
            var rebuildCount = new DBHelper().GetRebuildCount(dt);
            var repairCount = new DBHelper().GetRepairCount(dt);
            var workOrderCount = new DBHelper().GetWorkOrderCount(dt);

            // 获取FTT值
            if (workOrderCount != 0)
            {
                data.Add(("FTT", Convert.ToInt32(((decimal)workOrderCount - (decimal)repairCount) * 100 / (decimal)workOrderCount) + tagConfigs.GetDetailValue("FTT")));
            }
            else
            {
                data.Add(("FTT", 0));
            }
            // 获取Repairs值
            data.Add(("Repairs", repairCount + tagConfigs.GetDetailValue("Repairs")));
            // 获取Rebuild值
            data.Add(("Rebuild", rebuildCount + tagConfigs.GetDetailValue("Rebuild")));


            data.ForEach(o =>
                {
                    Console.WriteLine($"{o.name} - {o.value}");
                    LogHeloer.Log($"{o.name} - {o.value}");
                });

            return data;
        }

        public void Send2Kepware(List<(string name, dynamic value)> data)
        {
            foreach (var d in data)
            {
                var addressSet = tagConfigs.Where(o => o.Categary.Equals(d.name, StringComparison.OrdinalIgnoreCase));

                foreach (var address in addressSet)
                {
                    LogHeloer.Log(address.TagAddress + ":" + d.value.ToString());
                    if (string.IsNullOrEmpty(address.TagAddress))
                    {
                        continue;
                    }
                    var result = ua.Write(new WriteDataValue
                    {
                        NodeId = NodeId.Parse(address.TagAddress),
                        Value = Convert.ToInt32(d.value),
                    });
                    if (result.Count > 0)
                    {
                        LogHeloer.Log(string.Join(",", result.Select(o => o.Error)));
                    }
                    else
                    {
                        LogHeloer.Log("Success");
                    }
                }
            }
        }

        public dynamic GetDataFromInterface()
        {
            var client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 30);
            HttpResponseMessage response = null;

            var postParam = new { lines = new[] { ConfigurationManager.AppSettings["PRODUCTIONLINE"] } };
            var jsonPostParam = Newtonsoft.Json.JsonConvert.SerializeObject(postParam);
            HttpContent content = new StringContent(jsonPostParam);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            response = client.PostAsync("http://10.16.112.20:9000/PS/BufferDisplay.aspx/GetBufferList", content).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            var result = response.Content.ReadAsStringAsync().Result;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(result) as JObject;
            var obj1 = obj.First.First.First;

            List<string> resultObj = new List<string>();
            resultObj.Add(obj1.Value<string>("AssemblyLine"));
            resultObj.Add(obj1.Value<string>("AssemblyLineName"));
            resultObj.Add(obj1.Value<string>("LastReceived"));
            resultObj.Add(obj1.Value<string>("InfoPointCode"));
            resultObj.Add(obj1.Value<string>("CurrentSeq"));
            resultObj.Add(obj1.Value<string>("QueueCount"));
            resultObj.Add(obj1.Value<string>("LastShipped"));
            resultObj.Add(obj1.Value<string>("LocalBank"));
            resultObj.Add(obj1.Value<string>("OemBank"));

            return resultObj;
        }
    }

    public class RealData
    {
        public string ASSEMBLY_LINE { get; set; }
        public string ASSEMBLY_LINE_NAME { get; set; }
        public DateTime LAST_RECEIVED { get; set; }
        public string INFO_POINT_CODE { get; set; }
        public long Current_Sequence { get; set; }
        public long Queue_Count { get; set; }
        public long Last_Shipped { get; set; }
        public int Local_Bank { get; set; }
        public int OEM_Bank { get; set; }
    }

    public class LogHeloer
    {
        static LogHeloer()
        {
            if (!Directory.Exists("Logs")) Directory.CreateDirectory("Logs");
        }

        public static void Log(string value)
        {
            File.AppendAllText(Path.Combine("Logs", DateTime.Now.ToString("yyyy-MM-dd") + ".txt"), $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {value}{Environment.NewLine}");
        }
    }
}
