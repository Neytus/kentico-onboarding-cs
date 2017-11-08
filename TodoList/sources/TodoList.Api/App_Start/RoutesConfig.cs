using System.Web.Http;

namespace TodoList.Api
{
    internal static class RoutesConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}