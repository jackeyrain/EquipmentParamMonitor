using FRAEquipmentParamService.Access;
using FRAEquipmentParamService.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRAEquipmentParamService.Implement
{
    public class StationDataConvert
    {
        private readonly string filePath;

        public StationDataConvert(string filePath)
        {
            this.filePath = filePath;
        }

        public StationEntity DataParse()
        {
            var entity = new StationEntity();

            var tags = ExcelHelper.ExcelToDataTable(filePath, "Tag", true);
            entity.Name = tags.Rows[0]["STATION_NAME"].ToString();
            entity.ParamSet = new List<ParamEntity>();

            var previousParam = string.Empty;
            var param = new ParamEntity();
            foreach (DataRow d in tags.Rows)
            {
                if (!previousParam.Equals(d["PARAMETER_NAME"].ToString()))
                {
                    param = new ParamEntity();
                    param.Name = d["PARAMETER_NAME"].ToString();
                    param.TagAddress = new List<NodeEntity>();
                    entity.ParamSet.Add(param);
                }

                param.TagAddress.Add(new NodeEntity { TagAddress = d["TAG_ADDRESS"].ToString(), Flag = d["FLAG"].ToString(), GuidLine = d["GUIDLINE"].ToString() });
                previousParam = d["PARAMETER_NAME"].ToString();
            }

            var config = ExcelHelper.ExcelToDataTable(filePath, "Config", false);
            foreach (DataRow d in config.Rows)
            {
                switch (d[0].ToString())
                {
                    case "OPC_SERVER":
                        entity.OPCConnectionStr = d[1].ToString();
                        break;
                    case "RUNNING_TAG":
                        entity.RunningNode = new NodeEntity { TagAddress = d[1].ToString(), Flag = "RUNNING" };
                        break;
                    case "PALLETID_TAG":
                        entity.PalletNode = new NodeEntity { TagAddress = d[1].ToString(), Flag = "PALLET" };
                        break;
                    case "PART_TYPE":
                        entity.PartType = new NodeEntity { TagAddress = d[1].ToString(), Flag = "PARTTYPE" };
                        break;
                }
            }

            return entity;
        }

        public LoadStationEntity LoadDataParse()
        {
            var entity = new LoadStationEntity();
            var config = ExcelHelper.ExcelToDataTable(filePath, "Config", false);
            entity.Name = "LoadStation";
            foreach (DataRow d in config.Rows)
            {
                switch (d[0].ToString())
                {
                    case "OPC_SERVER":
                        entity.OPCConnectionStr = d[1].ToString();
                        break;
                    case "PALLET_ID":
                        entity.PalletID = new NodeEntity { TagAddress = d[1].ToString(), Flag = "PALLET_ID" };
                        break;
                    case "WORK_ORDER":
                        entity.WorkOrder = new NodeEntity { TagAddress = d[1].ToString(), Flag = "WORK_ORDER" };
                        break;
                    case "WORK_REQUIRE":
                        entity.Ready = new NodeEntity { TagAddress = d[1].ToString(), Flag = "WORK_REQUIRE" };
                        break;
                }
            }

            return entity;
        }
    }
}
