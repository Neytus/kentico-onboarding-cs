using System.Configuration;

namespace TodoList.Repository.Static_Wrappers
{
    internal class ConnectionHelper
    {
        internal static string GetDbConnection() 
            => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}