using System;
using System.Net;

class Program
{
    static void Main()
    {
        Console.Write("Enter site address ==> ");
        string site = Console.ReadLine();

        Console.Write("\n");

        var ips = Dns.GetHostAddresses(site7);

        foreach (var ip in ips)
        {
            Console.WriteLine(ip);
        }

        try
        {
            var request = WebRequest.Create("https://" + site);
            var response = request.GetResponse();
            Console.WriteLine("\nSite available");
        }
        catch
        {
            Console.WriteLine("\nSite not available");
        }
    }
}