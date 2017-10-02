using System;
using System.Net.Http;
using System.Web.Http.Routing;
using TodoList.Contracts.Api;

namespace TodoList.Api.Static_Wrappers
{
    internal class LocationHelper : ILocationHelper
    {
        private readonly UrlHelper _urlHelper;

        public LocationHelper(HttpRequestMessage requestMessage)
        {
            _urlHelper = new UrlHelper(requestMessage);
        }

        public Uri GetNodeLocation(Guid id)
            => new Uri(_urlHelper.Route("nodes", new { id }), UriKind.Relative);
    }
}