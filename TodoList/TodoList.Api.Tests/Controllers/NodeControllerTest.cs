using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Models;
using TodoList.Api.Tests.Util;

namespace TodoList.Api.Tests.Controllers
{
    [TestFixture]
    internal class NodeControllerTest
    {
        private const string FirstGuidString = "d237bdda-e6d4-4e46-92db-1a7a0aeb9a72";
        private const string SecondGuidString = "b84bbcc7-d516-4805-b2e3-20a2df3758a2";
        private const string ThirdGuidString = "6171ec89-e3b5-458e-ae43-bc0e8ec061e2";
        private const string FourthGuidString = "b61670fd-33ce-400e-a351-f960230e3aae";
        private const string NotFoundGuidString = "aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72";

        public NodeController Controller;

        [SetUp]
        public void SetUp()
        {
            Controller = new NodeController();
            SetupControllerForTests(Controller);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage();
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/v1/nodes");
            var routeData = new HttpRouteData(route);

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public async Task Get_ReturnsAllNodes()
        {
            var expectedResult = new NodeModel[]
            {
                new NodeModel {Id = new Guid(FirstGuidString), Text = "poopy"},
                new NodeModel {Id = new Guid(SecondGuidString), Text = "GEARS"},
                new NodeModel {Id = new Guid(ThirdGuidString), Text = "Planet Music"},
                new NodeModel {Id = new Guid(FourthGuidString), Text = "Time to get shwifty"}
            };

            var createdResponse = await Controller.GetAsync();
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel[] actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparerWrapper.Comparer));
        }

        [Test]
        public async Task GetWithId_ReturnsCorrectNode()
        {
            var expectedResult = new NodeModel {Id = new Guid(FirstGuidString), Text = "poopy"};

            var createdResponse = await Controller.GetAsync(FirstGuidString);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel {Id = new Guid(FirstGuidString), Text = "poopy"};

            var createdResponse = await Controller.GetAsync(NotFoundGuidString);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = new NodeModel {Id = new Guid(SecondGuidString), Text = "GEARS"};

            var createdResponse = await Controller.PostAsync("TEST TEXT");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Put_UpdatesACorrectNode()
        {
            var expectedResult = new NodeModel {Id = new Guid(ThirdGuidString), Text = "Planet Music"};

            var createdResponse = await Controller.PutAsync(ThirdGuidString, "Planet Music");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Put_ActsLikeItUpdatedSomeNode()
        {
            var expectedResult = new NodeModel {Id = new Guid(ThirdGuidString), Text = "Planet Music"};

            var createdResponse = await Controller.PutAsync(NotFoundGuidString, "Planet Music");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Delete_DeletesCorrectNode()
        {
            var actualResponse = await Controller.DeleteAsync(FourthGuidString).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Delete_ActsLikeItDeletedSomeNode()
        {
            var actualResponse = await Controller.DeleteAsync(NotFoundGuidString).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}