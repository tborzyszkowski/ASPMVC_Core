using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MidiMarket
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //// Stronnicowanie Produktow
            //routes.MapRoute(
            //    name: "Products Paging",
            //    url: "Products/{page}",
            //    defaults: new { controller = "Products", action = "Index"}
            //);

            //// Filtrowanie kategorii
            //routes.MapRoute(
            //    name: null,
            //    url: "Products/{category}",
            //    defaults: new { controller = "Products", action = "Index", page = 1 }
            //);

            //// Filtrowanie kategorii plus stronnicowanie
            //routes.MapRoute(
            //    name: null,
            //    url: "Products/{category}/{page}",
            //    defaults: new { controller = "Products", action = "Index" }
            //);

            // routing domyslny
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
