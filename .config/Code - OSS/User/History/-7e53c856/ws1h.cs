using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static readonly int PrinterCount = 3;
    
    static Mutex[] printerMutexes = new Mutex[PrinterCount];
    
    static bool[] printerStatus = new bool[PrinterCount];

    static void Main()
    {
        for (int i = 0; i < PrinterCount; i++)
        {
            printerMutexes[i] = new Mutex();
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ПЕЧАТНЫЙ ЦЕНТР ===");
            Console.WriteLine($"Доступно принтеров: {PrinterCount}");
            Console.WriteLine("1 - Отправить документ на печать");
            Console.WriteLine("2 - Проверить статус принтеров");
            Console.WriteLine("0 - Выход");
            Console.Write("\nВыбор: ");

            string choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    Console.Write("Введите название документа: ");
                    string docName = Console.ReadLine() ?? "Без названия";
                    Thread userThread = new Thread(() => PrintDocument(docName));
                    userThread.Start();
                    break;

                case "2":
                    CheckStatus();
                    break;
            }
            Console.WriteLine("\nНажмите клавишу...");
            Console.ReadKey();
        }
    }

    static void PrintDocument(string docName)
    {
        int printerId = -1;
        int threadId = Thread.CurrentThread.ManagedThreadId;

        Console.WriteLine($"\n[Пользователь {threadId}] Документ '{docName}' в очереди на печать...");

        printerId = WaitHandle.WaitAny(printerMutexes);

        try
        {
            printerStatus[printerId] = true;
            Console.WriteLine($"\n[Принтер #{printerId}] Начата печать: {docName}");
            
            Thread.Sleep(new Random().Next(3000, 6000));

            Console.WriteLine($"\n[Принтер #{printerId}] Готово! Документ '{docName}' напечатан.");
        }
        finally
        {
            printerStatus[printerId] = false;
            printerMutexes[printerId].ReleaseMutex();
        }
    }

    static void CheckStatus()
    {
        Console.WriteLine("\n--- Статус принтеров ---");
        for (int i = 0; i < PrinterCount; i++)
        {
            string status = printerStatus[i] ? "ЗАНЯТ" : "СВОБОДЕН";
            Console.WriteLine($"Принтер #{i}: {status}");
        }
    }
}