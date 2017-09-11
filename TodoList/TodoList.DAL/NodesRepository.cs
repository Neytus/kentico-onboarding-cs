using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoList.Contracts.Models;
using TodoList.Contracts.DAL;

namespace TodoList.DAL
{
    internal class NodesRepository : INodesRepository
    {
        private readonly IMongoCollection<NodeModel> _dbCollection;

        public NodesRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            const string collectionName = "TodoList";
            var mongoClient = new MongoClient(connectionString);
            var databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);

            _dbCollection = database.GetCollection<NodeModel>(collectionName);            
        }

        public async Task<IEnumerable<NodeModel>> GetAllAsync() 
            => (await _dbCollection.FindAsync(FilterDefinition<NodeModel>.Empty)).ToEnumerable();

        public async Task<NodeModel> GetByIdAsync(Guid id)
            => await _dbCollection.Find(content => content.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(NodeModel model) 
            => await _dbCollection.InsertOneAsync(model);

        public async Task<NodeModel> UpdateAsync(NodeModel model)
            => await Task.FromResult(model);

        public async Task DeleteAsync(Guid id)
            => await Task.CompletedTask;
    }
}