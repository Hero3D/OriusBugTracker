using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Orius
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Reports",
               url: "Reports",
               defaults: new { controller = "Tickets", action = "Index"}
           );

            routes.MapRoute(
               name: "Profile",
               url: "User/ViewProfile/{username}",
               defaults: new { controller = "User", action = "ViewProfile", username = UrlParameter.Optional }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
