using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleScheduler
{
    // Класс модели задачи
    class TaskItem
    {
        public string ProgramPath { get; set; }
        public DateTime ExecuteTime { get; set; }
        public bool IsExecuted { get; set; } = false;

        public override string ToString() => 
            $"[{ExecuteTime:HH:mm:ss}] - {ProgramPath} (Статус: {(IsExecuted ? "Выполнено" : "Ожидает")})";
    }

    class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static string filePath = "tasks.txt";
        static string logPath = "scheduler_log.txt";
        static object locker = new object(); // Для потокобезопасности

        static void Main()
        {
            LoadTasks();
            
            // Запуск фонового потока мониторинга времени
            Thread monitorThread = new Thread(MonitorTasks);
            monitorThread.IsBackground = true;
            monitorThread.Start();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== ПЛАНИРОВЩИК ЗАДАЧ =====");
                Console.WriteLine("1 - Добавить задачу");
                Console.WriteLine("2 - Показать задачи");
                Console.WriteLine("3 - Удалить задачу");
                Console.WriteLine("4 - Очистить выполненные");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("=============================");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();

                // Выполнение каждой функции в отдельной Task (многопоточность)
                switch (choice)
                {
                    case "1": Task.Run(() => AddTask()).Wait(); break;
                    case "2": Task.Run(() => ShowTasks()).Wait(); break;
                    case "3": Task.Run(() => DeleteTask()).Wait(); break;
                    case "4": Task.Run(() => ClearExecuted()).Wait(); break;
                    case "0": return;
                }
            }
        }

        static void MonitorTasks()
        {
            while (true)
            {
                lock (locker)
                {
                    var now = DateTime.Now;
                    foreach (var task in tasks.Where(t => !t.IsExecuted && t.ExecuteTime <= now))
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo(task.ProgramPath) { UseShellExecute = true });
                            task.IsExecuted = true;
                            LogAction($"ЗАПУСК: {task.ProgramPath}");
                            SaveTasks();
                        }
                        catch (Exception ex)
                        {
                            LogAction($"ОШИБКА ЗАПУСКА {task.ProgramPath}: {ex.Message}");
                            task.IsExecuted = true; // Помечаем как "обработанное", чтобы не спамить ошибкой
                        }
                    }
                }
                Thread.Sleep(1000); // Проверка каждую секунду
            }
        }

        static void AddTask()
        {
            try
            {
                Console.Write("Введите путь к программе или имя (notepad): ");
                string path = Console.ReadLine();
                Console.Write("Введите время (ЧЧ:ММ:СС): ");
                TimeSpan time = TimeSpan.Parse(Console.ReadLine());
                
                DateTime executeDate = DateTime.Today.Add(time);
                if (executeDate < DateTime.Now) executeDate = executeDate.AddDays(1);

                lock (locker)
                {
                    tasks.Add(new TaskItem { ProgramPath = path, ExecuteTime = executeDate });
                    SaveTasks();
                }
                LogAction($"ДОБАВЛЕНО: {path} на {executeDate}");
                Console.WriteLine("Задача добавлена.");
            }
            catch (Exception ex) { Console.WriteLine("Ошибка: " + ex.Message); }
            Pause();
        }

        static void ShowTasks()
        {
            lock (locker)
            {
                if (tasks.Count == 0) Console.WriteLine("Список пуст.");
                for (int i = 0; i < tasks.Count; i++)
                    Console.WriteLine($"{i} | {tasks[i]}");
            }
            Pause();
        }

        static void DeleteTask()
        {
            ShowTasks();
            Console.Write("Введите ID для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                lock (locker)
                {
                    if (id >= 0 && id < tasks.Count)
                    {
                        LogAction($"УДАЛЕНО: {tasks[id].ProgramPath}");
                        tasks.RemoveAt(id);
                        SaveTasks();
                        Console.WriteLine("Удалено.");
                    }
                }
            }
            Pause();
        }

        static void ClearExecuted()
        {
            lock (locker)
            {
                tasks.RemoveAll(t => t.IsExecuted);
                SaveTasks();
            }
            Console.WriteLine("Выполненные задачи удалены из списка.");
            Pause();
        }

        static void SaveTasks()
        {
            var lines = tasks.Select(t => $"{t.ProgramPath};{t.ExecuteTime};{t.IsExecuted}");
            File.WriteAllLines(filePath, lines);
        }

        static void LoadTasks()
        {
            if (!File.Exists(filePath)) return;
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 3)
                {
                    tasks.Add(new TaskItem { 
                        ProgramPath = parts[0], 
                        ExecuteTime = DateTime.Parse(parts[1]), 
                        IsExecuted = bool.Parse(parts[2]) 
                    });
                }
            }
        }

        static void LogAction(string message)
        {
            string entry = $"[{DateTime.Now}] {message}";
            File.AppendAllText(logPath, entry + Environment.NewLine);
        }

        static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}