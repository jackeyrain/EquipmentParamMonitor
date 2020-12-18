using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectArrow.Filiter
{
    public class SessionVerifyFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (string.IsNullOrEmpty(filterContext.RequestContext.HttpContext.Session["account"] as string))
            {
                filterContext.HttpContext.Response.RedirectToRoute(new { Controller = "Home", Action = "Index" });
                return;
            }

            base.OnActionExecuted(filterContext);
        }
    }
}