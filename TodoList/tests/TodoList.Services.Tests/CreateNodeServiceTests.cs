using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TodoList.Api.Tests.Extensions;
using TodoList.Contracts.Models;
using TodoList.Contracts.Services;
using TodoList.Contracts.Repository;
using TodoList.Services.Nodes;

namespace TodoList.Services.Tests
{
    [TestFixture]
    internal class CreateNodeServiceTests
    {
        private static readonly DateTime TestTime = new DateTime(2017, 9, 8, 14, 20, 38);
        private static readonly Guid TestId = new Guid("61EDC3BF-0E94-456E-88C9-9034576C81B1");

        private ICreateNodeService _createNodeService;
        private INodesRepository _repository;
        private IGenerateIdService _generateIdService;
        private ICurrentTimeService _currentTimeService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<INodesRepository>();
            _generateIdService = Substitute.For<IGenerateIdService>();
            _currentTimeService = Substitute.For<ICurrentTimeService>();

            _createNodeService = new CreateNodeService(_repository, _generateIdService, _currentTimeService);
        }

        [Test]
        public async Task CreateNodeAsync_WithValidInputModel_ReturnsCorrectNodeModel()
        {
            var testModel = new NodeModel {Text = "poopy"};
            _repository.AddAsync(testModel).Returns(testModel);
            _currentTimeService.GetCurrentTime().Returns(TestTime);
            _generateIdService.GenerateId().Returns(TestId);

            var expectedNode = new NodeModel
            {
                Id = TestId,
                Text = "poopy",
                Creation = TestTime,
                LastUpdate = TestTime
            };

            var actualNode = await _createNodeService.CreateNodeAsync(testModel);

            Assert.That(actualNode, Is.EqualTo(expectedNode).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task CreateNodeAsync_WithNullData_ThrowsException()
        {
            try
            {
                await _createNodeService.CreateNodeAsync(null);
                Assert.Fail("No exception has been thrown on null data.");
            }
            catch (InvalidOperationException exception)
            {
                const string expectedMessage = "Text to add has to be provided.";

                Assert.That(expectedMessage, Is.EqualTo(exception.Message));
            }
            catch (Exception exception)
            {
                Assert.Fail("A different type of exception has been thrown: " + exception.GetType());
            }
        }
    }
}