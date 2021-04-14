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
            LogHelper.Log.LogInfo($"WCF Interface Receive {order_code}", LogHelper.LogType.Information);
            try
            {
                var repairs = DBAccess.Instance.Select<MES_TR_CIM_TOBE_REPAIRED>().Where(o => o.ORDER_CODE.Equals(order_code)).ToList();
                var data = ServiceHostServer.StationEntities;

                LogHelper.Log.LogInfo($"{order_code} defect items' quantity - {repairs.Count}", LogHelper.LogType.Information);

                if (repairs.Count <= 0)
                {
                    return $"{order_code} is Good Part.";
                }

                var assembly = repairs.FirstOrDefault().ASSEMBLY_LINE;
                var partType = 0;
                switch (assembly)
                {
                    case "RFWS":
                    case "RRWS":
                        partType = 1;
                        break;
                    case "LFWS":
                    case "LRWS":
                        partType = 2;
                        break;
                    default:
                        partType = 0;
                        break;
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
                    if (resultItem == null || string.IsNullOrEmpty(resultItem.GuidLine))
                        continue;

                    var station = Program.stationMonitors.FirstOrDefault(o =>
                    {
                        if (!(o is StationMonitor))
                            return false;
                        return ((StationMonitor)o).Entity.Name.Equals(entity.Name);
                    });
                    if (station == null)
                        continue;
                    // 写入defect position
                    var result = station.WriteValue(resultItem.GuidLine, (sbyte)2);
                    if (!string.IsNullOrEmpty(result))
                        return result;
                    // 写入part type
                    result = station.WriteValue(((StationMonitor)station).Entity.PartType.TagAddress, (Int32)partType);
                    LogHelper.Log.LogInfo($"{order_code} - Part Type: {partType}", LogHelper.LogType.Information);
                    returnMsg.Add(_position);
                }
                LogHelper.Log.LogInfo($"{order_code} - {returnMsg}", LogHelper.LogType.Information);
                return string.Join(",", returnMsg);
            }
            catch (Exception ex)
            {
                LogHelper.Log.LogInfo(ex, LogHelper.LogType.Exception);
                return ex.Message;
            }
        }
    }
}
