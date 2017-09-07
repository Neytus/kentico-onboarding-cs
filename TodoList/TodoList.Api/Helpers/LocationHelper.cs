using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoList.Contracts.Api;

namespace TodoList.Api.Helpers
{
    internal class LocationHelper : ILocationHelper
    {
        private readonly HttpRequestMessage _requestMessage;

        public LocationHelper(HttpRequestMessage requestMessage)
        {
            _requestMessage = requestMessage;
        }

        public string GetLocation(Guid id)
        {
            return new UrlHelper(_requestMessage).Route("nodes", new { id });
        }
    }
}