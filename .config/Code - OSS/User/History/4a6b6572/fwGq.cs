using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n1 — Start processes\n2 — List processes\n3 — Terminate processes\n4 — Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StartProcesses();
                    break;
                case "2":
                    ListProcesses();
                    break;
                case "3":
                    TerminateProcesses();
                    break;
                case "4":
                    return;
            }
        }
    }

    static void StartProcesses()
    {
        try
        {
            Process.Start("notepad.exe");
            Process.Start("calc.exe");
            Console.WriteLine("Processes started. Waiting 5 seconds...");
            Thread.Sleep(5000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting process: {ex.Message}");
        }
    }

    static void ListProcesses()
    {
        Console.WriteLine("\nEnter process name to filter (or press Enter for all):");
        string filter = Console.ReadLine().ToLower();

        var processes = Process.GetProcesses();

        Console.WriteLine($"{"Name",-20} | {"ID",-10} | {"Memory (MB)",-15}");
        Console.WriteLine(new string('-', 50));

        foreach (var p in processes)
        {
            try
            {
                if (string.IsNullOrEmpty(filter) || p.ProcessName.ToLower().Contains(filter))
                {
                    long memory = p.WorkingSet64 / 1024 / 1024;
                    Console.WriteLine($"{p.ProcessName,-20} | {p.Id,-10} | {memory,-15}");
                }
            }
            catch (Exception)
            {
                continue;
            }
        }
    }

    static void TerminateProcesses()
    {
        Console.WriteLine("Terminating notepad and calc...");
        string[] targetNames = { "notepad", "calc" };

        foreach (string name in targetNames)
        {
            var processes = Process.GetProcessesByName(name);
            foreach (var p in processes)
            {
                try
                {
                    p.Kill();
                    p.WaitForExit();
                    Console.WriteLine($"Process {p.ProcessName} (ID: {p.Id}) terminated.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not terminate {name}: {ex.Message}");
                }
            }
        }
    }
}