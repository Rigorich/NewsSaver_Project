using System;

namespace NewsManager.Instructions
{
    public abstract class DownloaderInstruction
    {
        public abstract string MainPageUrl { get; protected set; }
        public abstract int CrawlDepth { get; protected set; }

        public abstract bool IsNeedToDownload(string url);
        public virtual Abot2.Core.IHtmlParser GetLinkExtractor()
        {
            return null;
        }
    }
}
