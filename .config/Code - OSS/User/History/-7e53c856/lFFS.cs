using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArrayAnalysisTask
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[20];
            Random rnd = new Random();

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rnd.Next(-10, 11);
            }

            Console.WriteLine("Массив: " + string.Join(", ", numbers));

            int positiveCount = 0;
            int negativeCount = 0;
            int zeroCount = 0;
            long product = 1;

            Task[] tasks = new Task[4];

            tasks[0] = Task.Run(() =>
            {
                positiveCount = numbers.Count(n => n > 0);
            });

            tasks[1] = Task.Run(() =>
            {
                negativeCount = numbers.Count(n => n < 0);
            });

            tasks[2] = Task.Run(() =>
            {
                zeroCount = numbers.Count(n => n == 0);
            });

            tasks[3] = Task.Run(() =>
            {
                foreach (var n in numbers)
                {
                    product *= n;
                }
            });

            Task.WaitAll(tasks);

            Console.WriteLine("\n--- РЕЗУЛЬТАТЫ ---");
            Console.WriteLine($"Положительных: {positiveCount}");
            Console.WriteLine($"Отрицательных: {negativeCount}");
            Console.WriteLine($"Нулей: {zeroCount}");
            Console.WriteLine($"Произведение: {product}");

            Console.ReadKey();
        }
    }
}