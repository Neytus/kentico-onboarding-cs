using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoList.Contracts.Api;
using TodoList.Contracts.DAL;

namespace TodoList.DAL
{
    internal class NodesRepository : INodesRepository
    {
        private readonly IMongoCollection<NodeModel> _dbCollection;

        private static readonly Guid FirstId = new Guid("d237bdda-e6d4-4e46-92db-1a7a0aeb9a72");
        private static readonly Guid SecondId = new Guid("b84bbcc7-d516-4805-b2e3-20a2df3758a2");
        private static readonly Guid ThirdId = new Guid("6171ec89-e3b5-458e-ae43-bc0e8ec061e2");
        private static readonly Guid FourthId = new Guid("b61670fd-33ce-400e-a351-f960230e3aae");

        public NodesRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            const string collectionName = "TodoList";
            var mongoClient = new MongoClient(connectionString);
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);

            _dbCollection = database.GetCollection<NodeModel>(collectionName);            
        }

        public async Task<IEnumerable<NodeModel>> GetAllAsync() => (await _dbCollection.FindAsync(FilterDefinition<NodeModel>.Empty)).ToEnumerable();

        public async Task<NodeModel> GetByIdAsync(Guid id)
            => await _dbCollection.Find(content => content.Id == FirstId).FirstOrDefaultAsync();

        public async Task<NodeModel> AddAsync(NodeModel model)
            => await Task.FromResult(new NodeModel
            {
                Id = SecondId,
                Text = "GEARS"
            });

        public async Task<NodeModel> UpdateAsync(NodeModel model)
            => await Task.FromResult(new NodeModel
            {
                Id = ThirdId,
                Text = "Planet Music"
            });

        public async Task DeleteAsync(Guid id)
            => await Task.CompletedTask;
    }
}