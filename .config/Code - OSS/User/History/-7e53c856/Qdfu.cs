using System;
using System.Threading;

class Program
{
    private static Mutex mutex = new Mutex();
    static void Main()
    {
        for (int i = 1; i <= 3; i++)
        {
            int orderId = new Random().Next(100, 999);
            new Thread(() => {
                mutex.WaitOne();
                Console.WriteLine($"Начало выдачи заказа №{orderId}");
                Thread.Sleep(1000);
                Console.WriteLine($"Заказ №{orderId} выдан.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}