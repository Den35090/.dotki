using System;
using System.Threading;

class Program
{
    private static Mutex mutex = new Mutex();
    static void Main()
    {
        for (int i = 1; i <= 3; i++)
        {
            int id = i;
            new Thread(() => {
                mutex.WaitOne();
                Console.WriteLine($"Сотрудник {id} копирует документы...");
                Thread.Sleep(new Random().Next(1000, 2000));
                Console.WriteLine($"Сотрудник {id} завершил копирование.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}