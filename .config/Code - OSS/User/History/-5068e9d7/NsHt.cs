using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace FileManagerApp
{
    class Program
    {
        // Путь к файлу логов
        static string logPath = "file_manager_log.txt";

        static void Main()
        {
            Console.Title = "Console File Manager";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== ФАЙЛОВЫЙ МЕНЕДЖЕР =====");
                Console.WriteLine($"Текущая директория: {Directory.GetCurrentDirectory()}");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("1 - Показать файлы и папки");
                Console.WriteLine("2 - Создать файл");
                Console.WriteLine("3 - Удалить файл/папку");
                Console.WriteLine("4 - Копировать файл");
                Console.WriteLine("5 - Переместить файл");
                Console.WriteLine("6 - Информация о файле");
                Console.WriteLine("7 - Поиск файла");
                Console.WriteLine("8 - Сменить директорию");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("=============================");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowContent(); break;
                    case "2": CreateFile(); break;
                    case "3": DeleteItem(); break;
                    case "4": CopyFile(); break;
                    case "5": MoveFile(); break;
                    case "6": 
                        GetFileInfo(); 
                        break;
                    case "7": 
                        SearchFile(); 
                        break;
                    case "8": 
                        ChangeDirectory(); 
                        break;
                    case "0": 
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowContent()
        {
            Console.Clear();
            try
            {
                string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());

                Console.WriteLine("ПАПКИ:");
                foreach (var d in dirs) Console.WriteLine($"  [DIR] {Path.GetFileName(d)}");

                Console.WriteLine("\nФАЙЛЫ:");
                foreach (var f in files) Console.WriteLine($"  [FILE] {Path.GetFileName(f)}");
            }
            catch (Exception ex) { LogError(ex.Message); }
            Pause();
        }

        static void CreateFile()
        {
            Console.Write("Введите имя файла (с расширением): ");
            string name = Console.ReadLine();
            try
            {
                File.Create(name).Close(); // Close сразу освобождает файл
                Console.WriteLine("Файл создан.");
            }
            catch (Exception ex) { LogError(ex.Message); }
            Pause();
        }

        static void DeleteItem()
        {
            Console.Write("Введите имя файла или папки для удаления: ");
            string path = Console.ReadLine();
            try
            {
                if (File.Exists(path)) { File.Delete(path); Console.WriteLine("Файл удален."); }
                else if (Directory.Exists(path)) { Directory.Delete(path, true); Console.WriteLine("Папка удалена."); }
                else Console.WriteLine("Объект не найден.");
            }
            catch (Exception ex) { LogError(ex.Message); }
            Pause();
        }

        static void CopyFile()
        {
            Console.Write("Имя исходного файла: ");
            string source = Console.ReadLine();
            Console.Write("Имя нового файла (путь): ");
            string dest = Console.ReadLine();
            try
            {
                File.Copy(source, dest, true);
                Console.WriteLine("Скопировано.");
            }
            catch (Exception ex) { LogError(ex.Message); }
            Pause();
        }

        static void MoveFile()
        {
            Console.Write("Имя файла: ");
            string source = Console.ReadLine();
            Console.Write("Новый путь/имя: ");
            string dest = Console.ReadLine();
            try
            {
                File.Move(source, dest);
                Console.WriteLine("Перемещено.");
            }
            catch (Exception ex) { LogError(ex.Message); }
            Pause();
        }

        static void GetFileInfo()
        {
            Console.Write("Введите имя файла: ");
            string name = Console.ReadLine();
            try
            {
                FileInfo info = new FileInfo(name);
                if (info.Exists)
                {
                    Console.WriteLine($"Размер: {info.Length} байт");
                    Console.WriteLine($"Создан: {info.CreationTime}");
                    Console.WriteLine($"Расширение: {info.Extension}");
                }
                else Console.WriteLine("Файл не найден.");
            }
            catch (Exception ex) { LogError(ex.Message); }
            Pause();
        }

        static void SearchFile()
        {
            Console.Write("Введите маску поиска (например, *.txt): ");
            string pattern = Console.ReadLine();
            try
            {
                var files = Directory.GetFiles(Directory.GetCurrentDirectory(), pattern, SearchOption.AllDirectories);
                Console.WriteLine($"Найдено совпадений: {files.Length}");
                foreach (var f in files) Console.WriteLine(f);
            }
            catch (Exception ex) { LogError(ex.Message); }
            Pause();
        }

        static void ChangeDirectory()
        {
            Console.Write("Введите путь (или '..' для выхода назад): ");
            string newPath = Console.ReadLine();
            try
            {
                Directory.SetCurrentDirectory(newPath);
            }
            catch (Exception ex) { LogError(ex.Message); }
        }

        static void LogError(string message)
        {
            string logEntry = $"[{DateTime.Now}] ОШИБКА: {message}";
            Console.WriteLine(logEntry);
            File.AppendAllText(logPath, logEntry + Environment.NewLine);
        }

        static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}