using System;
using System.Net;
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
            => await Task.FromResult<IHttpActionResult>(Ok(Nodes));

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> GetAsync(Guid id)
            => await Task.FromResult<IHttpActionResult>(Ok(Nodes[0]));

        [Route("api/v1/nodes/{text}")]
        public async Task<IHttpActionResult> PostAsync(string text)
            => await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Created, Nodes[1]));

        [Route("api/v1/nodes/{id}/{text}")]
        public async Task<IHttpActionResult> PutAsync(Guid id, string text)
            => await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Accepted, Nodes[2]));

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => await Task.FromResult<IHttpActionResult>(Ok());

        private static NodeModel[] InitializeData()
        {
            var nodes = new NodeModel[]
            {
                new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"},
                new NodeModel {Id = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2"), Text = "GEARS"},
                new NodeModel {Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"), Text = "Planet Music"},
                new NodeModel {Id = new Guid("b61670fd-33ce-400e-a351-f960230e3aae"), Text = "Time to get shwifty"}
            };

            return nodes;
        }
    }
}