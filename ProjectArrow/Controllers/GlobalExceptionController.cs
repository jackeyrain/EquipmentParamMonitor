using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectArrow.Controllers
{
    public class GlobalExceptionController : Controller
    {
        // GET: GlobalException
        public ActionResult Index()
        {
            return View("GlobalException");
        }
    }
}