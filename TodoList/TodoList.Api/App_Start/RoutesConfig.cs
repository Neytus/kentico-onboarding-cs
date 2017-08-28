using System.Web.Http;

namespace TodoList.Api
{
    public static class RoutesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultPostRoute",
                "api/v1/nodes/{text}"
            );
        }
    }
}