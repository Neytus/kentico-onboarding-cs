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
        public IEnumerable<NodeModel> NodesList { get; set; }

        public NodeController()
        {
            NodesList = InitializeData();
        }

        public IEnumerable<NodeModel> Get()
        {
            return NodesList;
        }

        public IEnumerable<NodeModel> Get(int id)
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

        private static IEnumerable<NodeModel> InitializeData()
        {
            var nodes = new List<NodeModel>
            {
                new NodeModel(1, "poopy"),
                new NodeModel(2, "GEARS"),
                new NodeModel(3, "Planet Music"),
                new NodeModel(4, "Time to get shwifty")
            };

            return nodes;
        }
    }
}