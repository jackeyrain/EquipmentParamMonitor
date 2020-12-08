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
            var data = DBHelper.Db.Select<MES_TM_BAS_RAW_PACKAGE_DETAIL>()
                .WithSql(@"select c.* from MES.ProjectArrow a
                              left join mes.TM_BAS_RAW_PACKAGE b on a.PARTNUMBER = b.NAME and b.PACKAGE_TYPE = 70
                              left join mes.TM_BAS_RAW_PACKAGE_DETAIL c on b.FID = c.PACKAGE_FID
                              where a.ID = @TaskId", new { TaskId = taskId })
                .ToList(o => new MaterialEntity { BarcodeRule = o.BARCODE });

            return data;
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
    }
}