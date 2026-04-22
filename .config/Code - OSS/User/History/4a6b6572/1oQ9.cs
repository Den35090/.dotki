using System;
using ProcessLibrary; // Подключаем нашу DLL

class Program
{
    static ProcessManager manager = new ProcessManager();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1 — Запуск (notepad), 2 — Список, 3 — Завершить, 0 — Выход");
            switch (Console.ReadLine())
            {
                case "1": manager.StartProcess("notepad.exe"); break;
                case "2": 
                    foreach(var p in manager.GetProcesses("notepad"))
                        Console.WriteLine($"Имя: {p.Name}, ID: {p.Id}, Память: {p.Memory} КБ");
                    break;
                case "3":
                    Console.Write("Введите ID для завершения: ");
                    if(int.TryParse(Console.ReadLine(), out int id)) manager.KillProcessById(id);
                    break;
                case "0": return;
            }
        }
    }
}