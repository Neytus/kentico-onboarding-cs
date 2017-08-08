using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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
    public class NodeControllerTest
    {
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
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{text}");
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
                new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"},
                new NodeModel {Id = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2"), Text = "GEARS"},
                new NodeModel {Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"), Text = "Planet Music"},
                new NodeModel {Id = new Guid("b61670fd-33ce-400e-a351-f960230e3aae"), Text = "Time to get shwifty"}
            };

            var createdResponse = await Controller.GetAsync().Result.ExecuteAsync(CancellationToken.None);
            createdResponse.TryGetContentValue(out object actualResult);

            Assert.IsNotNull(createdResponse.Content);
            Assert.That(createdResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public async Task GetWithId_ReturnsCorrectNode()
        {
            var expectedResult = new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"};

            var createdResponse = await Controller.GetAsync("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72").Result
                .ExecuteAsync(CancellationToken.None);
            createdResponse.TryGetContentValue(out object actualResult);

            Assert.IsNotNull(createdResponse.Content);
            Assert.That(createdResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public async Task GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"};

            var createdResponse = await Controller.GetAsync("aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72").Result
                .ExecuteAsync(CancellationToken.None);
            createdResponse.TryGetContentValue(out object actualResult);

            Assert.IsNotNull(createdResponse.Content);
            Assert.That(createdResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public async Task Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = new NodeModel {Id = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2"), Text = "GEARS"};

            var createdResponse = await Controller.PostAsync("TEST TEXT").Result.ExecuteAsync(CancellationToken.None);
            createdResponse.TryGetContentValue(out object actualResult);

            Assert.IsNotNull(createdResponse.Content);
            Assert.That(createdResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public async Task Put_UpdatesACorrectNode()
        {
            var expectedResult = new NodeModel
            {
                Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"),
                Text = "Planet Music"
            };

            var createdResponse = await Controller.PutAsync("6171ec89-e3b5-458e-ae43-bc0e8ec061e2", "Planet Music").Result
                .ExecuteAsync(CancellationToken.None);
            createdResponse.TryGetContentValue(out object actualResult);

            Assert.IsNotNull(createdResponse.Content);
            Assert.That(createdResponse.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public async Task Put_ActsLikeItUpdatedSomeNode()
        {
            var expectedResult = new NodeModel
            {
                Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"),
                Text = "Planet Music"
            };
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new ObjectContent<NodeModel>(expectedResult, new JsonMediaTypeFormatter())
            };

            var createdResponse = await Controller.PutAsync("00018889-e3b5-458e-ab43-bc0e8ec761e2", "Planet Music").Result
                .ExecuteAsync(CancellationToken.None);
            createdResponse.TryGetContentValue(out object actualResult);

            Assert.IsNotNull(createdResponse.Content);
            Assert.That(createdResponse.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));

            // TODO ToString() not the best for comparing HttpResponseMessages
            Assert.That(expectedResponse, Is.EqualTo(createdResponse).Using(HttpResponseMessageEqualityComparer.Instance));
        }

        [Test]
        public async Task Delete_DeletesCorrectNode()
        {
            var actualResponse = await Controller.DeleteAsync("b61670fd-33ce-400e-a351-f960230e3aae").Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNotNull(actualResponse);
            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Delete_ActsLikeItDeletedSomeNode()
        {
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);

            var actualResponse = await Controller.DeleteAsync("00000012-33ce-400e-a351-f960230e3aae").Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNotNull(actualResponse);
            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResponse.ToString(), Is.EqualTo(actualResponse.ToString()));
        }
    }
}