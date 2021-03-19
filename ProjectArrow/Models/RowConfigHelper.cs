using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectArrow.Entity;

namespace ProjectArrow.Models
{
    public class RowConfigHelper
    {
        public List<MaterialEntity> GetRawMaterial(int taskId)
        {
            var tasks = new TaskHelper().GetTask(taskId);

            var data = DBHelper.Db.Select<MES_TM_BAS_RAW_PACKAGE_DETAIL>()
                .WithSql($@"select c.* from MES.ProjectArrow a
                              left join mes.TM_BAS_RAW_PACKAGE b on a.PARTNUMBER = b.NAME and b.PACKAGE_TYPE = 70
                              left join mes.TM_BAS_RAW_PACKAGE_DETAIL c on b.FID = c.PACKAGE_FID
                              where a.ID in ( {string.Join(",", tasks.Select(o => o.ID).ToArray())} )")
                .ToList();
            var entity = data.Select(o => new MaterialEntity { BarcodeRule = o.BARCODE }).ToList();
            return entity;
        }

        public List<HoleEntity> GetHole(int equipId)
        {
            var data = DBHelper.Db.Select<MES_TM_BAS_RAW_PACKAGE_DETAIL>()
                .WithSql(@"SELECT b.* from MES.TM_BAS_RAW_PACKAGE a 
                              inner join MES.TM_BAS_RAW_PACKAGE_DETAIL b on a.FID = b.PACKAGE_FID and b.VALID_FLAG = 1
                              inner join MES.TM_BAS_EQUIPMENT c on b.EQUIP_FID = c.FID and c.VALID_FLAG = 1
                              where a.VALID_FLAG = 1 AND a.PACKAGE_TYPE = 40 and c.ID = @EquipID", new { EquipID = equipId })
                .ToList(o => new HoleEntity { BarcodeRule = o.BARCODE });

            return data;
        }

        public List<Barrer> GetBarrers()
        {
            var data = DBHelper.Db.Select<MES_TM_BAS_RAW_PACKAGE>()
                .Where(o => o.VALID_FLAG.Value && o.PACKAGE_TYPE.Value == 10)
                .IncludeMany(o => o.mES_TM_BAS_RAW_PACKAGE_DETAILs.Where(p => o.FID == p.PACKAGE_FID).Where(p => p.VALID_FLAG.Value))
                .ToList();
            List<Barrer> barrers = new List<Barrer>();
            foreach (var d in data)
            {
                foreach (var dd in d.mES_TM_BAS_RAW_PACKAGE_DETAILs)
                {
                    var barrer = new Barrer();
                    barrer.Name = d.NAME;
                    barrer.EquipId = (int)DBHelper.Db.Select<MES_TM_BAS_EQUIPMENT>()
                           .Where(o => o.FID.Value == dd.EQUIP_FID).Where(o => o.VALID_FLAG.Value)
                           .First().ID;
                    barrer.EmptyValue = Convert.ToInt32(dd.BARCODE);
                    barrer.TagAddress = dd.REMARK;
                    barrers.Add(barrer);
                }
            }
            return barrers;
        }
    }
}