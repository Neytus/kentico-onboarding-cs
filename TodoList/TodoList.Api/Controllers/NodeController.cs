using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TodoList.Api.Models;
using TodoList.BL;

namespace TodoList.Api.Controllers
{
    public class NodeController : ApiController
    {
        public INodeRepository Repository;

        public NodeController(INodeRepository repository)
        {
            Repository = repository;
        }

        [Route("api/v1/nodes")]
        [ResponseType(typeof(NodeModel[]))]
        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult<IHttpActionResult>(Ok(Repository.GetAllAsync()));

        [Route("api/v1/nodes/{id}")]
        [ResponseType(typeof(NodeModel))]
        public async Task<IHttpActionResult> GetAsync(string id)
            => await Task.FromResult<IHttpActionResult>(Ok(Repository.GetByIdAsync("d123bdda-e6d4-4e46-92db-1a7a0aeb9a72")));

        [Route("api/v1/nodes/{text}")]
        [ResponseType(typeof(NodeModel))]
        public async Task<IHttpActionResult> PostAsync(string text)
            => await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Created, Repository.AddAsync("random text")));

        [Route("api/v1/nodes/{id}/{text}")]
        [ResponseType(typeof(NodeModel))]
        public async Task<IHttpActionResult> PutAsync(string id, string text)
            => await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Accepted, Repository.UpdateAsync("6aabec89-e3b5-458e-ae43-bc0e8ec061e2", "any text")));

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> DeleteAsync(string id) => await Task.FromResult<IHttpActionResult>(Ok(Repository.DeleteAsync("b6215481-33ce-400e-a351-f960230e3aae")));
    }
}