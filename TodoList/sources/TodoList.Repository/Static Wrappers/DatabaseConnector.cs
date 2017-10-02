using System.Configuration;

namespace TodoList.Repository.Static_Wrappers
{
    internal class DatabaseConnector
    {
        internal static string GetDbConnection() 
            => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}