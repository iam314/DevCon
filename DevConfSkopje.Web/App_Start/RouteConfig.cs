using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevConfSkopje.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "GlobalNotFoundError",
               "NotFound",
               new { controller = "Base", action = "PageNotFound" }
            );

            routes.MapRoute(
             "GlobalError",
             "Error",
             new { controller = "Base", action = "GlobalError" }
            );
            
            routes.MapRoute(
              "RegSuccess",
              "Success",
              new { controller = "Base", action = "SuccessRegistration"}
              );

            routes.MapRoute(
              "Admin",
              "Administration",
              new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DevConf", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
