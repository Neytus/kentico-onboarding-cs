using System.Web.Http;

namespace TodoList.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(RoutesConfig.Register);
            GlobalConfiguration.Configure(FormatConfig.Register);
        }
    }
}
