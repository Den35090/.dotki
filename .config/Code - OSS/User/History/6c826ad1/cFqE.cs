using System;

namespace GeometryLibrary
{
    public static class GeometryService
    {
        // Площадь круга
        public static double GetArea(double radius)
        {
            if (radius < 0) throw new ArgumentException("Радиус не может быть отрицательным.");
            return Math.PI * Math.Pow(radius, 2);
        }

        // Перегрузка: площадь прямоугольника
        public static double GetArea(double width, double height)
        {
            if (width < 0 || height < 0) throw new ArgumentException("Стороны не могут быть отрицательными.");
            return width * height;
        }

        // Периметр треугольника
        public static double GetPerimeter(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0) throw new ArgumentException("Стороны должны быть > 0.");
            if (a + b <= c || a + c <= b || b + c <= a) throw new ArgumentException("Некорректный треугольник.");
            return a + b + c;
        }

        // Проверка на равносторонность
        public static bool IsEquilateral(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0) throw new ArgumentException("Стороны должны быть > 0.");
            return a == b && b == c;
        }
    }
}