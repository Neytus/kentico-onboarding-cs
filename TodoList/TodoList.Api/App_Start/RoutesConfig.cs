using System.Runtime.Remoting.Contexts;
using System.Web.Http;

namespace TodoList.Api
{
    public static class RoutesConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}