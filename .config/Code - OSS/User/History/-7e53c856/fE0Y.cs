using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenNumbersTask
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> evenNumbers = new List<int>();

            Task task = Task.Run(() =>
            {
                for (int i = 1; i <= 5000; i++)
                {
                    if (i % 2 == 0)
                    {
                        evenNumbers.Add(i);
                    }
                }
            });

            task.Wait();

            Console.WriteLine($"Количество найденных чисел: {evenNumbers.Count}");

            Console.WriteLine("\nПервые 10 чисел:");
            var firstTen = evenNumbers.Take(10);
            foreach (var num in firstTen)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine("\n\nПоследние 10 чисел:");
            var lastTen = evenNumbers.Skip(Math.Max(0, evenNumbers.Count - 10));
            foreach (var num in lastTen)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}