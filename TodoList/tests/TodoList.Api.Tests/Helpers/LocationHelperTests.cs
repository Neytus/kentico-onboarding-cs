using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using NSubstitute;
using NUnit.Framework;
using TodoList.Api.Static_Wrappers;
using TodoList.Contracts.Api;

namespace TodoList.Api.Tests.Helpers
{
    [TestFixture]
    public class LocationHelperTests
    {
        private static readonly Guid TestedId = new Guid("aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72");

        private HttpRequestMessage _httpRequestMessage;
        private ILocator _locator;

        [SetUp]
        public void SetUp()
        {
            _httpRequestMessage = Substitute.For<HttpRequestMessage>();

            _locator = new Locator(_httpRequestMessage);
        }

        [Test]
        public void GetNodeLocation_WithCorrectId_ReturnsCorrectUrl()
        {
            ConfigureRequestMessage(TestedId);
            var expectedUrl = "/my/awesome/shwifty/nodes/" + TestedId;

            var actualUrl = _locator.GetNodeLocation(TestedId);

            Assert.That(expectedUrl, Is.EqualTo(actualUrl));
        }

        private void ConfigureRequestMessage(Guid id)
        {
            var configuration = new HttpConfiguration();
            var route = configuration.Routes.MapHttpRoute(
                name: "nodes",
                routeTemplate: "my/awesome/shwifty/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
            var routeData = new HttpRouteData(route,
                new HttpRouteValueDictionary
                {
                    {"id", id},
                    {"controller", "nodes"}
                }
            );

            _httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, configuration);
            _httpRequestMessage.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
        }
    }
}