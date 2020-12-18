using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .OrderBy(o => o.ID)
                .ToList();
            return tasks;
        }

        public List<MES_ProjectArrow> GetTask(long id)
        {
            var task = DBHelper.Db.Select<MES_ProjectArrow>()
               .Where(o => o.ID == id)
               .First();
            if (task.SERIAL_ID.HasValue)
            {
                var tasks = DBHelper.Db.Select<MES_ProjectArrow>()
                    .Where(o => o.SERIAL_ID == task.SERIAL_ID)
                    .ToList();
                return tasks;
            }
            else
            {
                return new List<MES_ProjectArrow> { task };
            }
        }

        public bool UpdateTask(int id, int status)
        {
            var tasks = this.GetTask((long)id);
            tasks.ForEach(o =>
            {
                o.STATUS = status;
            });

            return DBHelper.Db.Update<MES_ProjectArrow>()
                  .SetSource(tasks)
                  .ExecuteAffrows() > 0;
        }

        public Task<bool> BarrelLeave(int equipId)
        {
            return new TaskFactory().StartNew(() =>
            {
                var tasks = DBHelper.Db.Select<MES_ProjectArrow>()
                                       .Where(o => o.EQUIPID == equipId).Where(o => o.STATUS == 1)
                                       .ToList();
                if (tasks.Count <= 0) return false;

                return DBHelper.Db.Update<MES_ProjectArrow>()
                    .SetSource(tasks)
                    .Set(o => o.STATUS, 99)
                    .ExecuteAffrows() > 0;
            });
        }
    }
}