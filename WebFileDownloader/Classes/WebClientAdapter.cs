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
            int splitLocation = completeUrl.LastIndexOf('/');
            string fileName = completeUrl.Substring(splitLocation + 1);
            string fileLocation = completeUrl.Substring(0, splitLocation);

            if(!Uri.TryCreate(fileLocation, UriKind.Absolute, out var address))
            {
                InvalidUrlRequested.Invoke(this, EventArgs.Empty);
                return;
            }

            DownloadProgressChanged.Invoke(this, EventArgs.Empty);
            webClient.DownloadFileAsync(address, fileName);
            DownloadCompleted.Invoke(this, EventArgs.Empty);
        }
    }
}