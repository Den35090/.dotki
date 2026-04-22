using System;
using System.Net;

class Program
{
    static void Main()
    {
        Console.Write("Введите сайт (например, https://google.com): ");
        string site = Console.ReadLine();

        // Part 1: Check availability
        try
        {
            // Create a request to the URL provided by the user
            WebRequest request = WebRequest.Create(site);
            
            // Send the request and check the response
            using (WebResponse response = request.GetResponse())
            {
                Console.WriteLine("Сайт доступен!");
            }
        }
        catch
        {
            Console.WriteLine("Ошибка подключения");
        }

        // Part 2: Get and print IP addresses
        try
        {
            // Extract the host name from the URL (removes https://)
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