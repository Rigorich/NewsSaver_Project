using System;
using GeneralClasses;
using System.Threading.Tasks;
using NewsManager.Instructions;

namespace NewsManager
{
    public class Manager
    {
        public Manager()
        {
            Pullenti.Sdk.InitializeAll();
        }
        public async Task<NewsArticle[]> GetNewsAsync(SpecificInstruction instruction, int maxNewsCount = 1, int crawlDelayMilliSeconds = 1000)
        {
            HtmlDownloader downloader = new HtmlDownloader(instruction, maxNewsCount + 1, crawlDelayMilliSeconds);
            NewsMaker maker = new NewsMaker(instruction);
            await downloader.DownloadPagesAsync();
            await maker.ProcessPagesAsync(downloader.Pages);
            return maker.News;
        }
    }
}
