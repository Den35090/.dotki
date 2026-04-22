using System;
using MathLibrary;

class Program
{
    static void Main()
    {
        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(2, 3);

        Fraction result = f1.Multiply(f2);

        Console.WriteLine($"Дробь 1: {f1}");
        Console.WriteLine($"Дробь 2: {f2}");
        Console.WriteLine($"Результат умножения: {result}");
        
        Console.ReadKey();
    }
}