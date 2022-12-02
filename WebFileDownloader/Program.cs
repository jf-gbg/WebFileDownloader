using System;
using System.Threading;
using WebFileDownloader;
using WebFileDownloader.Classes;

public class Program
{
    static WebClientAdapter webClientAdapter = new WebClientAdapter();

    public static void Main()
    {
        Download("https://www1.ncdc.noaa.gov/pub/data/swdi/stormevents/csvfiles");
        Console.ReadLine();
    }

    private static void Download(string url)
    {
        var client = new WebClientAdapter();
        var waiter = new ManualResetEventSlim();

        using (waiter)
        {
            client.InvalidUrlRequested += (sender, args) =>
            {
                var oldColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Invalid URL {args}");
                Console.BackgroundColor = oldColor;
            };

            client.DownloadProgressChanged += (sender, args) =>
            {
                Console.WriteLine($"Downloading... {args.ProgressPercentage}% complete ({args.BytesReceived:NO})");
            };

            client.DownloadCompleted += (sender, args) => 
            {
                Console.WriteLine($"Download file");
                waiter.Set();
            };

            Console.WriteLine($"Downloading {url}");
            var request = client.DownloadFile(url);
            if (request == null)
                return;
            using (request)
            {
                if(!waiter.Wait(TimeSpan.FromSeconds(10D)))
                {
                    Console.WriteLine($"Timedout downloading {url}");
                }
            }
        }
    }
}