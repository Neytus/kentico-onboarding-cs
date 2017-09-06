using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using NSubstitute;
using NUnit.Framework;
using TodoList.Contracts.Api;

namespace TodoList.Api.Tests.Helpers
{
    [TestFixture]
    public class LocationHelperTest
    {
        private static readonly Guid TestedId = new Guid("aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72");

        private HttpRequestMessage _httpRequestMessage;
        private ILocationHelper _locationHelper;

        [SetUp]
        public void SetUp()
        {
            _httpRequestMessage = ConfigureRequestMessage(TestedId);
            _locationHelper = Substitute.For<ILocationHelper>();

            _locationHelper.GetLocation(_httpRequestMessage, TestedId)
                .Returns("api/v1/nodes/aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72");
        }

        [Test]
        public void ReturnsCorrectUrl()
        {
            var expectedUrl = "api/v1/nodes/aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72";

            var actualUrl = _locationHelper.GetLocation(_httpRequestMessage, TestedId);

            Assert.That(expectedUrl, Is.EqualTo(actualUrl));
        }

        private HttpRequestMessage ConfigureRequestMessage(Guid id)
        {
            _httpRequestMessage = new HttpRequestMessage();
            var configuration = new HttpConfiguration();
            var route = configuration.Routes.MapHttpRoute(
                name: "nodes",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
            var routeData = new HttpRouteData(route,
                new HttpRouteValueDictionary
                {
                    {"id", id},
                    {"controller", "Nodes"}
                }
            );
            _httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);
            _httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            return _httpRequestMessage;
        }
    }
}