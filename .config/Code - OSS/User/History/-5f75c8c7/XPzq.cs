using System;
using System.Net;
using System.Net.NetworkInformation;

class Program
{
    static void Main()
    {
        string[] site = { "https://google.com", "https://wikippedia.com", "https://example.com/" };

        int SiteAvailableCount = 0;

        foreach (string name in site)
        {
            try
            {
                WebRequest request = (WebRequest)WebRequest.Create(name);

                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"{name} >>> available");
                    SiteAvailableCount++;
                }
            }
            catch
            {
                Console.WriteLine($"{name} >>> not available");
            }
        }
        Console.WriteLine("\nSite available >>> " + SiteAvailableCount);
    }
}