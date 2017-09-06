using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoList.Contracts.Api;

namespace TodoList.Api.Helpers
{
    internal class LocationHelper : ILocationHelper
    {
        public string GetLocation(HttpRequestMessage httpRequestMessage, Guid id)
        {
            return new UrlHelper(httpRequestMessage).Route("nodes", new { id });
        }
    }
}