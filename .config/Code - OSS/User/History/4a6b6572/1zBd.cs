using System;
using GeometryLibrary;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine($"Площадь круга (r=5): {GeometryService.GetArea(5):F2}");
            Console.WriteLine($"Площадь прямоугольника (4x6): {GeometryService.GetArea(4, 6)}");
            
            double a = 5, b = 5, c = 5;
            Console.WriteLine($"Периметр треугольника ({a},{b},{c}): {GeometryService.GetPerimeter(a, b, c)}");
            Console.WriteLine($"Равносторонний? {GeometryService.IsEquilateral(a, b, c)}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка валидации: {ex.Message}");
        }
    }
}