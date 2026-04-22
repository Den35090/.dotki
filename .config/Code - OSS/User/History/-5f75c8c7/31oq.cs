using System;
using System.Net;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Введите сайт: ");
            var response = console.ReadLine.GetResponse();

            Console.WriteLine("Сайт доступен!");
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }
        
        string site = Console.ReadLine();

        var ips = Dns.GetHostAddresses(site);

        foreach (var ip in ips)
        {
            Console.WriteLine(ip);
        }
    }
}