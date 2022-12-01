using System;
using System.Threading;
using WebFileDownloader;
using WebFileDownloader.Classes;

public class Program
{
    static WebClientAdapter webClientAdapter = new WebClientAdapter();

    public static void Main()
    {
        webClientAdapter.DownloadCompleted += DownloadCompleted;
        Console.ReadLine();
    }

    private static void Download(string url, string destination)
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
        }
    }
}