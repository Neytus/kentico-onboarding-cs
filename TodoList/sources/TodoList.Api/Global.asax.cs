﻿using System.Web.Http;

namespace TodoList.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(RoutesConfig.Register);
            GlobalConfiguration.Configure(FormatConfig.Register);
            GlobalConfiguration.Configure(DependencyInjectionConfig.Register);

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}