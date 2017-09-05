using System;
using System.Net.Http;
using System.Web.Http.Routing;

namespace TodoList.Api.Helpers
{
    internal class LocationHelper : ILocationHelper
    {
        private readonly HttpRequestMessage _httpRequestMessage;

        public LocationHelper(HttpRequestMessage message)
        {
            _httpRequestMessage = message;
        }

        public string GetLocation(Guid id)
        {
            return new UrlHelper(_httpRequestMessage).Route("nodes", new {id});
        } 
    }
}