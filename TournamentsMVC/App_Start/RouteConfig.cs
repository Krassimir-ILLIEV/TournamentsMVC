using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TournamentsMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");                       

            routes.MapRoute(
               name: "Rate",
               url: "player/rate",
               defaults: new { controller = "Player", action = "Rate" }
           );

            routes.MapRoute(
               name: "Player",
               url: "player/{id}",
               defaults: new { controller = "Player", action = "Index" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
