using System;

namespace Server
{
    public abstract class DownloaderInstruction
    {
        public abstract string MainPageUrl { get; protected set; }
        public abstract int CrawlDepth { get; protected set; }

        public abstract bool IsNeedToDownload(string url);
    }
}
