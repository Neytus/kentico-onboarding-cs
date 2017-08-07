using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetAsync()
        {
            var nodes = await Task.FromResult<IHttpActionResult>(Ok(Nodes));

            return nodes;
        }

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var node = await Task.FromResult<IHttpActionResult>(Ok(Nodes[0]));

            return node;
        }

        [Route("api/v1/nodes/{text}")]
        public async Task<IHttpActionResult> PostAsync(string text)
            => await Task.FromResult<IHttpActionResult>(Ok(Nodes[1]));

        [Route("api/v1/nodes/{id}/{text}")]
        public async Task<IHttpActionResult> PutAsync(int id, string text)
        {
            var node = await Task.FromResult<IHttpActionResult>(Ok(Nodes[2]));

            return node;
        }

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var node = await Task.FromResult<IHttpActionResult>(Ok(Nodes[3]));

            return node;
        }

        private static NodeModel[] InitializeData()
        {
            var nodes = new NodeModel[]
            {
                new NodeModel {Id = 1, Text = "poopy"},
                new NodeModel {Id = 2, Text = "GEARS"},
                new NodeModel {Id = 3, Text = "Planet Music"},
                new NodeModel {Id = 4, Text = "Time to get shwifty"}
            };

            return nodes;
        }
    }
}