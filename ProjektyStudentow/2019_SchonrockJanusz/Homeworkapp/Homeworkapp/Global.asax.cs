﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Homeworkapp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["userCount"] = 0;
            Application["activeUserCount"] = 0;
        }

        protected void Session_Start()
        {
            int userCount = (int)Application["userCount"];
            userCount++;
            Application["userCount"] = userCount;

            int activeUserCount = (int)Application["activeUserCount"];
            activeUserCount++;
            Application["activeUserCount"] = activeUserCount;
        }

        protected void Session_End()
        {
            int activeUserCount = (int)Application["activeUserCount"];
            activeUserCount--;
            Application["activeUserCount"] = activeUserCount;
        }
    }
}
