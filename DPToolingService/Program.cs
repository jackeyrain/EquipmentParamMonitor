using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPToolingService
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.Log.LogInfo("Tooling service is starting.", LogHelper.LogType.Information);

            var orderTags = ConfigurationManager.GetSection("OrderTagHelper") as OrderTagHelper;

            var orderQueue = new OrderQueue(orderTags.OrderTags);
            orderQueue.GetWorkOrderBuffer();
        }
    }
}
