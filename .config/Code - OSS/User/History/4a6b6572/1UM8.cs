using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MathLibrary; // Подключаем нашу DLL

class Program
{
    static void Main()
    {
        int size = 1_000_000;
        int[] numbers = new int[size];
        Random rnd = new Random();
        for (int i = 0; i < size; i++) numbers[i] = rnd.Next(1, 10000);

        Console.WriteLine("Начинаем параллельный анализ...");
        Stopwatch sw = Stopwatch.StartNew();

        long sum = 0;
        int max = int.MinValue;
        int evenCount = 0;
        int primeCount = 0;
        object lockObj = new object();

        Task[] tasks = new Task[4];

        tasks[0] = Task.Run(() => sum = numbers.Sum(x => (long)x));

        tasks[1] = Task.Run(() => {
            int localMax = numbers.AsParallel().Max();
            lock(lockObj) { if (localMax > max) max = localMax; }
        });

        tasks[2] = Task.Run(() => evenCount = numbers.AsParallel().Count(n => n % 2 == 0));

        tasks[3] = Task.Run(() => {
            int count = 0;
            // Использование метода из DLL
            Parallel.ForEach(numbers, n => {
                if (MathUtils.IsPrime(n)) Interlocked.Increment(ref count);
            });
            primeCount = count;
        });

        Task.WaitAll(tasks);
        sw.Stop();

        Console.WriteLine($"\nВремя: {sw.ElapsedMilliseconds} мс");
        Console.WriteLine($"Сумма: {sum}, Макс: {max}, Четных: {evenCount}, Простых: {primeCount}");
    }
}