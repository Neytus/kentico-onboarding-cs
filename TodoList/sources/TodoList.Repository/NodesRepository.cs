using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoList.Contracts.Api;
using TodoList.Contracts.Models;
using TodoList.Contracts.Repository;

namespace TodoList.Repository
{
    internal class NodesRepository : INodesRepository
    {
        private readonly IMongoCollection<NodeModel> _dbCollection;

        public NodesRepository(IDatabaseConnector connector)
        {
            const string collectionName = "TodoList";
            var mongoClient = new MongoClient(connector.DbConnection);
            var databaseName = MongoUrl.Create(connector.DbConnection).DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);

            _dbCollection = database.GetCollection<NodeModel>(collectionName);
        }

        public async Task<IEnumerable<NodeModel>> GetAllAsync()
            => (await _dbCollection.FindAsync(FilterDefinition<NodeModel>.Empty)).ToEnumerable();

        public async Task<NodeModel> GetByIdAsync(Guid id)
            => await _dbCollection.Find(node => node.Id == id).FirstOrDefaultAsync();

        public async Task<NodeModel> AddAsync(NodeModel model)
        {
            await _dbCollection.InsertOneAsync(model);
            return model;
        }

        public async Task<NodeModel> UpdateAsync(NodeModel model) 
            => await _dbCollection.FindOneAndReplaceAsync(node => node.Id == model.Id, model);

        public async Task DeleteAsync(Guid id)
            => await _dbCollection.DeleteOneAsync(node => node.Id == id);
    }
}