using System;
using System.Threading;

namespace SynchronizationTasks
{
    public class Chef
    {
        private static Mutex _mutex = new Mutex();
        private Random _rnd = new Random();

        public void Start()
        {
            for (int i = 1; i <= 5; i++)
            {
                int id = i;
                new Thread(() => Cook(id)).Start();
            }
        }

        private void Cook(int id)
        {
            _mutex.WaitOne();
            Console.WriteLine($"Chef {id} is cooking...");
            Thread.Sleep(_rnd.Next(1000, 2000));
            Console.WriteLine($"Chef {id} finished.");
            _mutex.ReleaseMutex();
        }
    }
}