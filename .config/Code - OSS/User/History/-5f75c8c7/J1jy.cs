using System;
using System.Net;

class Program
{
    static void Main()
    {
        try
        {
            var request = WebRequest.Create("https://google.com","https://cloudflare.com");
            var response = request.GetResponse();

            Console.WriteLine("Сайт доступен!");
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }
    }
}