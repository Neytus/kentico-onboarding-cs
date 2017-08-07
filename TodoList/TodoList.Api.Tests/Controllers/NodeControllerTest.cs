using System;
using System.Threading;
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
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Get_ReturnsAllNodes()
        {
            var expectedResult = new NodeModel[]
            {
                new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"},
                new NodeModel {Id = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2"), Text = "GEARS"},
                new NodeModel {Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"), Text = "Planet Music"},
                new NodeModel {Id = new Guid("b61670fd-33ce-400e-a351-f960230e3aae"), Text = "Time to get shwifty"}
            };
            var actualResult = Controller.GetAsync();

            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }

        [Test]
        public void GetWithId_ReturnsCorrectNode()
        {
            var expectedResult = new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"};

            var actualResult = Controller.GetAsync("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72").Result
                .ExecuteAsync(new CancellationToken()).Result.Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"};

            var actualResult = Controller.GetAsync("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72").Result
                .ExecuteAsync(new CancellationToken()).Result.Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = new NodeModel {Id = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2"), Text = "GEARS"};

            var actualResult = Controller.PostAsync("TEST TEXT").Result.ExecuteAsync(new CancellationToken()).Result
                .Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Put_UpdatesACorrectNode()
        {
            var expectedResult = new NodeModel
            {
                Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"),
                Text = "Planet Music"
            };

            var actualResult = Controller.PutAsync("6171ec89-e3b5-458e-ae43-bc0e8ec061e2", "Planet Music").Result
                .ExecuteAsync(new CancellationToken()).Result.Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Put_ActsLikeItUpdatedSomeNode()
        {
            var expectedResult = new NodeModel
            {
                Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"),
                Text = "Planet Music"
            };

            var actualResult = Controller.PutAsync("6171ec89-e3b5-458e-ae43-bc0e8ec061e2", "Planet Music").Result
                .ExecuteAsync(new CancellationToken()).Result.Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Delete_DeletesCorrectNode()
        {
            var expectedResult = new NodeModel
            {
                Id = new Guid("b61670fd-33ce-400e-a351-f960230e3aae"),
                Text = "Time to get shwifty"
            };

            var actualResult = Controller.DeleteAsync("b61670fd-33ce-400e-a351-f960230e3aae").Result
                .ExecuteAsync(new CancellationToken()).Result.Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Delete_ActsLikeItDeletedSomeNode()
        {
            var expectedResult = new NodeModel
            {
                Id = new Guid("b61670fd-33ce-400e-a351-f960230e3aae"),
                Text = "Time to get shwifty"
            };

            var actualResult = Controller.DeleteAsync("b61670fd-33ce-400e-a351-f960230e3aae").Result
                .ExecuteAsync(new CancellationToken()).Result.Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }
    }
}