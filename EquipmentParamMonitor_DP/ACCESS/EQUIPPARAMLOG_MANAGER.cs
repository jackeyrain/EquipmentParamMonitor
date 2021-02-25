using EquipmentParamMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.ACCESS
{
    public class EQUIPPARAMLOG_MANAGER : DbContext<EQUIPPARAMLOG>
    {
        public int AddBluk(List<EQUIPPARAMLOG> logSet)
        {
            return this.Db.Insertable(logSet.ToArray()).ExecuteCommand();
        }
    }
}
