using System;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace TaskManagerApp
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Console Task Manager";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== ДИСПЕТЧЕР ЗАДАЧ =====");
                Console.WriteLine("1 - Показать процессы (Топ-20 по памяти)");
                Console.WriteLine("2 - Запустить процесс");
                Console.WriteLine("3 - Завершить процесс");
                Console.WriteLine("4 - Информация о процессе");
                Console.WriteLine("5 - Монитор CPU и RAM (Live)");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("============================");

                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowProcesses(); break;
                    case "2": StartProcess(); break;
                    case "3": KillProcess(); break;
                    case "4": ProcessInfo(); break;
                    case "5": MonitorSystem(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Ошибка: неверный пункт меню.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowProcesses()
        {
            Console.Clear();
            Console.WriteLine($"{"ID",-10} | {"Name",-25} | {"Memory (MB)",-10}");
            Console.WriteLine(new string('-', 50));

            // Берем топ 20 самых "тяжелых" процессов для наглядности
            var processes = Process.GetProcesses()
                                   .OrderByDescending(p => p.WorkingSet64)
                                   .Take(20);

            foreach (var p in processes)
            {
                long mem = p.WorkingSet64 / 1024 / 1024;
                Console.WriteLine($"{p.Id,-10} | {p.ProcessName,-25} | {mem,-10}");
            }
            Pause();
        }

        static void KillProcess()
        {
            Console.Write("Введите ID процесса для завершения: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    using (Process p = Process.GetProcessById(id))
                    {
                        p.Kill();
                        Console.WriteLine("Процесс успешно завершен.");
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
            }
            else Console.WriteLine("Некорректный ID.");
            Pause();
        }

        static void ProcessInfo()
        {
            Console.Write("Введите ID процесса: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    using (Process p = Process.GetProcessById(id))
                    {
                        Console.WriteLine($"\nДетали процесса [{p.ProcessName}]:");
                        Console.WriteLine($"- ID: {p.Id}");
                        Console.WriteLine($"- Память: {p.WorkingSet64 / 1024 / 1024} MB");
                        
                        // Безопасное чтение системных свойств
                        try { Console.WriteLine($"- Запущен: {p.StartTime}"); } 
                        catch { Console.WriteLine("- Запущен: [Нет доступа]"); }

                        Console.WriteLine($"- Потоков: {p.Threads.Count}");
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
            }
            Pause();
        }

        static void MonitorSystem()
        {
            Console.Clear();
            Console.CursorVisible = false;

            // Использование using для очистки системных счетчиков
            using var cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            using var ram = new PerformanceCounter("Memory", "Available MBytes");

            Console.WriteLine("Мониторинг запущен. Нажмите любую клавишу для выхода...");
            int top = Console.CursorTop;

            while (!Console.KeyAvailable)
            {
                // Важно: NextValue() вызывается один раз за итерацию
                float cpuVal = cpu.NextValue();
                float ramVal = ram.NextValue();

                Console.SetCursorPosition(0, top);
                Console.WriteLine($"CPU Load:      {cpuVal,6:F1} %   ");
                Console.WriteLine($"RAM Available: {ramVal,6:F0} MB  ");
                
                Thread.Sleep(1000);
            }
            
            Console.ReadKey(true);
            Console.CursorVisible = true;
        }

        static void StartProcess()
        {
            Console.Write("Имя процесса (notepad, calc): ");
            string name = Console.ReadLine();
            try { Process.Start(name); Console.WriteLine("Команда отправлена."); }
            catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}