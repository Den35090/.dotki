using System;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static decimal[] accountBalances = { 1000m, 2000m, 3000m, 4000m, 5000m };
    
    static Mutex[] accountMutexes = {
        new Mutex(), new Mutex(), new Mutex(), new Mutex(), new Mutex()
    };

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== БАНКОМАТ ===");
            Console.WriteLine("1 - Показать балансы");
            Console.WriteLine("2 - Снять деньги");
            Console.WriteLine("3 - Перевести деньги ");
            Console.WriteLine("0 - Выход");
            Console.Write("\nВыбор: ");

            string choice = Console.ReadLine();

            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    ShowBalances();
                    break;
                case "2":
                    WithdrawMoneyUI();
                    break;
                case "3":
                    TransferMoneyUI();
                    break;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }

    static void ShowBalances()
    {
        Console.WriteLine("\nТекущие балансы:");
        for (int i = 0; i < accountBalances.Length; i++)
        {
            accountMutexes[i].WaitOne();
            Console.WriteLine($"Счет #{i}: {accountBalances[i]} грн.");
            accountMutexes[i].ReleaseMutex();
        }
    }

    static void WithdrawMoneyUI()
    {
        Console.Write("Введите номер счета: ");
        int acc = int.Parse(Console.ReadLine());
        Console.Write("Сумма снятия: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        Thread thread = new Thread(() => WithdrawAction(acc, amount));
        thread.Start();
    }

    static void WithdrawAction(int acc, decimal amount)
    {
        Console.WriteLine($"\nОжидание доступа к счету #{acc}...");
        
        accountMutexes[acc].WaitOne();
        
        Console.WriteLine($"Проверка счета #{acc}...");
        Thread.Sleep(1500);

        if (accountBalances[acc] >= amount)
        {
            accountBalances[acc] -= amount;
            Console.WriteLine($"УСПЕХ Снято {amount}. Новый баланс счета #{acc}: {accountBalances[acc]}");
        }
        else
        {
            Console.WriteLine($"ОШИБКА Недостаточно средств на счете #{acc}");
        }

        accountMutexes[acc].ReleaseMutex();
    }

    static void TransferMoneyUI()
    {
        Console.Write("С какого счета (0-4): ");
        int from = int.Parse(Console.ReadLine());
        Console.Write("На какой счет (0-4): ");
        int to = int.Parse(Console.ReadLine());
        Console.Write("Сумма перевода: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        Thread thread = new Thread(() => TransferAction(from, to, amount));
        thread.Start();
    }

    static void TransferAction(int from, int to, decimal amount)
    {
        if (from == to) return;

        int first = Math.Min(from, to);
        int second = Math.Max(from, to);

        Console.WriteLine($"\n[Поток {Thread.CurrentThread.ManagedThreadId}] Ожидание блокировки счетов {from} и {to}...");

        accountMutexes[first].WaitOne();
        accountMutexes[second].WaitOne();

        Console.WriteLine($"[Поток {Thread.CurrentThread.ManagedThreadId}] Выполнение перевода {amount} со счета {from} на {to}...");
        Thread.Sleep(2000);
        if (accountBalances[from] >= amount)
        {
            accountBalances[from] -= amount;
            accountBalances[to] += amount;
            Console.WriteLine($"[УСПЕХ] Перевод завершен. {from}: {accountBalances[from]} | {to}: {accountBalances[to]}");
        }
        else
        {
            Console.WriteLine($"[ОШИБКА] Недостаточно средств на счете {from}");
        }

        accountMutexes[second].ReleaseMutex();
        accountMutexes[first].ReleaseMutex();
    }
}