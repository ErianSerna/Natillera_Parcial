using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Natillera_Parcial.Clases;

namespace Natillera_Parcial
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MessageHandlers.Add(new TokenValidationHandler());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
