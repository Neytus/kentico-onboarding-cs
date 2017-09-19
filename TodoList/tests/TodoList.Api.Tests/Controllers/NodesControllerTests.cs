﻿using System;
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

namespace TodoList.Api.Tests.Controllers
{
    [TestFixture]
    internal class NodesControllerTests
    {
        private static readonly Guid FirstId = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72");
        private static readonly Guid FourthId = new Guid("b61670fd-33ce-400e-a351-f960230e3aae");
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
            Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"),
            Text = "Planet Music"
        };

        private static readonly NodeModel FourthModel = new NodeModel
        {
            Id = FourthId,
            Text = "Time to get shwifty"
        };

        private NodesController _controller;

        [SetUp]
        public void SetUp()
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

            repository.AddAsync(new NodeModel()).ReturnsForAnyArgs(SecondModel);

            repository.UpdateAsync(new NodeModel()).ReturnsForAnyArgs(ThirdModel);

            var locationHelper = Substitute.For<ILocationHelper>();
            locationHelper.GetNodeLocation(new Guid()).ReturnsForAnyArgs(new Uri("my/awesome/shwifty/path", UriKind.Relative));

            _controller = GetControllerForTests(repository, locationHelper);
        }

        [Test]
        public async Task Get_WithoutSpecifiedId_ReturnsAllNodes()
        {
            var expectedResult = new[]
            {
                FirstModel,
                SecondModel,
                ThirdModel,
                FourthModel
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
        public async Task Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = SecondModel;

            var createdResponse = await _controller.PostAsync(new NodeModel {Id = Guid.Empty, Text = "poopy"});
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(responseMessage.Headers.Location.ToString(), Is.EqualTo("my/awesome/shwifty/path"));
            Assert.That(expectedResult, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task Put_UpdatesACorrectNode()
        {
            var expectedResult = ThirdModel;

            var createdResponse = await _controller.PutAsync(expectedResult);
            var responseMessage = await createdResponse.ExecuteAsync(CancellationToken.None);
            responseMessage.TryGetContentValue(out NodeModel actualResult);

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(expectedResult, Is.EqualTo(actualResult).UsingNodeModelEqualityComparer());
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

        private static NodesController GetControllerForTests(INodesRepository repository, ILocationHelper locationHelper)
            => new NodesController(repository, locationHelper)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
    }
}