using ProjectArrow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectArrow.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // var r = TempData["message"];
            return View();
        }
        [HttpPost]
        public ActionResult Login(string txtAccount, string txtPassword)
        {
            if (new LoginHelper().Login(txtAccount, txtPassword))
            {
                this.Request.RequestContext.HttpContext.Session["account"] = txtAccount;
                return RedirectToAction("Index", "ProjectArrow", false);
            }
            TempData["message"] = "Login Fail!";
            return RedirectToAction(nameof(Index), false);
        }
    }
}