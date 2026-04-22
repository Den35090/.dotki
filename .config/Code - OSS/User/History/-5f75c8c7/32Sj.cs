using System;
using System.Net;

class Program
{
    static void Main()
    {
        Console.Write("Введите сайт ==> ");
        string site = Console.ReadLine();

        try
        {
            WebRequest request = WebRequest.Create(site);
            
            using (WebResponse response = request.GetResponse())
            {
                Console.WriteLine("Сайт доступен!");
            }
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }

        try
        {
            string hostName = new Uri(site).Host;
            var ips = Dns.GetHostAddresses(hostName);

            Console.WriteLine("\nIP адреса:");
            foreach (var ip in ips)
            {
                Console.WriteLine(ip);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка получения IP: " + ex.Message);
        }
    }
}