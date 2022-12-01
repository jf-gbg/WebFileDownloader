using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFileDownloader
{
    public class DownloadProgressChangedEventArgs : System.EventArgs
    {
        public int ProgressPercentage { get; init; } = 0;
        public long BytesReceived { get; init; } = 0;

        public DownloadProgressChangedEventArgs(int progressPercentage, long bytesReceived)
        {
            ProgressPercentage = progressPercentage;
            BytesReceived = bytesReceived;
        }
    }
}