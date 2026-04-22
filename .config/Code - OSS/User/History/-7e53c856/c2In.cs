using System;
using System.Threading;

class Program
{
    private static Mutex mutex = new Mutex();
    private static int books = 5;
    static void Main()
    {
        for (int i = 1; i <= 3; i++)
        {
            int id = i;
            new Thread(() => {
                mutex.WaitOne();
                books--;
                Console.WriteLine($"Читатель {id} взял книгу. Осталось: {books}");
                Thread.Sleep(500);
                mutex.ReleaseMutex();
            }).Start();
        }
    }
}