using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Irving.Web.Attributes;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.Mvc;

namespace Irving.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        [ExcludeFromCodeCoverage]
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CopyModelStateAttribute());
        }

        [ExcludeFromCodeCoverage]
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CreateAssetPost",
                url: "Asset/Create",
                defaults: new { controller = "Asset", action = "CreateAsset" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );
            
            routes.MapRoute(
                name: "EditAssetPost",
                url: "Asset/Edit/{id}",
                defaults: new { controller = "Asset", action = "EditAsset" },
                constraints: new { httpMethod =  new HttpMethodConstraint("POST") }
            );

            routes.MapRoute(
                name: "ListAsset",
                url: "Asset/List",
                defaults: new { controller = "Home", action = "Dashboard" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Dashboard", id = UrlParameter.Optional }
            );
        }

        [ExcludeFromCodeCoverage]
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            BundleTable.Bundles.RegisterTemplateBundles();
            FluentValidationModelValidatorProvider.Configure();
        }
    }
}