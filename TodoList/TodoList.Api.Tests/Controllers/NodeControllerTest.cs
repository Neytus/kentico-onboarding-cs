using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TodoList.Api.Controllers;
using TodoList.Api.Models;

namespace TodoList.Api.Tests.Controllers
{
    
    public class NodeControllerTest
    {
        public NodeController Controller;
        public readonly NodeModelEqualityComparer Comparer = new NodeModelEqualityComparer();

        [SetUp]
        public void SetUp()
        {
            Controller = new NodeController(new List<NodeModel>
            {
                new NodeModel(1, "poopy"),
                new NodeModel(2, "GEARS"),
                new NodeModel(3, "Planet Music"),
                new NodeModel(4, "Time to get shwifty")
            });
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Get_ReturnsAllNodes()
        {
            var expectedResult = new List<NodeModel>
            {
                new NodeModel(1, "poopy"),
                new NodeModel(2, "GEARS"),
                new NodeModel(3, "Planet Music"),
                new NodeModel(4, "Time to get shwifty")
            };
            var actualResult = Controller.Get();

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void GetWithId_ReturnsCorrectNode()
        {
            var expectedResult = new NodeModel(1, "poopy");
            var actualResult = Controller.Get(1);

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel(0, "default text");
            var actualResult = Controller.Get(10);

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = new List<NodeModel>
            {
                new NodeModel(1, "poopy"),
                new NodeModel(2, "GEARS"),
                new NodeModel(3, "Planet Music"),
                new NodeModel(4, "Time to get shwifty"),
                new NodeModel(5, "birdman")
            };

            Controller.Post("birdman");
            var actualResult = Controller.NodesList;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Put_UpdatesACorrectNode()
        {
            var expectedResult = new List<NodeModel>
            {
                new NodeModel(1, "poopy butthole"),
                new NodeModel(2, "GEARS"),
                new NodeModel(3, "Planet Music"),
                new NodeModel(4, "Time to get shwifty")
            };

            Controller.Put(1, "poopy butthole");
            var actualResult = Controller.NodesList;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Put_ActsLikeItUpdatedSomeNode()
        {
            var expectedResult = new List<NodeModel>
            {
                new NodeModel(1, "poopy"),
                new NodeModel(2, "GEARS"),
                new NodeModel(3, "Planet Music"),
                new NodeModel(4, "Time to get shwifty")
            };

            Controller.Put(6, "poopy butthole");
            var actualResult = Controller.NodesList;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Delete_DeletesCorrectNode()
        {
            var expectedResult = new List<NodeModel>
            {
                new NodeModel(1, "poopy"),
                new NodeModel(2, "GEARS"),
                new NodeModel(4, "Time to get shwifty")
            };

            Controller.Delete(3);
            var actualResult = Controller.NodesList;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Delete_ActsLikeItDeletedSomeNode()
        {
            var expectedResult = new List<NodeModel>
            {
                new NodeModel(1, "poopy"),
                new NodeModel(2, "GEARS"),
                new NodeModel(3, "Planet Music"),
                new NodeModel(4, "Time to get shwifty")
            };

            Controller.Delete(5);
            var actualResult = Controller.NodesList;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }
    }

    public class NodeModelEqualityComparer : IEqualityComparer<NodeModel>
    { 
        public bool Equals(NodeModel x, NodeModel y)
        {
            if ((x == null) || (y == null) || x.GetType() != y.GetType()) return false;

            return (x.Id == y.Id) && (x.Text == y.Text);
        }

        public int GetHashCode(NodeModel obj)
        {
            return obj.GetHashCode();
        }
    }
}
