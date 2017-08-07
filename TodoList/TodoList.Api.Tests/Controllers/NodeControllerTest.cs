using System.Threading;
using System.Web.Http.Results;
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
                new NodeModel{Id = 1, Text = "poopy"},
                new NodeModel{Id = 2, Text = "GEARS"},
                new NodeModel{Id = 3, Text = "Planet Music"},
                new NodeModel{Id = 4, Text = "Time to get shwifty"}
            };
            var actualResult = Controller.GetAsync();

            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }

        [Test]
        public void GetWithId_ReturnsCorrectNode()
        {
            var expectedResult = new NodeModel{Id = 1,Text = "poopy"};

            var actualResult = Controller.GetAsync(0).Result.ExecuteAsync(new CancellationToken()).Result.Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = new NodeModel{ Id = 1, Text = "poopy" };

            var response = Controller.GetAsync(0).Result;
            var actualResult = ((OkNegotiatedContentResult<NodeModel>) response).Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = new NodeModel { Id = 2, Text = "GEARS" };

            var response = Controller.PostAsync("TEST TEXT").Result;
            var actualResult = ((OkNegotiatedContentResult<NodeModel>)response).Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Put_UpdatesACorrectNode()
        {
            var expectedResult = new NodeModel { Id = 3, Text = "Planet Music" };
                                               
            var response = Controller.GetAsync(0).Result;
            var actualResult = ((OkNegotiatedContentResult<NodeModel>)response).Content;
                                               
            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }                                      
                                               
        [Test]                                 
        public void Put_ActsLikeItUpdatedSomeNode()
        {                                      
            var expectedResult = new NodeModel { Id = 3, Text = "Planet Music" };

            var response = Controller.GetAsync(0).Result;
            var actualResult = ((OkNegotiatedContentResult<NodeModel>)response).Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Delete_DeletesCorrectNode()
        {
            var expectedResult = new NodeModel { Id = 4, Text = "Time to get shwifty" };

            var response = Controller.GetAsync(0).Result;
            var actualResult = ((OkNegotiatedContentResult<NodeModel>)response).Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }

        [Test]
        public void Delete_ActsLikeItDeletedSomeNode()
        {
            var expectedResult = new NodeModel { Id = 4, Text = "Time to get shwifty" };

            var response = Controller.GetAsync(0).Result;
            var actualResult = ((OkNegotiatedContentResult<NodeModel>)response).Content;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(NodeModelEqualityComparer.Instance));
        }
    }
}
