using GeneralClasses;
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
    class HtmlDownloader
    {
        public DownloaderInstruction Instruction { get; protected set; }
        public InternetPage[] Pages { get; protected set; }

        protected List<InternetPage> tempPages { get; set; }

        public int PagesLimit { get; protected set; }
        public int MinCrawlDelayMilliSeconds { get; protected set; }

        public HtmlDownloader(DownloaderInstruction instruction, int pagesLimit = 2, int crawlDelayMilliSeconds = 1000)
        {
            Instruction = instruction;
            PagesLimit = pagesLimit;
            MinCrawlDelayMilliSeconds = crawlDelayMilliSeconds;
        }

        public async Task DownloadPagesAsync()
        {
            Pages = null;
            tempPages = new List<InternetPage>();

            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = PagesLimit,
                MaxCrawlDepth = Instruction.CrawlDepth,
                MinCrawlDelayPerDomainMilliSeconds = MinCrawlDelayMilliSeconds,
            };

            var crawler = new PoliteWebCrawler(config);

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

            var crawlResult = await crawler.CrawlAsync(new Uri(Instruction.MainPageUrl));

            Crawler_AllPagesCompleted();
        }
        protected void Crawler_OnePageCompleted(object sender, PageCrawlCompletedArgs e)
        {
            var url = e.CrawledPage.Uri.AbsoluteUri;
            var hrml = e.CrawledPage.Content.Text;
            if (url != Instruction.MainPageUrl)
            {
                tempPages.Add(new InternetPage(url, hrml));
            }
        }
        protected void Crawler_AllPagesCompleted()
        {
            Pages = tempPages.ToArray();
            tempPages = null;
        }
    }
}
