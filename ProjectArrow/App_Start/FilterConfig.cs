﻿using ProjectArrow.Filiter;
using System.Web;
using System.Web.Mvc;

namespace ProjectArrow
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalExceptionFilter());
        }
    }
}
