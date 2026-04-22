using MySqlConnector; // Новый провайдер для MariaDB
using System.Data;

namespace BankSystemDapper.Data
{
    public class DbConnectionFactory
    {
        // Убедись, что пароль совпадает с тем, что ты задавал (7532)
        private readonly string _connectionString = "Server=127.0.0.1;Port=3306;Database=BankSystem;Uid=root;Pwd=7532;";

        public IDbConnection Create()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}