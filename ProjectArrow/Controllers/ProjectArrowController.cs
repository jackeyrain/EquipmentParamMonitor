using ProjectArrow.Entity;
using ProjectArrow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectArrow.Controllers
{
    public class ProjectArrowController : Controller
    {
        // GET: ProjectArrow
        public ActionResult Index()
        {
            var lines = this.GetStation();
            ViewBag.Lines = lines;
            return View();
        }
        [HttpGet]

        public JsonResult GetArrowTask(string assemblyLine)
        {
            var task = new TaskHelper().GetTasks(assemblyLine).Select(o => new { Order = o.ORDERNUMBER, Id = o.ID, PartNo = o.PARTNUMBER, PartName = o.PARTNAME }).ToList();
            return Json(task, JsonRequestBehavior.AllowGet);
        }

        private List<SelectEntity> GetStation()
        {
            var lines = new AssemblyHelper().GetAssemblyLine().Select(o => new SelectEntity { Name = o.ASSEMBLY_LINE_NAME, Value = o.ASSEMBLY_LINE }).ToList();
            return lines;
        }
        [HttpGet]
        public ActionResult ArrowDo(long id)
        {
            var task = new TaskHelper().GetTask(id);
            if (task != null)
            {
                return View(task);
            }
            else
            {
                return Redirect(nameof(Index));
            }
        }
    }
}