using System;
using System.Threading;

namespace BankSystemDapper.SystemProgramming
{
    public class ThreadService
    {
        public void LoadDataAsync()
        {
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("\n[Поток] Начинаю фоновую загрузку данных...");
                Thread.Sleep(3000); // Имитация долгой работы
                Console.WriteLine("\n[Поток] Данные успешно кэшированы в фоне!");
            });
            thread.Start();
        }
    }
}