using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using TodoList.Api.Tests.Extensions;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;
using TodoList.Contracts.Services;
using TodoList.Services.Nodes;
using Assert = NUnit.Framework.Assert;

namespace TodoList.Services.Tests
{
    [TestFixture]
    internal class UpdateNodeServiceTests
    {
        private static readonly DateTime TestTime = new DateTime(2017, 9, 8, 14, 20, 38);
        private static readonly Guid TestId = new Guid("61EDC3BF-0E94-456E-88C9-9034576C81B1");
        private static readonly DateTime UpdatedTime = new DateTime(2017, 9, 10, 16, 28, 48);

        private readonly NodeModel _baseNode = new NodeModel
        {
            Id = TestId,
            Text = "poopy",
            Creation = TestTime,
            LastUpdate = TestTime
        };

        private INodesRepository _repository;
        private ICurrentTimeService _currentTimeService;
        private IUpdateNodeService _updateNodeService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<INodesRepository>();
            _currentTimeService = Substitute.For<ICurrentTimeService>();

            _currentTimeService.GetCurrentTime().Returns(TestTime);

            _repository.GetByIdAsync(TestId).Returns(_baseNode);

            _updateNodeService = new UpdateNodeService(_repository, _currentTimeService);
        }

        [Test]
        public async Task UpdateNodeInDb_WithCorrectData_UpdatesNodeInDbCorrectly()
        {
            var expectedNode = new NodeModel
            {
                Id = TestId,
                Text = "poopy butt",
                Creation = TestTime,
                LastUpdate = UpdatedTime
            };

            _currentTimeService.GetCurrentTime().Returns(UpdatedTime);
            _repository.UpdateAsync(expectedNode).Returns(expectedNode);

            var actualNode = await _updateNodeService.UpdateNodeAsync(expectedNode);

            Assert.That(actualNode, Is.EqualTo(expectedNode).UsingNodeModelEqualityComparer());
        }

        [Test]
        public async Task UpdateNodeInDb_WithNullData_ThrowsException()
        {
            try
            {
                await _updateNodeService.UpdateNodeAsync(null);
                Assert.Fail("No exception has been thrown on null data.");
            }
            catch (InvalidOperationException exception)
            {
                var expectedMessage = "Values to update have to be provided.";

                Assert.That(expectedMessage, Is.EqualTo(exception.Message));
            }
            catch (Exception exception)
            {
                Assert.Fail("A different type of exception has been thrown: " + exception.GetType());
            }
        }

        [Test]
        public async Task IsInDbAsync_ReturnsTrueForNodeInDb()
        {
            var isNodeInDb = await _updateNodeService.IsInDbAsync(TestId);

            Assert.That(isNodeInDb, Is.EqualTo(true));
        }

        [Test]
        public async Task IsInDbAsync_ReturnsFalseForNodeNotInDb()
        {
            var anotherGuid = new Guid("317200b4-2845-43ce-93d4-aa35511e4c68");

            var isNodeInDb = await _updateNodeService.IsInDbAsync(anotherGuid);

            Assert.That(isNodeInDb, Is.EqualTo(false));
        }
    }
}