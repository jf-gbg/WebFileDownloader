using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFileDownloader
{
    public class DownloadProgressChangedEventArgs
    {
        public int ProgressPercentage { get; set; }
        public int BytesReceived { get; set; }

        
    }
}