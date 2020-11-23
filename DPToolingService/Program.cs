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
        private IFreeSql freeSql = null;
        static void Main(string[] args)
        {
            var orderTags = ConfigurationManager.GetSection("OrderTagAddress") as OrderTagAddress;
        }
    }
}
