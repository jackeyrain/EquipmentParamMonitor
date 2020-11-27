using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectArrow.Entity;

namespace ProjectArrow.Models
{
    public class TaskHelper
    {
        public List<MES_ProjectArrow> GetTasks(string assemblyLine)
        {
            var tasks = DBHelper.Db.Select<MES_ProjectArrow>()
                .Where(o => o.VALID_FLAG.Value &&
                o.ASSEMBLYLINE.Equals(assemblyLine, StringComparison.OrdinalIgnoreCase) &&
                o.STATUS == 0)
                .OrderBy(o=>o.ID)
                .ToList();
            return tasks;
        }

        public MES_ProjectArrow GetTask(long id)
        {
            var tasks = DBHelper.Db.Select<MES_ProjectArrow>()
               .Where(o => o.ID == id)
               .First();
            return tasks;
        }
    }
}