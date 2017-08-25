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
        private static readonly Guid FirstId = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72");
        private static readonly Guid SecondId = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2");
        private static readonly Guid ThirdId = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2");
        private static readonly Guid FourthId = new Guid("b61670fd-33ce-400e-a351-f960230e3aae");
        private static readonly string NotFoundStringId = "aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72";

        public NodesController Controller;

        [SetUp]
        public void SetUp()
        {
            Controller = new NodesController();
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
            var expectedResult = new[]
            {
                new NodeModel {Id = FirstId, Text = "poopy"},
                new NodeModel {Id = SecondId, Text = "GEARS"},
                new NodeModel {Id = ThirdId, Text = "Planet Music"},
                new NodeModel {Id = FourthId, Text = "Time to get shwifty"}
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
            var expectedResult = new NodeModel {Id = FirstId, Text = "poopy"};

            var createdResponse = await Controller.GetAsync("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel {Id = FirstId, Text = "poopy"};

            var createdResponse = await Controller.GetAsync(NotFoundStringId);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = new NodeModel {Id = SecondId, Text = "GEARS"};

            var createdResponse = await Controller.PostAsync("TEST TEXT");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Put_UpdatesACorrectNode()
        {
            var expectedResult = new NodeModel {Id = ThirdId, Text = "Planet Music"};

            var createdResponse = await Controller.PutAsync("6171ec89-e3b5-458e-ae43-bc0e8ec061e2", "Planet Music");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Put_ActsLikeItUpdatedSomeNode()
        {
            var expectedResult = new NodeModel {Id = ThirdId, Text = "Planet Music"};

            var createdResponse = await Controller.PutAsync(NotFoundStringId, "Planet Music");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Delete_DeletesCorrectNode()
        {
            var actualResponse = await Controller.DeleteAsync("b61670fd-33ce-400e-a351-f960230e3aae").Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Delete_ActsLikeItDeletedSomeNode()
        {
            var actualResponse = await Controller.DeleteAsync(NotFoundStringId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}