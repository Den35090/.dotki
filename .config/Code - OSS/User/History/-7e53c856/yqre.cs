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
                DateTime start = DateTime.Now;
                Console.WriteLine($"Посетитель {id} кормит животное.");
                Thread.Sleep(1500);
                Console.WriteLine($"Посетитель {id} потратил {(DateTime.Now - start).TotalSeconds} сек.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}