using System;
using System.Threading;

namespace BankSystemDapper.SystemProgramming
{
    public class ThreadService
    {
        public void LoadDataInThread() // Название должно быть в точности таким
        {
            Thread thread = new Thread(() => {
                Console.WriteLine("\n[THREAD] Фоновая загрузка...");
                Thread.Sleep(2000);
                Console.WriteLine("[THREAD] Готово!");
            });
            thread.Start();
        }
    }
}