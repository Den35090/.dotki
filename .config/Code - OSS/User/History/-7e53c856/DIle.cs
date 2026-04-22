using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.Write("Введите число для анализа: ");
        string input = Console.ReadLine() ?? "0";

        if (!long.TryParse(input, out long number) || number < 0)
        {
            Console.WriteLine("Введите корректное положительное число.");
            return;
        }

        BigInteger factorialResult = 1;
        int digitsCount = 0;
        int digitsSum = 0;

        Parallel.Invoke(
            () =>
            {
                factorialResult = CalculateFactorial(number);
                Console.WriteLine("Факториал вычислен.");
            },
            () =>
            {
                digitsCount = input.Length;
                Console.WriteLine("Количество цифр подсчитано.");
            },
            () =>
            {
                digitsSum = input.Where(char.IsDigit).Sum(c => c - '0');
                Console.WriteLine("Сумма цифр вычислена.");
            }
        );

        Console.WriteLine("\n--- РЕЗУЛЬТАТЫ ---");
        Console.WriteLine($"Число: {number}");
        Console.WriteLine($"Факториал: {factorialResult}");
        Console.WriteLine($"Количество цифр: {digitsCount}");
        Console.WriteLine($"Сумма цифр: {digitsSum}");
        
        Console.ReadKey();
    }

    static BigInteger CalculateFactorial(long n)
    {
        if (n == 0) return 1;
        
        BigInteger result = 1;
        object lockObject = new object();

        Parallel.For(1, n + 1, () => (BigInteger)1, (i, state, localValue) =>
        {
            return localValue * i;
        },
        localResult =>
        {
            lock (lockObject)
            {
                result *= localResult;
            }
        });

        return result;
    }
}