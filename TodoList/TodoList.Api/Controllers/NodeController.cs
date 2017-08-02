using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    public class NodeController : ApiController
    {
        public IEnumerable<Node> NodesList { get; set; }

        public NodeController()
        {
            NodesList = InitializeData();
        }

        public IEnumerable<Node> Get()
        {
            return NodesList;
        }

        public IEnumerable<Node> Get(int id)
        {
            var returnNode = from n in NodesList
                where n.Id == id
                select n;

            return returnNode;
        }

        public void Post(string text)
        {
        }

        public void Put(int id, string text)
        {
        }

        public void Delete(int id)
        {
        }

        private static IEnumerable<Node> InitializeData()
        {
            var nodes = new List<Node>
            {
                new Node(1, "poopy"),
                new Node(2, "GEARS"),
                new Node(3, "Planet Music"),
                new Node(4, "Time to get shwifty")
            };

            return nodes;
        }
    }
}