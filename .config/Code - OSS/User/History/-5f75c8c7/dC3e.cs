using System;
using System.Net;
using System.Net.NetworkInformation;

class Program
{
    static void Main()
    {
        string[] site = { "https://google.com", "https://wikippedia.com", "https://example.com/" };

        int SiteAvailableCount = 0;
        string fastestSite = "";
        long minTime = long.MaxValue;

        foreach (string name in site)
        {
            try
            {
                WebRequest request = (WebRequest)WebRequest.Create(name);

                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"{name} ==> available");
                    SiteAvailableCount++;
                    if (sw.ElapsedMilliseconds < minTime) { minTime = sw.ElapsedMilliseconds; fastestSite = site; }
                }
            }
            catch
            {
                Console.WriteLine($"{name} ==> not available");
            }
        }
        Console.WriteLine("\nSite available ==> " + SiteAvailableCount);
    }
}