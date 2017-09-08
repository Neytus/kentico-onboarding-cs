using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NSubstitute;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Tests.Util;
using TodoList.Contracts.Api;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Models;

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

        private NodesController _controller;

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
            repository.GetByIdAsync(NotFoundId)
                .Returns(new NodeModel {Id = FirstId, Text = "poopy"});

            repository.UpdateAsync(new NodeModel()).ReturnsForAnyArgs(new NodeModel
            {
                Id = ThirdId,
                Text = "Planet Music"
            });

            return repository;
        }

        private ILocationHelper MockLocationHelper()
        {
            var locationHelper = Substitute.For<ILocationHelper>();
            locationHelper.GetLocation(new Guid()).ReturnsForAnyArgs("api/v1/nodes/id");

            return locationHelper;
        }

        [SetUp]
        public void SetUp()
        {
            _controller = GetControllerForTests(MockRepository(), MockLocationHelper());
        }

        private static NodesController GetControllerForTests(INodesRepository repository,
            ILocationHelper locationHelper)
        {
            return new NodesController(repository, locationHelper)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
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

            var createdResponse = await _controller.GetAsync();
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel[] actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult, Is.EqualTo(actualResult).AsCollection.UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task GetWithId_ReturnsCorrectNode()
        {
            var expectedResult = new NodeModel {Id = FirstId, Text = "poopy"};

            var createdResponse = await _controller.GetAsync(FirstId);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel {Id = FirstId, Text = "poopy"};

            var createdResponse = await _controller.GetAsync(NotFoundId);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = new NodeModel {Id = SecondId, Text = "GEARS"};

            var createdResponse = await _controller.PostAsync(new NodeModel { Id = SecondId, Text = "GEARS" });
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(responseMessage.Headers.Location.ToString(), Is.EqualTo("api/v1/nodes/id"));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Put_UpdatesACorrectNode()
        {
            var expectedResult = new NodeModel {Id = ThirdId, Text = "Planet Music"};

            var createdResponse = await _controller.PutAsync(expectedResult);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult.NodeModelEquals(actualResult));
        }

        [Test]
        public async Task Delete_DeletesCorrectNode()
        {
            var actualResponse = await _controller.DeleteAsync(FourthId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Delete_ActsLikeItDeletedSomeNode()
        {
            var actualResponse = await _controller.DeleteAsync(NotFoundId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}