using System.Data.SqlClient;

namespace ProductsApp.Services
{
    public class DBHelper
    {
        private static SqlConnection? _conn;
        private DBHelper() { }

        public static SqlConnection? GetConnection()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            string? url = configuration.GetConnectionString("defaultConnection");

            try
            {
                _conn = new SqlConnection(url);
                return _conn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public static void CloseConnection()
        {
            if (_conn is not null) _conn.Close();
        }
    }
}
