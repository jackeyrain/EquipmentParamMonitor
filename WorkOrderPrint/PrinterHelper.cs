using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderPrint
{
    public class PrinterHelper
    {
        ManagementObjectSearcher searcher = null;
        public PrinterHelper()
        {
            searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Printer");
        }

        public List<string> GetPrinter()
        {
            var result = new List<string>();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                // Console.WriteLine("-----------------------------------");
                // Console.WriteLine("Win32_Printer instance");
                // Console.WriteLine("-----------------------------------");
                // Console.WriteLine("Default: {0}", queryObj["Default"]);
                // Console.WriteLine("DeviceID: {0}", queryObj["DeviceID"]);
                // Console.WriteLine("DriverName: {0}", queryObj["DriverName"]);
                result.Add(queryObj["DriverName"].ToString());
            }
            return result;
        }
        public bool SetDefault(string printer)
        {
            try
            {
                ManagementObject classInstance = new ManagementObject("root\\CIMV2", "Win32_Printer.DeviceID='" + printer + "'", null);
                ManagementBaseObject outParams = classInstance.InvokeMethod("SetDefaultPrinter", null, null);
                var r = outParams.Properties["ReturnValue"];
                return r.Value.ToString() == "0";
            }
            catch (Exception ex)
            {
                LogHelper.Log.LogInfo(ex, LogHelper.LogType.Exception);
                return false;
            }
        }
    }
}
