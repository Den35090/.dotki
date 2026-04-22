using MySqlConnector;
using System.Data;

namespace BankSystemDapper.Data
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString = "Server=127.0.0.1;Port=3306;Database=LibraryDb;Uid=root;Pwd=7532;";

        public IDbConnection Create() => new MySqlConnection(_connectionString);
    }
}