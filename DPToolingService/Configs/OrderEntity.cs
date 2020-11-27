using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPToolingService
{
    public class OrderEntity
    {
        public OrderTag OrderTag { get; set; }
        public MES_TT_APS_WORK_ORDER MES_TT_APS_WORK_ORDER { get; set; }
        public int Value { get; set; }
    }
}
