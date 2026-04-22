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
            int count = i * 3;
            new Thread(() => {
                mutex.WaitOne();
                Console.WriteLine($"Клиент {id} стирает {count} вещей.");
                Thread.Sleep(1500);
                Console.WriteLine($"Клиент {id} закончил стирку.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}