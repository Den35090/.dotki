using System;
using System.Threading;

class Program
{
    static readonly int MachineCount = 3;
    static Mutex[] machineMutexes = new Mutex[MachineCount];
    static bool[] isMachineBusy = new bool[MachineCount];

    static void Main()
    {
        for (int i = 0; i < MachineCount; i++)
        {
            machineMutexes[i] = new Mutex();
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ГОРОДСКАЯ ПРАЧЕЧНАЯ ===");
            Console.WriteLine($"Всего машин: {MachineCount}");
            Console.WriteLine("1 - Показать состояние машин");
            Console.WriteLine("2 - Постирать вещи (Новый клиент)");
            Console.WriteLine("0 - Выход");
            Console.Write("\nВаш выбор: ");

            string choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    ShowStatus();
                    break;
                case "2":
                    Console.Write("Сколько вещей принес клиент? ");
                    if (int.TryParse(Console.ReadLine(), out int count))
                    {
                        Thread clientThread = new Thread(() => WashProcess(count));
                        clientThread.Start();
                    }
                    break;
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }

    static void WashProcess(int itemCount)
    {
        int clientId = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"\n[Клиент {clientId}] Ожидает свободную машину...");

        // Ждем, пока освободится ЛЮБАЯ из машин
        int machineId = WaitHandle.WaitAny(machineMutexes);

        try
        {
            isMachineBusy[machineId] = true;
            Console.WriteLine($"\n[Машина #{machineId}] Клиент {clientId} начал стирку ({itemCount} вещ.)");
            
            // Имитация процесса стирки
            Thread.Sleep(new Random().Next(4000, 7000));

            Console.WriteLine($"\n[Машина #{machineId}] СТИРКА ОКОНЧЕНА. Клиент {clientId} забирает {itemCount} вещ.");
        }
        finally
        {
            isMachineBusy[machineId] = false;
            machineMutexes[machineId].ReleaseMutex();
        }
    }

    static void ShowStatus()
    {
        Console.WriteLine("\n--- ТЕКУЩЕЕ СОСТОЯНИЕ ---");
        for (int i = 0; i < MachineCount; i++)
        {
            string status = isMachineBusy[i] ? "ЗАГРУЖЕНА" : "СВОБОДНА";
            Console.WriteLine($"Стиральная машина #{i}: {status}");
        }
    }
}