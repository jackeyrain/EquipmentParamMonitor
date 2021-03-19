using EquipmentParamMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.ACCESS
{
    public class FCEQUIPPARAMLOG_MANAGER : DbContext<FCEQUIPPARAMLOG>
    {
        public int AddBluk(List<FCEQUIPPARAMLOG> logSet)
        {
            return this.Db.Insertable(logSet.ToArray()).ExecuteCommand();
        }
    }
}
