using TodoList.Contracts.Models;

namespace TodoList.Contracts.DAL
{
    public interface INodesFactory
    {
        NodeModel ReturnNewNode(string text);
    }
}
