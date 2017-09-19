using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoList.Contracts.Api;

namespace TodoList.Api.Helpers
{
    internal class LocationHelper : ILocationHelper
    {
        private readonly UrlHelper _urlHelper;

        public LocationHelper(HttpRequestMessage requestMessage)
        {
            _urlHelper = new UrlHelper(requestMessage);
        }

        public string GetNodeLocation(Guid id) 
            => _urlHelper.Route("nodes", new { id });
    }
}