using TodoList.Contracts.Api;

namespace TodoList.Contracts.DAL
{
    public interface INodesFactory
    {
        NodeModel ReturnNewNode(string text);
    }
}
