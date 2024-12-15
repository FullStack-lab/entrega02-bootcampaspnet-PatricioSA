using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Forun
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CommentDetails",
                url: "comentarios/{id}/{author}",
                defaults: new { controller = "Forum", action = "CommentDetails", author = UrlParameter.Optional },
                constraints: new { id = @"\d+" } // Garante que o id seja numérico
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Forum", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
