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

        public MES_TT_APS_WORK_ORDER MES_TT_APS_WORK_ORDER;

        public IList<ParamEntity> GetFailResult()
        {
            var failSet = this.ParamSet.Where(o => !o.ResultCheck()).ToList();
            return failSet;
        }

        public bool GetPallet()
        {
            MES_TP_FRA_Pallet = DBAccess.Instance.Select<MES_TP_FRA_Pallet>()
                .Where(o => o.PalletID.Equals(PalletNode.Value.ToString()) && o.LineCode.Equals(this.LineCode))
                .First();

            if (MES_TP_FRA_Pallet != null)
            {
                MES_TT_APS_WORK_ORDER = DBAccess.Instance.Select<MES_TT_APS_WORK_ORDER>()
                    .WithLock(SqlServerLock.NoLock)
                    .Where(o => o.ORDER_CODE.Equals(MES_TP_FRA_Pallet.WorkOrder, StringComparison.OrdinalIgnoreCase))
                    .IncludeMany(o => o.mES_TT_APS_WORK_ORDER_ASSEMBLies.Where(p => p.ORDER_ID == o.ID))
                    .First();
                return MES_TT_APS_WORK_ORDER != null;
            }
            return false;
        }

        internal void Initialize()
        {
            MES_TP_FRA_Pallet = null;
            MES_TT_APS_WORK_ORDER = null;
        }
    }
}
