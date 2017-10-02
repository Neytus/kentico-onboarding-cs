using System.Configuration;

namespace TodoList.Repository.Static_Wrappers
{
    internal static class ConnectionHelper
    {
        internal static string GetDbConnection()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}