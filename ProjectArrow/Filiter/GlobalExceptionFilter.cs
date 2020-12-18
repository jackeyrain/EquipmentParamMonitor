using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectArrow.Filiter
{
    public class GlobalExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.RedirectToRoute(new { Controller = "GlobalException", Action = "Index" });
            return;
        }
    }
}