using System.Configuration;

namespace TodoList.DAL.Helpers
{
    internal static class ConnectionHelper
    {
        public static string GetDbConnection()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
