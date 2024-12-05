using Npgsql;


namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// Lớp giúp mở kết nối đến database PostgreSQL.
    /// </summary>
    public class DatabaseHelper
    {
        private string _connectionString = "Host=localhost;Username=yourusername;Password=yourpassword;Database=yourdatabase";

        // Mở kết nối đến PostgreSQL
        public NpgsqlConnection OpenConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        // Đóng kết nối
        public void CloseConnection(NpgsqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }

}