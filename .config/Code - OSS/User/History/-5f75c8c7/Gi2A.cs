using System;
using System.Net;

class Program
{
    static void Main()
    {
        try
        {
            var request = WebRequest.Create("https://google.com", "https://cloudflare.com", "example.com");
            var response = request.GetResponse();
            int availableCount = 0;
            string fastestSite = "";
            long minTime = long.MaxValue;

            Console.WriteLine("Сайт доступен!");
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }
    }
}