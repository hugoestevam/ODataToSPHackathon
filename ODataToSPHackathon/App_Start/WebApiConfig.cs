using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using ODataToSPHackathon.Models;


namespace ODataToSPHackathon
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<EnterprisesModel>("Enterprises");                        
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
