using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkOrderPrint
{
    public class BartenderPrintHelper : IDisposable
    {

        private static object _entity_lock_object = new object();

        private static object _entity_lock_objectN = new object();

        private string _exePath = string.Empty;

        private string _btwPath = string.Empty;

        private string _dbPath = string.Empty;

        private const char TAB = '\t';

        private object _lock_print_object = new object();

        private object _lock_docList_object = new object();

        private object _lock_queue_object = new object();

        private object _lock_file_object = new object();

        // Print Pool List static Variable
        private readonly int PrintBatchCount = 1;

        private static ManualResetEventSlim mres = null;

        private static CancellationTokenSource cts = null;

        public BartenderPrintHelper(string btwPath, int count, bool startThreadPool = true)
        {
            this._exePath = Path.Combine(Environment.CurrentDirectory, "Bartend", "bartend.exe");
            this._btwPath = btwPath;
            if (!File.Exists(this._btwPath))
            {
                throw new FileNotFoundException("btw file does exists");
            }
            var btwInfoName = new FileInfo(btwPath).Name;
            this._dbPath = Path.Combine("temp", btwInfoName.Substring(0, btwInfoName.Length - 4) + ".txt");

            mres = new ManualResetEventSlim(true);
            cts = new CancellationTokenSource();
            PrintBatchCount = count;
        }

        public string BTWPath
        {
            set
            {
                if (File.Exists(value))
                {
                    _btwPath = value;
                }
                else
                {
                    throw new FileNotFoundException("btw file does not exist.");
                }
            }
            get
            {
                return _btwPath;
            }
        }

        public void Print(List<string> dic, int count = 1)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in dic)
            {
                sb.Append(item); sb.Append(TAB);
            }

            WriteFile(_dbPath, sb.ToString(), Encoding.UTF8, false);
            lock (_lock_print_object)
            {
                Print();
            }
        }

        private void Print()
        {
            mres.Reset();

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(this._exePath, " /F=\"" + _btwPath + "\" /D=\"" + _dbPath + "\" /P /X");
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardOutput = true;
            try
            {
                process.Start();
                process.WaitForExit();
                if (!process.HasExited)
                {
                    process.Kill();
                    process.Dispose();
                }
            }
            catch (Exception ex)
            {
                process.Kill();
                process.Dispose();
                LogHelper.Log.LogInfo(ex, LogHelper.LogType.Exception);
            }
            finally
            {
                try
                {
                    // File.Delete(_dbPath);
                }
                catch (Exception exx)
                {
                    LogHelper.Log.LogInfo(exx, LogHelper.LogType.Exception);
                }
            }
            mres.Set();
        }

        private void WriteFile(string filePath, string strText, Encoding encoding, bool append)
        {
            lock (_lock_file_object)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                using (StreamWriter sw = new StreamWriter(filePath, append, encoding))
                {
                    sw.WriteLine(strText);
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                cts.Cancel();
                mres.Set();
            }
            GC.Collect();
        }

        ~BartenderPrintHelper()
        {
            Dispose(false);
        }
    }
}
