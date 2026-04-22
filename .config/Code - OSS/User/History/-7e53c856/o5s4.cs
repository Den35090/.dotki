using System;
using System.Threading.Tasks;

namespace TaskAreaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите длину стороны A: ");
            double sideA = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Введите длину стороны B: ");
            double sideB = double.Parse(Console.ReadLine() ?? "0");

            double area = 0;
            double perimeter = 0;

            Task task1 = new Task(() =>
            {
                area = sideA * sideB;
                Console.WriteLine("Task 1 finished");
            });
            task1.Start();

            Task task2 = Task.Factory.StartNew(() =>
            {
                perimeter = 2 * (sideA + sideB);
                Console.WriteLine("Task 2 finished");
            });

            Task task3 = Task.Run(() =>
            {
                Console.WriteLine("Task 3 finished");
            });

            Task.WaitAll(task1, task2, task3);

            Console.WriteLine("\n--- РЕЗУЛЬТАТЫ ---");
            Console.WriteLine($"Стороны: {sideA} x {sideB}");
            Console.WriteLine($"Площадь: {area}");
            Console.WriteLine($"Периметр: {perimeter}");
            
            Console.ReadKey();
        }
    }
}