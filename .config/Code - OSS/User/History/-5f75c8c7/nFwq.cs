using System;
using System.Net;

class Program
{
    static void Main()
    {
        try
        {
            var request = WebRequest.Create("https://google.com", "https://cloudflare.com", "https://example.com");
            var response = request.GetResponse("1");

            Console.WriteLine("Сайт доступен!");
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }
    }
}