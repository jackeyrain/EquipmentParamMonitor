using Jakware.UaClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            var dataTable = DBHelper.DBInstance.Ado.ExecuteDataTable(System.Data.CommandType.StoredProcedure, "PROC_GET_BUFFER_DISPLAY");
            var dataRow = dataTable.Select($"{nameof(RealData.ASSEMBLY_LINE)} = 'Cockpit'").FirstOrDefault();
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
            data.Add(("Queue", realData.Queue_Count));
            // 获取LocalBank值
            data.Add(("LocalBank", realData.Local_Bank));
            // 获取WIP值
            data.Add(("WIP", new DBHelper().GetWIPCount()));
            // 获取OEMBank值
            data.Add(("OEMBank", realData.OEM_Bank));

            var dt = shiftConfig.CalcDT(DateTime.Now);
            var rebuildCount = new DBHelper().GetRebuildCount(dt);
            var repairCount = new DBHelper().GetRepairCount(dt);
            var workOrderCount = new DBHelper().GetWorkOrderCount(dt);

            // 获取FTT值
            if (workOrderCount != 0)
            {
                data.Add(("FTT", Convert.ToInt32(((decimal)workOrderCount - (decimal)repairCount) * 100 / (decimal)workOrderCount)));
            }
            else
            {
                data.Add(("FTT", 0));
            }
            // 获取Repairs值
            data.Add(("Repairs", repairCount));
            // 获取Rebuild值
            data.Add(("Rebuild", rebuildCount));


            data.ForEach(o =>
                {
                    Console.WriteLine($"{o.name} - {o.value}");
                });

            return data;
        }

        public void Send2Kepware(List<(string name, dynamic value)> data)
        {
            foreach (var d in data)
            {
                var address = tagConfigs.FirstOrDefault(o => o.Categary.Equals(d.name, StringComparison.OrdinalIgnoreCase));
                if (string.IsNullOrEmpty(address.TagAddress))
                {
                    continue;
                }
                ua.AsyncWrite(new WriteDataValue
                {
                    NodeId = NodeId.Parse(address.TagAddress),
                    Value = d.value,
                });
            }
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
}
