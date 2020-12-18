using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectArrow.Entity;
using ProjectArrow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectArrow.Filiter;

namespace ProjectArrow.Controllers
{
    [SessionVerifyFilter]
    public class ProjectArrowController : Controller
    {
        private int index { get; set; } = 0;
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
            var tasks = new TaskHelper().GetTask(id);
            if (tasks != null)
            {
                var material = new RowConfigHelper().GetRawMaterial(Convert.ToInt32(id));
                var hole = new RowConfigHelper().GetHole(Convert.ToInt32(tasks.First().EQUIPID.Value));

                ViewBag.Material = material;
                ViewBag.Hole = hole;

                return View(tasks);
            }
            else
            {
                return Redirect(nameof(Index));
            }
        }

        [HttpGet]
        public JsonResult Regex(string rule, string barcode)
        {
            bool result;
            try { result = this.RegexBarcodeRule(rule, barcode); }
            catch { result = false; }
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        private bool RegexBarcodeRule(string rule, string barcode)
        {
            Boolean matched = false;
            Int32 inputIndex = 0;
            Int32 patternIndex = 0;

            //无通配符 * 时，比较算法（）
            while (inputIndex < barcode.Length && patternIndex < rule.Length && (rule[patternIndex] != '*'))
            {
                if ((rule[patternIndex] != '?') && (barcode[inputIndex] != rule[patternIndex]))
                {//如果模式字符不是通配符，且输入字符与模式字符不相等，则可判定整个输入字串与模式不匹配
                    return matched;
                }
                patternIndex++;
                inputIndex++;
                if (patternIndex == rule.Length && inputIndex < barcode.Length)
                {
                    return matched;
                }
                if (inputIndex == barcode.Length && patternIndex < rule.Length)
                {
                    return matched;
                }
                if (patternIndex == rule.Length && inputIndex == barcode.Length)
                {
                    matched = true;
                    return matched;
                }
            }
            //有通配符 * 时，比较算法
            Int32 mp = 0;
            Int32 cp = 0;
            while (inputIndex < barcode.Length)
            {
                if (patternIndex < rule.Length && rule[patternIndex] == '*')
                {
                    if (++patternIndex >= rule.Length)
                    {
                        matched = true;
                        return matched;
                    }
                    mp = patternIndex;
                    cp = inputIndex + 1;
                }
                else if (patternIndex < rule.Length && ((rule[patternIndex] == barcode[inputIndex]) || (rule[patternIndex] == '?')))
                {
                    patternIndex++;
                    inputIndex++;
                }
                else
                {
                    patternIndex = mp;
                    inputIndex = cp++;
                }
            }
            //当输入字符为空且模式为*时
            while (patternIndex < rule.Length && rule[patternIndex] == '*')
            {
                patternIndex++;
            }
            return patternIndex >= rule.Length ? true : false;

        }
        [HttpPost]
        public JsonResult ProjectArrowSubmit(int taskId, string dataStr)
        {
            var array = JsonConvert.DeserializeObject(dataStr) as JArray;
            List<string> material = new List<string>();
            List<string> serial = new List<string>();
            List<string> hole = new List<string>();
            for (int i = 0; i < array.Count(); i++)
            {
                switch (array[i]["c"].Value<string>())
                {
                    case "m":
                        material.Add(array[i]["d"].Value<string>());
                        break;
                    case "s":
                        serial.Add(array[i]["d"].Value<string>());
                        break;
                    case "h":
                        hole.Add(array[i]["d"].Value<string>());
                        break;
                }
            }
            List<MES_TT_PC_RAW_PART_CHARGING> data = new List<MES_TT_PC_RAW_PART_CHARGING>();
            for (int i = 0; i < material.Count(); i++)
            {
                data.Add(new MES_TT_PC_RAW_PART_CHARGING
                {
                    FID = Guid.NewGuid(),
                    PACKAGE_BARCODE = material[i],
                    PART_BARCODE = material[i],
                    BATCH_NO = serial[i],
                    VALID_FLAG = true,
                    PLANT = "HLP",
                    CREATE_USER = this.Request.RequestContext.HttpContext.Session["account"].ToString(),
                    CREATE_DATE = DateTime.Now,
                }); ;
            }
            data.AddRange(Array.ConvertAll(hole.ToArray(), o => new MES_TT_PC_RAW_PART_CHARGING
            {
                FID = Guid.NewGuid(),
                PACKAGE_BARCODE = o,
                PART_BARCODE = o,
                VALID_FLAG = true,
                PLANT = "HLP",
                CREATE_USER = this.Request.RequestContext.HttpContext.Session["account"].ToString(),
                CREATE_DATE = DateTime.Now,
            }));

            var result = DBHelper.Db.Insert<MES_TT_PC_RAW_PART_CHARGING>(data).ExecuteAffrows() > 0 &&
                            new TaskHelper().UpdateTask(Convert.ToInt32(taskId), 1);

            return Json(new { result = result });
        }
    }
}