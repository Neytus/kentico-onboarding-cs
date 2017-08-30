using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoList.Contracts.DAL;

namespace TodoList.Api.Controllers
{
    [Route("api/v1/Nodes/{id:guid?}", Name = "Nodes")]
    public class NodesController : ApiController
    {
        private readonly INodesRepository _repository;

        public NodesController(INodesRepository repository)
        {
            _repository = repository;
        }

        public NodesController()
        {
        }

        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult<IHttpActionResult>(Ok(_repository.GetAllAsync().Result));

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => await Task.FromResult<IHttpActionResult>(Ok(_repository.GetByIdAsync(new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72")).Result));

        public async Task<IHttpActionResult> PostAsync([FromBody] string text)
        {
            var result = _repository.AddAsync("text").Result;
            return await Task.FromResult<IHttpActionResult>(CreatedAtRoute("Nodes", new { id = result.Id.ToString() }, result));
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, string text)
            => await Task.FromResult<IHttpActionResult>(Content(HttpStatusCode.Accepted, _repository.UpdateAsync(new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2"), "text").Result));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => await Task.FromResult<IHttpActionResult>(Ok());

    }
}