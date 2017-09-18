using System.Configuration;

namespace TodoList.Repository.Helpers
{
    internal static class ConnectionHelper
    {
        internal static string GetDbConnection()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
