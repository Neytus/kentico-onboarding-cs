using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Api.Models;

namespace TodoList.Api.Controllers
{
    public class NodesController : ApiController
    {
        private NodeModel[] Nodes { get; }

        public NodesController()
        {
            Nodes = InitializeData().ToArray();
        }

        [Route("api/v1/nodes")]
        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult<IHttpActionResult>(Ok(Nodes));

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> GetAsync(string id)
            => await Task.FromResult<IHttpActionResult>(Ok(Nodes[0]));

        [Route("api/v1/nodes/{text}")]
        public async Task<IHttpActionResult> PostAsync(string text)
        {
            var response = await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Created, Nodes[1]));
            response.ExecuteAsync(CancellationToken.None).Result.Headers.Location = Request.RequestUri;
            return response;
        }

        [Route("api/v1/nodes/{id}/{text}")]
        public async Task<IHttpActionResult> PutAsync(string id, string text) => 
            await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Accepted, Nodes[2]));

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> DeleteAsync(string id) => await Task.FromResult<IHttpActionResult>(Ok());

        private static IEnumerable<NodeModel> InitializeData()
        {
            yield return new NodeModel {Id = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72"), Text = "poopy"};
            yield return new NodeModel {Id = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2"), Text = "GEARS"};
            yield return new NodeModel {Id = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"), Text = "Planet Music"};
            yield return new NodeModel
            {
                Id = new Guid("b61670fd-33ce-400e-a351-f960230e3aae"),
                Text = "Time to get shwifty"
            };
        }
    }
}