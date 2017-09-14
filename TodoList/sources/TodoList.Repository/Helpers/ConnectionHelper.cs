using System.Configuration;

namespace TodoList.Repository.Helpers
{
    internal static class ConnectionHelper
    {
        public static string GetDbConnection()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
