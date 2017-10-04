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

        private INodesRepository _repository;
        private ICreateNodeService _createNodeService;
        private IUpdateNodeService _updateNodeService;
        private ILocator _locator;

        private NodesController _controller;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<INodesRepository>();
            _createNodeService = Substitute.For<ICreateNodeService>();
            _updateNodeService = Substitute.For<IUpdateNodeService>();
            _locator = Substitute.For<ILocator>();

            _controller = new NodesController(_repository, _createNodeService, _updateNodeService, _locator)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
        }

        [Test]
        public async Task GetAllNodes_ReturnsAllNodes()
        {
            _repository.GetAllAsync().Returns(new[]
            {
                FirstModel,
                SecondModel,
                ThirdModel,
                FourthModel
            });

            var expectedResult = new[]
            {
                FirstModel, SecondModel, ThirdModel, FourthModel
            };

            var createdResponse = await _controller.GetAsync();
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            actualMessage.TryGetContentValue(out NodeModel[] actualResult);

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualResult, Is.EqualTo(expectedResult).AsCollection.UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Get_WithExistingId_ReturnsCorrectNode()
        {
            _repository.GetByIdAsync(FirstId).Returns(FirstModel);

            var expectedResult = FirstModel;

            var createdResponse = await _controller.GetAsync(FirstId);
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            actualMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualResult, Is.EqualTo(expectedResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Get_WithNonexistentId_ReturnsNotFound()
        {
            var createdResponse = await _controller.GetAsync(NotFoundId);
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Get_WithDefaultId_ReturnsBadRequest()
        {
            var createdResponse = await _controller.GetAsync(Guid.Empty);
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Post_WithValidNodeModel_InsertsNewNodeCorrectly()
        {
            var modelToPost = new NodeModel {Text = "GEARS"};
            _createNodeService.CreateNodeAsync(modelToPost).Returns(SecondModel);

            var expectedResult = SecondModel;
            var expectedUri = new Uri("my/awesome/shwifty/path", UriKind.Relative);
            _locator.GetNodeLocation(new Guid()).ReturnsForAnyArgs(expectedUri);

            var createdResponse = await _controller.PostAsync(modelToPost);
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            actualMessage.TryGetContentValue(out NodeModel actualResult);
            var actualUri = actualMessage.Headers.Location;

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualUri, Is.EqualTo(expectedUri));
            Assert.That(actualResult, Is.EqualTo(expectedResult).UsingNodeModelEqualityComparer());
        }

        [Test, TestCaseSource(nameof(InvalidNodeModelsToPost))]
        public async Task Post_WithInvalidData_ReturnsBadRequest(NodeModel node)
        {
            var createdResponse = await _controller.PostAsync(node);
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            actualMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(actualResult, Is.Null);
        }

        [Test]
        public async Task Put_WithValidNodeInDb_UpdatesACorrectNode()
        {
            _updateNodeService.IsInDbAsync(ThirdId).Returns(true);
            _updateNodeService.UpdateNodeAsync(ThirdModel).Returns(ThirdModel);

            var expectedResult = ThirdModel;

            var createdResponse = await _controller.PutAsync(ThirdModel);
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            actualMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(actualResult, Is.EqualTo(expectedResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Put_WithValidNodeNotInDb_CreatesANewNode()
        {
            var modelToPut = FourthModel;
            _updateNodeService.IsInDbAsync(modelToPut.Id).Returns(false);
            _createNodeService.CreateNodeAsync(modelToPut).Returns(modelToPut);

            var expectedResult = modelToPut;
            var expectedUri = new Uri("my/awesome/highway/to/hell", UriKind.Relative);
            _locator.GetNodeLocation(new Guid()).ReturnsForAnyArgs(expectedUri);

            var createdResponse = await _controller.PutAsync(modelToPut);
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            actualMessage.TryGetContentValue(out NodeModel actualResult);
            var actualUri = actualMessage.Headers.Location;

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(actualUri, Is.EqualTo(expectedUri));
            Assert.That(actualResult, Is.EqualTo(expectedResult).UsingNodeModelEqualityComparer());
        }

        [Test, TestCaseSource(nameof(InvalidNodeModelsToPut))]
        public async Task Put_WithInvalidData_ReturnsBadRequest(NodeModel node)
        {
            var createdResponse = await _controller.PutAsync(node);
            var actualMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            actualMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(actualMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(actualResult, Is.Null);
        }

        [Test]
        public async Task Delete_WithIdFromDb_DeletesCorrectNode()
        {
            _updateNodeService.IsInDbAsync(FirstId).Returns(true);

            var actualResponse = await _controller.DeleteAsync(FirstId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(actualResponse.Content, Is.Null);
        }

        [Test]
        public async Task Delete_WithIdNotFromDb_ReturnsNotFoundStatus()
        {
            _updateNodeService.IsInDbAsync(SecondId).Returns(false);

            var actualResponse = await _controller.DeleteAsync(SecondId).Result
                .ExecuteAsync(CancellationToken.None);

            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(actualResponse.Content, Is.Null);
        }

        [Test]
        public async Task Delete_WithInvalidId_ReturnsBadRequest()
        {
            var invalidId = Guid.Empty;

            var actualResponse = await _controller.DeleteAsync(invalidId).Result.ExecuteAsync(CancellationToken.None);

            Assert.That(actualResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        private static readonly object[] InvalidNodeModelsToPost =
        {
            null,
            new NodeModel {Text = "   "},
            new NodeModel {Text = string.Empty},
            new NodeModel {Id = NotFoundId, Text = "GEARS"},
            new NodeModel {Text = "warm", Creation = new DateTime(2000, 10, 8, 12, 14, 1)},
            new NodeModel {Text = "hot", LastUpdate = new DateTime(2002, 11, 9, 20, 50, 11)},
        };

        private static readonly object[] InvalidNodeModelsToPut =
        {
            null,
            new NodeModel {Id = Guid.Empty},
            new NodeModel {Text = "Nothing like you"},
            new NodeModel {Id = FirstId, Text = "   "},
            new NodeModel {Id = FirstId, Text = string.Empty},
            new NodeModel {Id = FirstId, Text = "cold", Creation = new DateTime(2000, 10, 8, 12, 14, 1)},
            new NodeModel {Id = FirstId, Text = "cool", LastUpdate = new DateTime(2002, 11, 9, 20, 50, 11)},
        };
    }
}