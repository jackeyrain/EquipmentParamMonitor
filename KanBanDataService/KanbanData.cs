using Jakware.UaClient;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;
using DbType = SqlSugar.DbType;

namespace KanBanDataService
{
    public class KanbanData
    {
        private int INTERVAL = 60;
        private SqlSugarClient db;
        private JakwareUaClient ua;
        public KanbanData()
        {
            int.TryParse(ConfigurationManager.AppSettings["INTERVAL"], out INTERVAL);
            db = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = ConfigurationManager.AppSettings["PISCES"],
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
            });

            //Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    Console.WriteLine(sql + "\r\n" + Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            //};
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
                    await Task.Delay(INTERVAL * 1000);

                    Implement();
                }
            });
        }

        public void Implement()
        {
            try
            {
                this.Send2PLC(this.CalcCount());
            }
            catch (Exception ex)
            {
                Jakware.LogHelper.LogHelper.Log.LogInfo(ex, Jakware.LogHelper.LogHelper.LogType.Exception);
            }
        }

        /*
         1、积压单数
         2、在线生产数
        */
        private string sql = @"
        SELECT COUNT(1) 'stack' FROM MES.TT_APS_WORK_ORDER tawo  WITH(NOLOCK) WHERE tawo.VALID_FLAG=1 AND (tawo.STATUS=10 OR tawo.STATUS=20);
        SELECT COUNT(1) 'producting' FROM MES.TT_APS_WORK_ORDER tawo WITH(NOLOCK) WHERE tawo.VALID_FLAG=1 AND (tawo.STATUS=25 OR tawo.STATUS=120);

";

        public List<(string name, int value)> CalcCount()
        {
            List<(string name, int value)> data = new List<(string name, int value)>();
            var dataSet = db.Ado.GetDataSetAll(sql);

            /*
             1、积压单数
             2、在线生产数
            */
            data.Add(("stack", int.Parse(dataSet.Tables[0].Rows[0][0].ToString())));
            data.Add(("producting", int.Parse(dataSet.Tables[1].Rows[0][0].ToString())));

            data.ForEach(o =>
            {
                Jakware.LogHelper.LogHelper.Log.LogInfo($"{o.name} - {o.value}", Jakware.LogHelper.LogHelper.LogType.Information);
            });
            return data;
        }

        public void Send2PLC(List<(string name, int value)> data)
        {
            foreach (var d in data)
            {
                var address = ConfigurationManager.AppSettings[d.name];
                if (string.IsNullOrEmpty(address))
                {
                    continue;
                }
                ua.AsyncWrite(new WriteDataValue
                {
                    NodeId = NodeId.Parse(address),
                    Value = d.value,
                });
            }
        }
    }
}
