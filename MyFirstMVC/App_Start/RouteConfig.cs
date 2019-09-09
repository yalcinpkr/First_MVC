using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyFirstMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("hakkimdaRoute", "Hakkimda", new { controller = "Home", action = "About" }, namespaces: new string[] { "MyFirstMVC.Controllers" });
            routes.MapRoute("projelerimRoute", "Projelerim", new { controller = "Home", action = "Project" },namespaces: new string[] { "MyFirstMVC.Controllers" });
            routes.MapRoute("iletisimRoute", "Iletisim", new { controller = "Home", action = "Contact" }, namespaces: new string[] { "MyFirstMVC.Controllers" });
            routes.MapRoute("cerezkullanimiRoute", "CerezKullanimi", new { controller = "Home", action = "Cookies" }, namespaces: new string[] { "MyFirstMVC.Controllers" });
            routes.MapRoute("kullanimkosullariRoute", "KullanimKosullari", new { controller = "Home", action = "TermsofUse" }, namespaces: new string[] { "MyFirstMVC.Controllers" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces:new string[] {"MyFirstMVC.Controllers"}
            );
        }
    }
}
