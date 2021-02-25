using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.Service
{
    partial class WinService : ServiceBase
    {
        public WinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Thread.Sleep(60000);
            global::EquipmentParamMonitor.Program.Implement();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(o =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(1000);
            //        LogHelper.Log.LogInfo(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), LogHelper.LogType.Information);
            //        // File.AppendAllText("D:\\1.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine);
            //    }
            //}));
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
