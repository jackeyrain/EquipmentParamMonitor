using ProjectArrow.Entity;
using System;
using System.Collections.Generic;

namespace ProjectArrow.Models
{
    public class AssemblyHelper
    {
        public List<MES_TM_BAS_ASSEMBLY_LINE> GetAssemblyLine()
        {
            var lines = DBHelper.Db.Select<MES_TM_BAS_ASSEMBLY_LINE>().
                Where(o => o.VALID_FLAG.Value)
                .ToList();
            return lines;
        }
    }
}