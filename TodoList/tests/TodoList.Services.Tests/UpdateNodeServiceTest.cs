﻿using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TodoList.Api.Tests.Util;
using TodoList.Contracts.DAL;
using TodoList.Contracts.Models;
using TodoList.Contracts.Services;

namespace TodoList.Services.Tests
{
    [TestFixture]
    internal class UpdateNodeServiceTest
    {
        private static readonly DateTime TestTime = new DateTime(2017, 9, 8, 14, 20, 38);
        private static readonly Guid TestId = new Guid("61EDC3BF-0E94-456E-88C9-9034576C81B1");

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
        public async Task UpdatesNodeCorrectly()
        {
            var updateTime = new DateTime(2017, 9, 10, 16, 28, 48);
            var expectedNode = new NodeModel
            {
                Id = TestId,
                Text = "poopy butt",
                Creation = TestTime,
                LastUpdate = updateTime
            };

            _currentTimeService.GetCurrentTime().Returns(updateTime);
            _repository.UpdateAsync(expectedNode).Returns(Task.CompletedTask);

            var actualNode = await _updateNodeService.UpdateNodeAsync(expectedNode);

            Assert.That(actualNode.NodeModelEquals(expectedNode));
        }

        [Test]
        public async Task FindsNodeInDb()
        {
            var expectedValue = await _updateNodeService.IsInDbAsync(TestId);

            Assert.That(expectedValue, Is.True);
        }
    }
}