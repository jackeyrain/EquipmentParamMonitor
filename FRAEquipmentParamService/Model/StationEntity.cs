using DPToleranceMonitorService.Model.DB;
using FRAEquipmentParamService.Access;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FRAEquipmentParamService.Model
{
    public class StationEntity
    {
        public StationEntity()
        {
            this.LineCode = ConfigurationManager.AppSettings["LineCode"];
        }
        public string Name { get; set; }
        public string OPCConnectionStr { get; set; }
        public List<ParamEntity> ParamSet { get; set; }
        public NodeEntity RunningNode { get; set; }
        public NodeEntity PalletNode { get; set; }

        public string LineCode { get; set; }

        [JsonIgnore]
        public MES_TP_FRA_Pallet MES_TP_FRA_Pallet;

        public IList<ParamEntity> GetFailResult()
        {
            var failSet = this.ParamSet.Where(o => !o.ResultCheck()).ToList();
            return failSet;
        }

        public void GetPallet()
        {
            MES_TP_FRA_Pallet = DBAccess.Instance.Select<MES_TP_FRA_Pallet>()
                .Where(o => o.PalletID.Equals(PalletNode.Value.ToString()) && o.LineCode.Equals(this.LineCode))
                .First();
        }

        internal void Initialize()
        {
            MES_TP_FRA_Pallet = null;
        }
    }
}
