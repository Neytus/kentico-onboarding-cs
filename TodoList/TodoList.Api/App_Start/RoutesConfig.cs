using System.Web.Http;

namespace TodoList.Api
{
    public static class RoutesConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}