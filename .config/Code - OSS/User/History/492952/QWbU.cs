using Dapper;
using BankSystemDapper.Models;
using BankSystemDapper.Data;
using System.Collections.Generic;
using System.Linq;

namespace BankSystemDapper.Services
{
    public class BookService
    {
        private DbConnectionFactory _factory = new DbConnectionFactory();

        public List<Book> GetAll()
        {
            using var db = _factory.Create();
            return db.Query<Book>("SELECT * FROM Books").ToList();
        }

        public void Add(Book book)
        {
            using var db = _factory.Create();
            string sql = "INSERT INTO Books (Title, Author, Year, Price, Genre) VALUES (@Title, @Author, @Year, @Price, @Genre)";
            db.Execute(sql, book);
        }

        public void Delete(int id)
        {
            using var db = _factory.Create();
            db.Execute("DELETE FROM Books WHERE Id = @id", new { id });
        }

        public List<Book> SearchByAuthor(string author)
        {
            using var db = _factory.Create();
            return db.Query<Book>("SELECT * FROM Books WHERE Author LIKE @author", new { author = $"%{author}%" }).ToList();
        }
    }
}