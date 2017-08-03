using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    public class NodeController : ApiController
    {
        public List<NodeModel> NodesList { get; set; }

        public NodeController()
        {
            NodesList = InitializeData();
        }

        public List<NodeModel> Get()
        {
            return NodesList;
        }

        public NodeModel Get(int id)
        {
            var nodes = from n in NodesList
                        where n.Id == id
                        select n;

            var nodeModels = nodes as IList<NodeModel> ?? nodes.ToList();
            return !nodeModels.Any()
                ? new NodeModel()
                : nodeModels.First();
        }

        public NodeModel Post(string text)
        {
            var id = NodesList.Count();
            var node = new NodeModel(id + 1, text);
            NodesList.Add(node);

            return node;
        }

        public void Put(int id, string text)
        {
            var node = NodesList.SingleOrDefault(s => s.Id == id);

            if (node != null) node.Text = text;
        }

        public void Delete(int id)
        {
            var node = NodesList.SingleOrDefault(s => s.Id == id);
            NodesList.Remove(node);
        }

        private static List<NodeModel> InitializeData()
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