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
                Console.WriteLine($"Рабочий {id} за станком: начало обработки.");
                Thread.Sleep(2000);
                Console.WriteLine($"Рабочий {id} за станком: деталь готова.");
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}