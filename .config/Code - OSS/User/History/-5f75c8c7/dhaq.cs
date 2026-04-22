using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;

class Task2
{
    public static async Task Run()
    {
        string[] sites = { "google.com", "example.com", "nonexistent-site-test.xyz" };
        int availableCount = 0;
        string fastestSite = "";
        long minTime = long.MaxValue;

        using HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(3);

        foreach (var site in sites)
        {
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                var response = await client.GetAsync("https://" + site);
                sw.Stop();
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{site} — доступен ({sw.ElapsedMilliseconds} мс)");
                    availableCount++;
                    if (sw.ElapsedMilliseconds < minTime) { minTime = sw.ElapsedMilliseconds; fastestSite = site; }
                }
            }
            catch { Console.WriteLine($"{site} — ошибка"); }
        }
        Console.WriteLine($"\nДоступно: {availableCount}, Самый быстрый: {fastestSite}");
    }
}