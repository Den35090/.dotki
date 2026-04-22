using System;                     // базовая библиотека
using System.Diagnostics;         // работа с процессами
using System.Threading;           // работа с потоками
using System.Linq;                // для сортировки
using System.Collections.Generic; // список

namespace TaskManagerApp
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Console Task Manager"; // название окна

            while (true) // бесконечный цикл меню
            {
                Console.Clear(); // очистка консоли

                Console.WriteLine("===== ДИСПЕТЧЕР ЗАДАЧ =====");
                Console.WriteLine("1 - Показать процессы");
                Console.WriteLine("2 - Запустить процесс");
                Console.WriteLine("3 - Завершить процесс");
                Console.WriteLine("4 - Информация о процессе");
                Console.WriteLine("5 - Монитор CPU и RAM");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("============================");

                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowProcesses();
                        break;

                    case "2":
                        StartProcess();
                        break;

                    case "3":
                        KillProcess();
                        break;

                    case "4":
                        ProcessInfo();
                        break;

                    case "5":
                        MonitorSystem();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Ошибка выбора");
                        Pause();
                        break;
                }
            }
        }

        static void ShowProcesses()
        {
            Console.Clear();
            Console.WriteLine("СПИСОК ПРОЦЕССОВ\n");

            Process[] processes = Process.GetProcesses(); // получаем процессы

            foreach (var p in processes.OrderBy(p => p.ProcessName))
            {
                try
                {
                    Console.WriteLine($"{p.Id} | {p.ProcessName} | {p.WorkingSet64 / 1024 / 1024} MB");
                }
                catch
                {
                    Console.WriteLine($"{p.Id} | {p.ProcessName}");
                }
            }

            Pause();
        }

        static void StartProcess()
        {
            Console.Clear();

            Console.Write("Введите имя процесса (например notepad): ");
            string name = Console.ReadLine();

            try
            {
                Process.Start(name);
                Console.WriteLine("Процесс запущен");
            }
            catch
            {
                Console.WriteLine("Ошибка запуска");
            }

            Pause();
        }

        static void KillProcess()
        {
            Console.Clear();

            Console.Write("Введите ID процесса: ");
            int id = int.Parse(Console.ReadLine());

            try
            {
                Process p = Process.GetProcessById(id);
                p.Kill();

                Console.WriteLine("Процесс завершен");
            }
            catch
            {
                Console.WriteLine("Ошибка завершения");
            }

            Pause();
        }

        static void ProcessInfo()
        {
            Console.Clear();

            Console.Write("Введите ID процесса: ");
            int id = int.Parse(Console.ReadLine());

            try
            {
                Process p = Process.GetProcessById(id);

                Console.WriteLine($"Имя: {p.ProcessName}");
                Console.WriteLine($"ID: {p.Id}");
                Console.WriteLine($"Память: {p.WorkingSet64 / 1024 / 1024} MB");
                Console.WriteLine($"Запущен: {p.StartTime}");
                Console.WriteLine($"Потоков: {p.Threads.Count}");
            }
            catch
            {
                Console.WriteLine("Ошибка");
            }

            Pause();
        }

        static void MonitorSystem()
        {
            Console.Clear();

            PerformanceCounter cpu =
                new PerformanceCounter("Processor", "% Processor Time", "_Total");

            PerformanceCounter ram =
                new PerformanceCounter("Memory", "Available MBytes");

            Console.WriteLine("Мониторинг... нажмите любую клавишу для выхода\n");

            while (!Console.KeyAvailable)
            {
                float cpuValue = cpu.NextValue();
                Thread.Sleep(1000);

                Console.Clear();
                Console.WriteLine("===== МОНИТОР СИСТЕМЫ =====");
                Console.WriteLine($"CPU: {cpu.NextValue()} %");
                Console.WriteLine($"RAM Available: {ram.NextValue()} MB");
                Console.WriteLine("Нажмите любую клавишу...");
            }

            Console.ReadKey();
        }

        static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}