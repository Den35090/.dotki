using System;
using System.Net;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string[] site = { "https://google.com", "https://wikipedia.com", "https://example.com/" };

        int SiteAvailableCount = 0;
        string fastestSite = "";
        long minTime = long.MaxValue;

        foreach (string name in site)
        {
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                WebRequest request = WebRequest.Create(name);
                using (WebResponse response = request.GetResponse())
                {
                    sw.Stop();
                    Console.WriteLine($"{name} ==> available");
                    SiteAvailableCount++;
                    
                    if (sw.ElapsedMilliseconds < minTime) 
                    { 
                        minTime = sw.ElapsedMilliseconds; 
                        fastestSite = name; 
                    }
                }
            }
            catch
            {
                Console.WriteLine($"{name} ==> not available");
            }
        }
        
        Console.WriteLine("\nSite available ==> " + SiteAvailableCount + " | fastest ==> " + fastestSite);
    }
}