using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace WebFileDownloader.Classes
{
    public class WebClientAdapter
    {
        public event EventHandler DownloadCompleted = delegate{};
        public event EventHandler DownloadProgressChanged = delegate{};
        public event EventHandler InvalidUrlRequested = delegate{};

        public void DownloadFile(string completeUrl)
        {
            WebClient webClient = new WebClient();
            DownloadProgressChangedEventArgs eventArgs = new();
        
            // webClient.DownloadProgressChanged += (s, args) =>
            // {   
            //     DownloadProgressCallBack(s, eventArgs);             
            //     this.DownloadProgressChanged.Invoke(s, args);
            // };
            webClient.DownloadProgressChanged += (s, args) =>
            {
                DownloadProgressCallback(s, args)
            };

            webClient.DownloadFileCompleted += (s, args) => 
            {
                Console.WriteLine("Download Completed in Class");
                DownloadCompleted.Invoke (this, EventArgs.Empty);
            };

            int splitLocation = completeUrl.LastIndexOf('/');
            string fileName = completeUrl.Substring(splitLocation + 1);
            string fileLocation = completeUrl.Substring(0, splitLocation);

            if(!Uri.TryCreate(fileLocation, UriKind.Absolute, out var address))
            {
                InvalidUrlRequested.Invoke(this, EventArgs.Empty);
                return;
            }

            webClient.DownloadFileAsync(address, fileName);
        }

        // public static void DownLoadFileInBackground4(string address)
        // {
        //     WebClient client = new WebClient();
        //     Uri uri = new Uri(address);

        //     // Specify a DownloadFileCompleted handler here...

        //     // Specify a progress notification handler.
        //     client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);

        //     client.DownloadFileAsync(uri, "serverdata.txt");
        // }

        private static void DownloadProgressCallback4(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
        }
        private static void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine($"{0} bytes downloaded. {1}% complete.",
                e.BytesReceived,
                e.ProgressPercentage);
            
        }

    }
}