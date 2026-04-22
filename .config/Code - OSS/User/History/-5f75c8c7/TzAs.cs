using System;
using System.Net;

class Program
{
    static void Main()
    {
        try
        {
            var request = WebRequest.Create("https://google.com");
            var response = request.GetResponse();

            Console.WriteLine("Сайт доступен!");
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }
        Console.Write("Введите сайт: ");
        string site = Console.ReadLine();

        var ips = Dns.GetHostAddresses(site);

        foreach (var ip in ips)
        {
            Console.WriteLine(ip);
        }
    }
}