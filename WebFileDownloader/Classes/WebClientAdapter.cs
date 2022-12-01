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
        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged = delegate{};
        public event EventHandler<string> InvalidUrlRequested = delegate{};

        public IDisposable DownloadFile(string completeUrl)
        {

                int splitLocation = completeUrl.LastIndexOf('/');
                string fileName = completeUrl.Substring(splitLocation + 1);
                string fileLocation = completeUrl.Substring(0, splitLocation);

                if(!Uri.TryCreate(fileLocation, UriKind.Absolute, out var address))
                {
                    InvalidUrlRequested?.Invoke(this, fileLocation);
                    return null;
                }
                
                var client = new WebClient();
                
                client.DownloadFileCompleted += (s, e) =>
                    DownloadCompleted?.Invoke(this, EventArgs.Empty);

                client.DownloadProgressChanged += (s, e) => 
                {
                    DownloadProgressChanged?.Invoke(
                        this, new DownloadProgressChangedEventArgs(e.ProgressPercentage, e.BytesReceived));
                };

                client.DownloadFileAsync(address, fileName);

                return client;
        }

        private static void DownloadProgressCallback4(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                e.BytesReceived,
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