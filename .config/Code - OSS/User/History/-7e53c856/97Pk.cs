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
                Console.WriteLine($"Клиент {id} начал мойку машины.");
                Thread.Sleep(2000);
                Console.WriteLine($"Клиент {id} выехал с мойки.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}