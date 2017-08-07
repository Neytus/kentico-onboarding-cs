using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    public class NodeController : ApiController
    {
        public NodeModel[] Nodes { get; set; }

        public NodeController()
        {
            Nodes = InitializeData();
        }

        [Route("api/v1/nodes")]
        public NodeModel[] Get()
        {
            return Nodes;
        }

        [Route("api/v1/nodes/{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(Nodes[0]);
        }

        [Route("api/v1/nodes/{text}")]
        public IHttpActionResult Post(string text)
        {
            return Ok(Nodes[2]);
        }

        [Route("api/v1/nodes/{id}/{text}")]
        public IHttpActionResult Put(int id, string text)
        {
            return Ok(Nodes[1]);
        }

        [Route("api/v1/nodes/{id}")]
        public IHttpActionResult Delete(int id)
        {
            return Ok(Nodes[3]);
        }

        private static NodeModel[] InitializeData()
        {
            var nodes = new NodeModel[]
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