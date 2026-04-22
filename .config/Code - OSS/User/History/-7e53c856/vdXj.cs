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
                Console.WriteLine($"Садовник {id} поливает цветы.");
                Thread.Sleep(1200);
                Console.WriteLine($"Садовник {id} закончил полив.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}