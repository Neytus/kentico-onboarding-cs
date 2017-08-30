using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NSubstitute;
using System.Web.Http.Results;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Tests.Util;
using TodoList.Contracts.Api;
using TodoList.Contracts.DAL;

namespace TodoList.Api.Tests.Controllers
{
    [TestFixture]
    internal class NodesControllerTest
    {
        private static readonly Guid FirstId = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72");
        private static readonly Guid SecondId = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2");
        private static readonly Guid ThirdId = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2");
        private static readonly Guid FourthId = new Guid("b61670fd-33ce-400e-a351-f960230e3aae");
        private static readonly Guid NotFoundId = new Guid("aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72");

        public NodesController Controller;

        private INodesRepository MockRepository()
        {
            var repository = Substitute.For<INodesRepository>();

            repository.GetAllAsync().Returns(new[]
            {
                new NodeModel {Id = FirstId, Text = "poopy"},
                new NodeModel {Id = SecondId, Text = "GEARS"},
                new NodeModel {Id = ThirdId, Text = "Planet Music"},
                new NodeModel {Id = FourthId, Text = "Time to get shwifty"}
            });

            repository.GetByIdAsync(FirstId)
                .Returns(new NodeModel {Id = FirstId, Text = "poopy"});

            repository.AddAsync("text").Returns(new NodeModel
            {
                Id = SecondId,
                Text = "GEARS"
            });

            repository.UpdateAsync(ThirdId, "text").Returns(new NodeModel
            {
                Id = ThirdId,
                Text = "Planet Music"
            });

            return repository;
        }

        [SetUp]
        public void SetUp()
        {
            Controller = GetControllerForTests(MockRepository());
        }

        private static NodesController GetControllerForTests(INodesRepository repository)
        {
            return new NodesController(repository)
            {
                ControllerContext = { Configuration = new HttpConfiguration(new HttpRouteCollection()) },
                Request = new HttpRequestMessage { RequestUri = new Uri("api/v1/nodes/", UriKind.Relative) }
            };
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
            Assert.That(expectedResult, Is.EqualTo(actualResult).AsCollection.UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task GetWithId_ReturnsCorrectNode()
        {
            var expectedResult = new NodeModel { Id = FirstId, Text = "poopy" };

            var createdResponse = await Controller.GetAsync(FirstId);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel { Id = FirstId, Text = "poopy" };

            var createdResponse = await Controller.GetAsync(NotFoundId);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Post_InsertsNewNodeCorrectly()
        {
            var controller = new NodesController(MockRepository()) { 
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var createdResponse = await controller.PostAsync("GEARS");

            Assert.That(createdResponse, Is.InstanceOf<CreatedAtRouteNegotiatedContentResult<NodeModel>>());
            Assert.That(((CreatedAtRouteNegotiatedContentResult<NodeModel>) createdResponse).RouteValues["Id"], Is.EqualTo(SecondId.ToString()));
        }

        [Test]
        public async Task Put_UpdatesACorrectNode()
        {
            var expectedResult = new NodeModel { Id = ThirdId, Text = "Planet Music" };

            var createdResponse = await Controller.PutAsync(ThirdId, "Planet Music");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Put_ActsLikeItUpdatedSomeNode()
        {
            var expectedResult = new NodeModel { Id = ThirdId, Text = "Planet Music" };

            var createdResponse = await Controller.PutAsync(NotFoundId, "Planet Music");
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Delete_DeletesCorrectNode()
        {
            var actualResponse = await Controller.DeleteAsync(FourthId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Delete_ActsLikeItDeletedSomeNode()
        {
            var actualResponse = await Controller.DeleteAsync(NotFoundId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}