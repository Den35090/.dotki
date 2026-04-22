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
                Console.WriteLine($"Повар {id} готовит на плите...");
                Thread.Sleep(1500);
                Console.WriteLine($"Повар {id} закончил.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}