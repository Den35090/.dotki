using System;
using MathLibrary; // Подключаем пространство имен из DLL

class Program
{
    static void Main()
    {
        int number = 5;

        Console.WriteLine($"Число: {number}");
        Console.WriteLine($"Факториал: {Calculator.Factorial(number)}");
        Console.WriteLine($"Простое: {Calculator.IsPrime(number)}");
        Console.WriteLine($"Четное: {Calculator.IsEven(number)}");
        Console.WriteLine($"Нечетное: {Calculator.IsOdd(number)}");

        Console.ReadKey();
    }
}