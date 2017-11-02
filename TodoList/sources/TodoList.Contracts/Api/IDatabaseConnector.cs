namespace TodoList.Contracts.Api
{
    public interface IDatabaseConnector
    {
        string DbConnection { get; }
    }
}
