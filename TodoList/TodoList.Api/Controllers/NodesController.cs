using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Contracts.DAL;

namespace TodoList.Api.Controllers
{
    public class NodesController : ApiController
    {
        private readonly INodesRepository _repository;

        public NodesController(INodesRepository repository)
        {
            _repository = repository;
        }

        [Route("api/v1/nodes")]
        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult<IHttpActionResult>(Ok(_repository.GetAllAsync().Result));

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> GetAsync(string id)
            => await Task.FromResult<IHttpActionResult>(Ok(_repository.GetByIdAsync("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72").Result));

        [Route("api/v1/nodes/{text}")]
        public async Task<IHttpActionResult> PostAsync(string text) => await Task.FromResult<IHttpActionResult>(
            Created("http://localhost:52713/api/v1/nodes/123", _repository.AddAsync("text").Result));

        [Route("api/v1/nodes/{id}/{text}")]
        public async Task<IHttpActionResult> PutAsync(string id, string text) =>
            await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Accepted, _repository.UpdateAsync("6171ec89-e3b5-458e-ae43-bc0e8ec061e2", "text").Result));

        [Route("api/v1/nodes/{id}")]
        public async Task<IHttpActionResult> DeleteAsync(string id) => await Task.FromResult<IHttpActionResult>(Ok());
    }
}