using System;
using System.Collections.Generic;

namespace DPToolingService.ToolHelper
{
    public class WorkOrderEqualCompare : IEqualityComparer<MES_TT_APS_WORK_ORDER>
    {
        public bool Equals(MES_TT_APS_WORK_ORDER x, MES_TT_APS_WORK_ORDER y)
        {
            return x.ORDER_CODE.Equals(y.ORDER_CODE, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(MES_TT_APS_WORK_ORDER obj)
        {
            return obj.ORDER_CODE.GetHashCode();
        }
    }
}
