using System.Configuration;
using TodoList.Contracts.Api;

namespace TodoList.Api.Static_Wrappers
{
    internal class DatabaseConnector : IDatabaseConnector
    {
        private readonly ConnectionStringSettings _connectionStringSettings;

        public DatabaseConnector()
        {
            _connectionStringSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
        }

        public string DbConnection => _connectionStringSettings.ConnectionString;
    }
}