﻿using System.Web;
using System.Web.Mvc;

namespace Alliance_for_Life
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
