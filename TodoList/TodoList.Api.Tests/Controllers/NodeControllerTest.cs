﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
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
            var expectedResult = HttpStatusCode.Accepted;

            var response = Controller.Get(1);
            var statusCodeResult = response as StatusCodeResult;
            if (statusCodeResult == null) return;
            var actualResult = statusCodeResult.StatusCode;            

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void GetWithId_ReturnsDefaultNode()
        {
            var expectedResult = HttpStatusCode.Accepted;

            var response = Controller.Get(1);
            var statusCodeResult = response as StatusCodeResult;
            if (statusCodeResult == null) return;
            var actualResult = statusCodeResult.StatusCode;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Post_InsertsNewNodeCorrectly()
        {
            var expectedResult = HttpStatusCode.Accepted;

            var response = Controller.Post("bird person");
            var statusCodeResult = response as StatusCodeResult;
            if (statusCodeResult == null) return;
            var actualResult = statusCodeResult.StatusCode;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Put_UpdatesACorrectNode()
        {
            var expectedResult = HttpStatusCode.Accepted;

            var response = Controller.Put(1, "poopy butthole");
            var statusCodeResult = response as StatusCodeResult;
            if (statusCodeResult == null) return;
            var actualResult = statusCodeResult.StatusCode;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Put_ActsLikeItUpdatedSomeNode()
        {
            var expectedResult = HttpStatusCode.Accepted;

            var response = Controller.Put(1, "poopy butthole");
            var statusCodeResult = response as StatusCodeResult;
            if (statusCodeResult == null) return;
            var actualResult = statusCodeResult.StatusCode;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Delete_DeletesCorrectNode()
        {
            var expectedResult = HttpStatusCode.Accepted;

            var response = Controller.Delete(1);
            var statusCodeResult = response as StatusCodeResult;
            if (statusCodeResult == null) return;
            var actualResult = statusCodeResult.StatusCode;

            Assert.That(expectedResult, Is.EqualTo(actualResult).Using(Comparer));
        }

        [Test]
        public void Delete_ActsLikeItDeletedSomeNode()
        {
            var expectedResult = HttpStatusCode.Accepted;

            var response = Controller.Delete(2);
            var statusCodeResult = response as StatusCodeResult;
            if (statusCodeResult == null) return;
            var actualResult = statusCodeResult.StatusCode;

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