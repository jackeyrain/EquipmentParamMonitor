using EquipmentParamMonitor.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentParamMonitor.ACCESS
{
    public class EQUIPMENT_PARAM_MANAGER : DbContext<TM_BAS_EQUIPMENT>
    {
        public List<TM_BAS_EQUIPMENT[]> GetEquipmentSet()
        {
            var result = new List<TM_BAS_EQUIPMENT[]>();
            var stationSet = this.GetList(o =>
            o.EQUIP_TYPE.Equals("1", StringComparison.OrdinalIgnoreCase)
            && !o.CODE.StartsWith("TT", StringComparison.OrdinalIgnoreCase)
            && o.NAME.StartsWith("CKPT_IP_LINE", StringComparison.OrdinalIgnoreCase)
            );
            foreach (var station in stationSet)
            {
                var relationStation = this.GetList(o =>
                o.EQUIP_TYPE.Equals("1", StringComparison.OrdinalIgnoreCase)
                && o.NAME.StartsWith("CKPT_IP_LINE", StringComparison.OrdinalIgnoreCase)
                && o.NAME.StartsWith(station.NAME, StringComparison.OrdinalIgnoreCase));
                if (relationStation.Count <= 1)
                    continue;

                // TODO: for testing
                //if (!station.CODE.Contains("110"))
                //    continue;

                result.Add(relationStation.ToArray());
            }
            return result;
        }

        public List<EQUIPMENT_VARIABLE> GetEquipmentVariable(string[] equipCode)
        {
            string equipSet = string.Join("','", equipCode);
            equipSet = "'" + equipSet + "'";
            string sql = $@"SELECT a.NAME, a.GROUP_NAME, a.CODE, b.CLIENT_HANDLE, b.NAME 'PARAMNAME', b.CODE 'PARAMCODE', b.DESCRIPTION, b.VARIABLE_TYPE, b.DATA_TYPE
                            FROM MES.TM_BAS_EQUIPMENT a LEFT JOIN MES.TM_BAS_EQUIPMENT_VARIABLE b
                            ON a.ID = b.EQUIP_ID WHERE a.NAME in ({equipSet})";
            var result = this.Db.Ado.SqlQuery<EQUIPMENT_VARIABLE>(sql);
            return result;
        }
    }
}
