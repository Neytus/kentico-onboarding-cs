using TodoList.Contracts.Models;
using TodoList.Contracts.DAL;

namespace TodoList.DAL
{
    internal class NodesFactory : INodesFactory
    {
        private readonly INodesIdGenerator _idGenerator; 

        public NodesFactory(INodesIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public NodeModel ReturnNewNode(string text)
        {
            return new NodeModel {Id = _idGenerator.GenerateId() , Text = text} ;
        }
    }
}
