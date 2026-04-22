using System;
using System.Net;

class Program
{
    static void Main()
    {
        Console.Write("Введите сайт: ");
        try
        {
            var response = Console.ReadLine.GetResponse();

            Console.WriteLine("Сайт доступен!");
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }
        }
        string site = Console.ReadLine();

        var ips = Dns.GetHostAddresses(site);

        foreach (var ip in ips)
        {
            Console.WriteLine(ip);
        }
    }
}