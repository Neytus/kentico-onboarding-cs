using System.Web.Http;

namespace TodoList.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Nodes",
                routeTemplate: "api/v1/nodes/{id}",
                defaults: new { controller = "node", id = RouteParameter.Optional }
            );
        }
    }
}
