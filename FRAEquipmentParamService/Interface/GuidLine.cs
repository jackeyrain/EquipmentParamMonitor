using DPToleranceMonitorService.Model.DB;
using FRAEquipmentParamService.Access;
using FRAEquipmentParamService.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FRAEquipmentParamService.Interface
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“GuidLine”。
    public class GuidLine : IGuidLine
    {
        public string SendToGuidLine(string order_code)
        {
            try
            {
                var repairs = DBAccess.Instance.Select<MES_TR_CIM_TOBE_REPAIRED>().Where(o => o.ORDER_CODE.Equals(order_code)).ToList();
                var data = ServiceHostServer.StationEntities;

                if (repairs.Count <= 0)
                {
                    return $"{order_code} is Good Part.";
                }
                // STATION150-WeldResult_031-Depth:[509], Energy:[94], MeanPower:[0.085], Pass:[2], Robot:[6], Weld_No:[31]
                List<string> returnMsg = new List<string>();

                foreach (var defect in repairs)
                {
                    var sections = defect.DESCRIPTION?.Split('-');
                    if (sections == null || sections.Length != 3)
                        continue;

                    var _station = sections[0];
                    var _position = sections[1];

                    var entity = data.FirstOrDefault(o => o.Name.Equals(_station, StringComparison.OrdinalIgnoreCase));
                    if (entity == null)
                        continue;

                    var d_p = entity.ParamSet.FirstOrDefault(o => o.Name.Equals(_position, StringComparison.OrdinalIgnoreCase));
                    if (d_p == null)
                        continue;

                    var resultItem = d_p.TagAddress.FirstOrDefault(o => o.Flag.Equals("result", StringComparison.OrdinalIgnoreCase));
                    if (resultItem == null)
                        continue;

                    var station = Program.stationMonitors.FirstOrDefault(o =>
                    {
                        if (!(o is StationMonitor))
                            return false;
                        return ((StationMonitor)o).Entity.Name.Equals(entity.Name);
                    });
                    if (station == null)
                        continue;

                    var result = station.WriteValue(resultItem.GuidLine, 2);
                    if (!string.IsNullOrEmpty(result))
                        return result;

                    returnMsg.Add(_position);
                }

                return string.Join(",", returnMsg);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
