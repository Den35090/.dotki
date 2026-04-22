using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

class Program
{
    // Список для хранения запущенных нами экземпляров процессов
    static List<Process> myProcesses = new List<Process>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- МЕНЮ УПРАВЛЕНИЯ ---");
            Console.WriteLine("1 — Запуск процессов (Блокнот)");
            Console.WriteLine("2 — Список всех процессов (фильтр 'notepad')");
            Console.WriteLine("3 — Завершение наших процессов");
            Console.WriteLine("0 — Выход");
            Console.Write("Ваш выбор: ");

            switch (Console.ReadLine())
            {
                case "1": StartProcesses(); break;
                case "2": ShowProcesses("notepad"); break;
                case "3": KillProcesses(); break;
                case "0": return;
            }
        }
    }

    static void StartProcesses()
    {
        try
        {
            // Запускаем 2 экземпляра Блокнота
            for (int i = 0; i < 2; i++)
            {
                Process p = Process.Start("notepad.exe");
                myProcesses.Add(p);
                Console.WriteLine($"Запущен процесс с ID: {p.Id}");
            }
            
            Console.WriteLine("Ожидание 5 секунд...");
            Thread.Sleep(5000);
        }
        catch (Exception ex) { Console.WriteLine($"Ошибка запуска: {ex.Message}"); }
    }

    static void ShowProcesses(string filter)
    {
        Console.WriteLine($"\n--- Процессы по фильтру '{filter}' ---");
        // Получаем все процессы и фильтруем по имени
        var processes = Process.GetProcesses().Where(p => p.ProcessName.Contains(filter, StringComparison.OrdinalIgnoreCase));

        foreach (var p in processes)
        {
            try
            {
                // Использование памяти в байтах, делим на 1024 для КБ
                long memory = p.WorkingSet64 / 1024;
                Console.WriteLine($"Имя: {p.ProcessName,-15} | ID: {p.Id,-6} | Память: {memory} КБ");
            }
            catch { Console.WriteLine($"Процесс {p.Id} недоступен (отказано в доступе)."); }
        }
    }

    static void KillProcesses()
    {
        foreach (var p in myProcesses)
        {
            try
            {
                // Проверяем, не закрыт ли процесс уже
                if (!p.HasExited)
                {
                    p.Kill();
                    Console.WriteLine($"Процесс {p.Id} завершен.");
                }
            }
            catch (InvalidOperationException) { Console.WriteLine($"Процесс {p.Id} уже завершен."); }
            catch (Exception ex) { Console.WriteLine($"Ошибка завершения: {ex.Message}"); }
        }
        myProcesses.Clear();
    }
}