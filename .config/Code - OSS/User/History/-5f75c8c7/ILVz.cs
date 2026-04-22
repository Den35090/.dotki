using System;
using System.Net;

class Program
{
    static void Main()
    {
        Console.Write("Введите сайт: ");
        string site = Console.ReadLine();
        var response = site.GetResponse();
        Console.WriteLine("Сайт доступен!");
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }

        var ips = Dns.GetHostAddresses(site);

        foreach (var ip in ips)
        {
            Console.WriteLine(ip);
        }
    }
}