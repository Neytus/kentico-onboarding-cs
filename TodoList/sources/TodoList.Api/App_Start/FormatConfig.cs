using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace TodoList.Api
{
    internal static class FormatConfig
    {
        internal static void Register(HttpConfiguration config) 
            => config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
}