using BaseClasses;
using System;
using Abot2;
using Abot2.Core;
using Abot2.Crawler;
using Abot2.Poco;
using System.Threading.Tasks;
using System.Collections.Generic;
using NewsManager.Instructions;

namespace NewsManager
{
    public class HtmlDownloader
    {
        public DownloaderInstruction Instruction { get; protected set; }
        public InternetPage[] Pages { get; protected set; }

        protected List<InternetPage> TempPages { get; set; }

        public int PagesLimit { get; protected set; }
        public int MinCrawlDelayMilliSeconds { get; protected set; }

        public HtmlDownloader(
            DownloaderInstruction instruction,
            int pagesLimit,
            int crawlMinimumDelayMilliSeconds)
        {
            Instruction = instruction;
            PagesLimit = pagesLimit;
            MinCrawlDelayMilliSeconds = crawlMinimumDelayMilliSeconds;
        }

        public async Task DownloadPagesAsync()
        {
            Pages = null;
            TempPages = new List<InternetPage>();

            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = PagesLimit,
                MaxCrawlDepth = Instruction.CrawlDepth,
                MinCrawlDelayPerDomainMilliSeconds = MinCrawlDelayMilliSeconds,
            };

            var crawler = new PoliteWebCrawler(
                config,
                null,
                null,
                null,
                null,
                Instruction.GetLinkExtractor(),
                null,
                null,
                null
                );

            crawler.ShouldCrawlPageDecisionMaker = (pageToCrawl, crawlContext) =>
            {
                var url = pageToCrawl.Uri.AbsoluteUri;
                if (url != Instruction.MainPageUrl && !Instruction.IsNeedToDownload(url))
                {
                    return new CrawlDecision { Allow = false, Reason = "Page not Instruction.IsNeedToDownload(URL)" };
                }
                return new CrawlDecision { Allow = true };
            };

            crawler.PageCrawlCompleted += Crawler_OnePageCompleted;

            var crawlResult = await crawler.CrawlAsync(new Uri(Instruction.MainPageUrl)).ConfigureAwait(false);

            Crawler_AllPagesCompleted();
        }
        protected void Crawler_OnePageCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var url = e.CrawledPage.Uri.AbsoluteUri;
            var html = e.CrawledPage.Content.Text;
            if (url != Instruction.MainPageUrl)
            {
                TempPages.Add(new InternetPage(url, html));
            }
        }
        protected void Crawler_AllPagesCompleted()
        {
            Pages = TempPages.ToArray();
            TempPages = null;
        }
    }
}
