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
        private const string FirstGuid = "d237bdda-e6d4-4e46-92db-1a7a0aeb9a72";
        private const string SecondGuid = "b84bbcc7-d516-4805-b2e3-20a2df3758a2";
        private const string ThirdGuid = "6171ec89-e3b5-458e-ae43-bc0e8ec061e2";
        private const string FourthGuid = "b61670fd-33ce-400e-a351-f960230e3aae";
        private const string NotFoundGuid = "aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72";

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
            var expectedResult = new NodeModel[]
            {
                new NodeModel {Id = new Guid(FirstGuid), Text = "poopy"},
                new NodeModel {Id = new Guid(SecondGuid), Text = "GEARS"},
                new NodeModel {Id = new Guid(ThirdGuid), Text = "Planet Music"},
                new NodeModel {Id = new Guid(FourthGuid), Text = "Time to get shwifty"}
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
            var expectedResult = new NodeModel {Id = new Guid(FirstGuid), Text = "poopy"};

            var createdResponse = await Controller.GetAsync(FirstGuid);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel {Id = new Guid(FirstGuid), Text = "poopy"};

            var createdResponse = await Controller.GetAsync(NotFoundGuid);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = new NodeModel {Id = new Guid(SecondGuid), Text = "GEARS"};

            var createdResponse = await Controller.PostAsync("TEST TEXT");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Put_UpdatesACorrectNode()
        {
            var expectedResult = new NodeModel {Id = new Guid(ThirdGuid), Text = "Planet Music"};

            var createdResponse = await Controller.PutAsync(ThirdGuid, "Planet Music");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Put_ActsLikeItUpdatedSomeNode()
        {
            var expectedResult = new NodeModel {Id = new Guid(ThirdGuid), Text = "Planet Music"};

            var createdResponse = await Controller.PutAsync(NotFoundGuid, "Planet Music");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Delete_DeletesCorrectNode()
        {
            var actualResponse = await Controller.DeleteAsync(FourthGuid).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Delete_ActsLikeItDeletedSomeNode()
        {
            var actualResponse = await Controller.DeleteAsync(NotFoundGuid).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}