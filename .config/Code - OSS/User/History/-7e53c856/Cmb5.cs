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
                Console.WriteLine($"Клиент {id}: фотосессия началась.");
                Thread.Sleep(new Random().Next(1000, 2000));
                Console.WriteLine($"Клиент {id}: фотосессия окончена.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}