using System;
using System.Linq;
using ParallelLibrary;

class Program
{
    static void Main()
    {
        int[] data = Enumerable.Range(1, 100_000)
                               .Select(_ => new Random().Next(1, 1000))
                               .ToArray();

        ParallelCalculator calc = new ParallelCalculator();
        var result = calc.ProcessAll(data, 10);

        Console.WriteLine("Результаты вычислений:");
        Console.WriteLine($"Сумма: {result.Sum}");
        Console.WriteLine($"Максимум: {result.Max}");
        Console.WriteLine($"Кратных 10: {result.MultiplesCount}");
        Console.WriteLine($"Среднее: {result.Average:F2}");
        
        Console.ReadKey();
    }
}