using System;
using System.Net;

class Program
{
    static void Main()
    {
        try
        {
            var console.WriteLine request = WebRequest.Create("https://google.com");
            var response = request.GetResponse();

            Console.WriteLine("Сайт доступен!");
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }
    }
}