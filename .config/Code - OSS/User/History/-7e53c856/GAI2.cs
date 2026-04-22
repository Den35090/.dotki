using System;
using System.Threading;

class Program
{
    static int _itemsInStock = 20;
    static Mutex _checkoutMutex = new Mutex();
    static Mutex _stockMutex = new Mutex();

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== МАГАЗИН ===");
            Console.WriteLine("1 - Показать остаток");
            Console.WriteLine("2 - Купить товар");
            Console.WriteLine("3 - Добавить товар на склад");
            Console.WriteLine("0 - Выход");
            Console.Write("\nВыбор: ");

            string choice = Console.ReadLine();
            if (choice == "0") break;

            switch (choice)
            {
                case "1":
                    ShowStock();
                    break;
                case "2":
                    CreateCustomer();
                    break;
                case "3":
                    RestockUI();
                    break;
            }
            Console.WriteLine("\nНажмите клавишу...");
            Console.ReadKey();
        }
    }

    static void ShowStock()
    {
        _stockMutex.WaitOne();
        Console.WriteLine($"\nАктуальный остаток: {_itemsInStock}");
        _stockMutex.ReleaseMutex();
    }

    static void CreateCustomer()
    {
        Console.Write("Сколько товаров хочет купить клиент? ");
        if (int.TryParse(Console.ReadLine(), out int count))
        {
            Thread customer = new Thread(() => CustomerProcess(count));
            customer.Start();
        }
    }

    static void CustomerProcess(int count)
    {
        int id = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"\n[Клиент {id}] Выбирает товары...");
        Thread.Sleep(1000);

        _checkoutMutex.WaitOne();
        Console.WriteLine($"Обслуживание клиента {id}...");

        _stockMutex.WaitOne();
        if (_itemsInStock >= count)
        {
            Thread.Sleep(1500);
            _itemsInStock -= count;
            Console.WriteLine($"Клиент {id} купил {count} шт. Осталось: {_itemsInStock}");
        }
        else
        {
            Console.WriteLine($"Ошибка: Клиенту {id} не хватило товара (нужно {count}, есть {_itemsInStock})");
        }
        _stockMutex.ReleaseMutex();

        Console.WriteLine($"Клиент {id} ушел.");
        _checkoutMutex.ReleaseMutex();
    }

    static void RestockUI()
    {
        Console.Write("Сколько товара добавить? ");
        if (int.TryParse(Console.ReadLine(), out int count))
        {
            _stockMutex.WaitOne();
            _itemsInStock += count;
            Console.WriteLine($"Склад пополнен на {count}. Всего: {_itemsInStock}");
            _stockMutex.ReleaseMutex();
        }
    }
}