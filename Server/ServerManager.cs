using System;
using GeneralClasses;
using System.Threading.Tasks;

namespace Server
{
    class ServerManager
    {
        public async Task<NewsArticle[]> GetNewsAsync()
        {
            var instruction = new SpecificInstruction_1();
            HtmlDownloader downloader = new HtmlDownloader(instruction);
            NewsMaker maker = new NewsMaker(instruction);
            await downloader.DownloadPages();
            maker.ProcessPages(downloader.Pages);
            return maker.News;
        }
    }
}
