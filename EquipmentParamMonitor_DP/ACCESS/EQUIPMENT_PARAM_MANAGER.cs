using EquipmentParamMonitor.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            // 获取主工位设备
            var equipSet = this.GetList(o => (ConfigurationManager.AppSettings["EXTARLOCATION"].Split(new[] { ',' }).Contains(o.NAME)));
            foreach (var station in equipSet)
            {
                var relationEquip = this.GetList(o =>
                   o.EQUIP_TYPE.Equals("1", StringComparison.OrdinalIgnoreCase) &&
                   o.LOCATION.Equals(station.NAME, StringComparison.OrdinalIgnoreCase));
                // 将本工位设备加入
                relationEquip.Insert(0, station);

                // TODO: for testing
                //if (!station.CODE.Contains("110"))
                //    continue;

                result.Add(relationEquip.ToArray());
            }
            return result;
        }

        public List<EQUIPMENT_VARIABLE> GetEquipmentVariable(string[] equipCode)
        {
            string equipSet = string.Join("','", equipCode);
            equipSet = "'" + equipSet + "'";
            string sql = $@"SELECT a.NAME, a.GROUP_NAME, a.CODE, b.CLIENT_HANDLE, b.ID 'PARAMID', b.NAME 'PARAMNAME', b.CODE 'PARAMCODE', b.DESCRIPTION, b.VARIABLE_TYPE, b.DATA_TYPE
                            FROM MES.TM_BAS_EQUIPMENT a LEFT JOIN MES.TM_BAS_EQUIPMENT_VARIABLE b
                            ON a.ID = b.EQUIP_ID WHERE a.NAME in ({equipSet})";
            var result = this.Db.Ado.SqlQuery<EQUIPMENT_VARIABLE>(sql);
            return result;
        }
    }
}
