using System;
using System.Collections.Generic;
using ShopLibrary;

class Program
{
    static void Main()
    {
        List<Product> items = new List<Product>
        {
            new Product("Laptop", 1200, 5),
            new Product("Mouse", 25, 50),
            new Product("Keyboard", 75, 20),
            new Product("Monitor", 300, 10),
            new Product("Cable", 10, 100)
        };

        ProductService service = new ProductService(items);

        Console.WriteLine("Все товары:");
        foreach (var p in service.GetAll()) Console.WriteLine($"{p.Name} - {p.Price}$");

        var found = service.Find("Mouse");
        Console.WriteLine($"\nНайден товар: {found?.Name ?? "Не найден"}");

        Console.WriteLine("\nТовары от 50$ до 500$:");
        foreach (var p in service.Find(50, 500)) Console.WriteLine(p.Name);

        Console.WriteLine($"\nОбщая стоимость склада: {service.GetTotalValue()}$");

        Console.WriteLine("\nТовары дороже 200$:");
        foreach (var p in service.GetExpensiveProducts(200)) Console.WriteLine(p.Name);

        Console.ReadKey();
    }
}