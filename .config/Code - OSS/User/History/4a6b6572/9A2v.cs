using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int size = 1_000_000;
        int[] numbers = new int[size];
        Random rnd = new Random();
        for (int i = 0; i < size; i++) numbers[i] = rnd.Next(1, 10000);

        Console.WriteLine($"Начинаем анализ {size} чисел в многопоточном режиме...");

        Stopwatch sw = Stopwatch.StartNew();

        long sum = 0;
        int max = int.MinValue;
        int evenCount = 0;
        int primeCount = 0;
        object maxLock = new object();

        Task[] tasks = new Task[4];

        // 1. Сумма
        tasks[0] = Task.Run(() => sum = numbers.Sum(x => (long)x));

        // 2. Максимум
        tasks[1] = Task.Run(() => {
            int localMax = numbers.AsParallel().Max();
            lock(maxLock) { if (localMax > max) max = localMax; }
        });

        // 3. Количество чётных
        tasks[2] = Task.Run(() => evenCount = numbers.AsParallel().Count(n => n % 2 == 0));

        // 4. Количество простых чисел
        tasks[3] = Task.Run(() => {
            int count = 0;
            Parallel.ForEach(numbers, n => {
                if (IsPrime(n)) Interlocked.Increment(ref count);
            });
            primeCount = count;
        });

        Task.WaitAll(tasks);
        sw.Stop();

        Console.WriteLine("\n--- РЕЗУЛЬТАТЫ ---");
        Console.WriteLine($"Время выполнения: {sw.ElapsedMilliseconds} мс");
        Console.WriteLine($"Сумма: {sum}");
        Console.WriteLine($"Максимум: {max}");
        Console.WriteLine($"Чётных: {evenCount}");
        Console.WriteLine($"Простых: {primeCount}");
        
        Console.ReadKey();
    }

    static bool IsPrime(int n)
    {
        if (n < 2) return false;
        for (int i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0) return false;
        return true;
    }
}