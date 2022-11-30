using System;
using WebFileDownloader.Classes;

public class Program
{
    public static void Main()
    {
        DownloadCompleted();
        Console.ReadLine();
    }

    static void DownloadCompleted()
    {
        WebClientAdapter webClientAdapter = new WebClientAdapter();
        webClientAdapter.DownloadFile("https://www1.ncdc.noaa.gov/pub/data/swdi/stormevents/csvfiles/StormEvents_details-ftp_v1.0_d1950_c20210803.csv.gz");
    }
}