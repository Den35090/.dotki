using System;
using BankSystemDapper.Services;
using BankSystemDapper.Models;
using BankSystemDapper.SystemProgramming;

namespace BankSystemDapper.Menu
{
    public class MainMenu
    {
        private BookService _service = new BookService();
        private ThreadService _threadService = new ThreadService();
        private ProcessService _processService = new ProcessService();

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ БИБЛИОТЕКОЙ ===");
                Console.WriteLine("1-Все книги | 2-Добавить | 3-Удалить | 4-Изменить");
                Console.WriteLine("5-Сорт. Год | 6-Сорт. Цена | 7-Поиск Автора");
                Console.WriteLine("8-Поток (Load) | 9-Процесс (Report) | 0-Выход");
                Console.Write("\nВыбор: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1": PrintBooks(_service.GetAll()); break;
                    case "2": AddBook(); break;
                    case "3": DeleteBook(); break;
                    case "4": UpdateBook(); break;
                    case "5": PrintBooks(_service.SortByYear()); break;
                    case "6": PrintBooks(_service.SortByPrice()); break;
                    case "7": SearchAuthor(); break;
                    case "8": _threadService.LoadDataInThread(); break;
                    case "9": _processService.RunReportProcess(); break;
                }
                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        private void PrintBooks(System.Collections.Generic.List<Book> books)
        {
            books.ForEach(Console.WriteLine);
        }

        private void AddBook()
        {
            var b = new Book();
            Console.Write("Название: "); b.Title = Console.ReadLine() ?? "";
            Console.Write("Автор: "); b.Author = Console.ReadLine() ?? "";
            
            string? yearInput = Console.ReadLine();
            b.Year = int.TryParse(yearInput, out int y) ? y : 0;

            string? priceInput = Console.ReadLine();
            b.Price = decimal.TryParse(priceInput, out decimal p) ? p : 0;

            Console.Write("Жанр: "); b.Genre = Console.ReadLine() ?? "";
            _service.Add(b);
        }

        private void DeleteBook()
        {
            Console.Write("ID для удаления: ");
            int id = int.Parse(Console.ReadLine());
            _service.Delete(id);
        }

        private void SearchAuthor()
        {
            Console.Write("Введите автора: ");
            string author = Console.ReadLine();
            PrintBooks(_service.SearchByAuthor(author));
        }

        private void UpdateBook()
        {
            Console.Write("Введите ID книги для изменения: ");
            int id = int.Parse(Console.ReadLine());
            var b = new Book { Id = id };
            Console.Write("Новое название: "); b.Title = Console.ReadLine();
            Console.Write("Новый автор: "); b.Author = Console.ReadLine();
            Console.Write("Новый год: "); b.Year = int.Parse(Console.ReadLine());
            Console.Write("Новая цена: "); b.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Новый жанр: "); b.Genre = Console.ReadLine();
            _service.Update(b);
        }
    }
}