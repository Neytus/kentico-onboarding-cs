using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NSubstitute;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Tests.Extensions;
using TodoList.Contracts.Api;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;
using TodoList.Contracts.Services;

namespace TodoList.Api.Tests.Controllers
{
    [TestFixture]
    internal class NodesControllerTests
    {
        private static readonly Guid FirstId = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72");
        private static readonly Guid SecondId = new Guid("b61670fd-33ce-400e-a351-f960230e3aae");
        private static readonly Guid ThirdId = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2");
        private static readonly Guid NotFoundId = new Guid("aa0011ff-e6d4-4e46-92db-1a7a0aeb9a72");

        private static readonly NodeModel FirstModel = new NodeModel
        {
            Id = FirstId,
            Text = "poopy"
        };

        private static readonly NodeModel SecondModel = new NodeModel
        {
            Id = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2"),
            Text = "GEARS"
        };

        private static readonly NodeModel ThirdModel = new NodeModel
        {
            Id = ThirdId,
            Text = "Planet Music"
        };

        private static readonly NodeModel FourthModel = new NodeModel
        {
            Id = SecondId,
            Text = "Time to get shwifty"
        };

        private readonly INodesRepository _repository = NodesRepository();
        private readonly ICreateNodeService _createNodeService = CreateNodeService();
        private readonly IUpdateNodeService _updateNodeService = UpdateNodeService();
        private readonly ILocator _locator = LocationHelper();

        private NodesController _controller;

        [SetUp]
        public void SetUp()
        {
            _controller = new NodesController(_repository, _createNodeService, _updateNodeService, _locator)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
        }

        [Test]
        public async Task Get_WithoutSpecifiedId_ReturnsAllNodes()
        {
            var expectedResult = new[]
            {
                FirstModel, SecondModel, ThirdModel, FourthModel
            };

            var createdResponse = await _controller.GetAsync();
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel[] actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult, Is.EqualTo(actualResult).AsCollection.UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Get_WithCorrectId_ReturnsCorrectNode()
        {
            var expectedResult = FirstModel;

            var createdResponse = await _controller.GetAsync(FirstId);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Get_WithNotFoundId_ReturnsDefaultNode()
        {
            var expectedResult = FirstModel;

            var createdResponse = await _controller.GetAsync(NotFoundId);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(expectedResult, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Get_WithDefaultId_ReturnsBadRequest()
        {
            var createdResponse = await _controller.GetAsync(Guid.Empty);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Post_WithCorrectTextData_InsertsNewNodeCorrectly()
        {
            var expectedResult = SecondModel;

            var createdResponse = await _controller.PostAsync(new NodeModel {Text = "GEARS"});
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(responseMessage.Headers.Location.ToString(), Is.EqualTo("my/awesome/shwifty/path"));
            Assert.That(expectedResult, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Post_WithIncorrectTextData_ReturnsBadRequest()
        {
            var createdResponse = await _controller.PostAsync(new NodeModel {Text = "   "});
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(null, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Post_WithSpecifiedId_ReturnsBadRequest()
        {
            var createdResponse = await _controller.PostAsync(new NodeModel {Id = NotFoundId, Text = "GEARS"});
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(null, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Post_WithNullModel_ReturnsBadRequest()
        {
            var createdResponse = await _controller.PostAsync(null);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(null, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Put_WithCorrectNodeModel_UpdatesACorrectNode()
        {
            var expectedResult = ThirdModel;

            var createdResponse = await _controller.PutAsync(expectedResult);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Put_WithIncorrectTextData_ReturnsBadRequest()
        {
            var createdResponse = await _controller.PutAsync(new NodeModel {Id = ThirdId, Text = "    "});
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(null, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Put_WithoutSpecifiedId_ReturnsBadRequest()
        {
            var methodInput = new NodeModel {Text = "Nothing like you"};

            var createdResponse = await _controller.PutAsync(methodInput);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Put_WithNullModel_ReturnsBadRequest()
        {
            var createdResponse = await _controller.PutAsync(null);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(null, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Delete_WithIdFromDb_DeletesCorrectNode()
        {
            var actualResponse = await _controller.DeleteAsync(FirstId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Delete_WithIdNotFromDb_ReturnsNotFoundStatus()
        {
            var actualResponse = await _controller.DeleteAsync(SecondId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.IsNull(actualResponse.Content);
            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        private static ILocator LocationHelper()
        {
            var locationHelper = Substitute.For<ILocator>();
            locationHelper.GetNodeLocation(new Guid())
                .ReturnsForAnyArgs(new Uri("my/awesome/shwifty/path", UriKind.Relative));
            return locationHelper;
        }

        private static IUpdateNodeService UpdateNodeService()
        {
            var updateNodeService = Substitute.For<IUpdateNodeService>();
            updateNodeService.UpdateNodeAsync(new NodeModel()).ReturnsForAnyArgs(ThirdModel);
            updateNodeService.IsInDbAsync(ThirdId).Returns(true);
            return updateNodeService;
        }

        private static ICreateNodeService CreateNodeService()
        {
            var createNodeService = Substitute.For<ICreateNodeService>();
            createNodeService.CreateNodeAsync(new NodeModel()).ReturnsForAnyArgs(SecondModel);
            return createNodeService;
        }

        private static INodesRepository NodesRepository()
        {
            var repository = Substitute.For<INodesRepository>();

            repository.GetAllAsync().Returns(new[]
            {
                FirstModel,
                SecondModel,
                ThirdModel,
                FourthModel
            });

            repository.GetByIdAsync(FirstId)
                .Returns(FirstModel);
            repository.GetByIdAsync(NotFoundId)
                .Returns(FirstModel);
            repository.GetByIdAsync(ThirdId)
                .Returns(ThirdModel);

            repository.DeleteAsync(new Guid()).ReturnsForAnyArgs(Task.CompletedTask);
            return repository;
        }
    }
}