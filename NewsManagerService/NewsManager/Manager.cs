using System;
using BaseClasses;
using System.Threading.Tasks;
using NewsManager.Instructions;
using NewsManager.Instructions.Available;
using System.Linq;
using System.Collections.Generic;

namespace NewsManager
{
    public class Manager
    {
        private static readonly List<SpecificInstruction> AllInstructions =
            new List<SpecificInstruction>
        {
            new Partisan(),
            new Telegraf()
        };

        public Manager()
        {
            Pullenti.Sdk.InitializeAll();
        }

        public void AddInstruction(SpecificInstruction instruction)
        {
            AllInstructions.Add(instruction);
        }

        public string[] GetAvailableSites()
        {
            return AllInstructions.Select(i => i.MainPageUrl).ToArray();
        }

        private SpecificInstruction GetInstruction(string url)
        {
            return 
                AllInstructions.FirstOrDefault(i => i.MainPageUrl == url)
                ?? AllInstructions.FirstOrDefault(i => i.MainPageUrl.Contains(url));
        }

        public async Task<NewsArticle[]> GetNewsAsync(
            string url,
            int maxNewsCount = 1,
            int crawlMinimumDelayMilliSeconds = 1000)
        {
            SpecificInstruction instruction = GetInstruction(url);
            if (instruction is null)
            {
                return null;
            }
            HtmlDownloader downloader = new HtmlDownloader(instruction, maxNewsCount + 1, crawlMinimumDelayMilliSeconds);
            NewsBuilder maker = new NewsBuilder(instruction);
            await downloader.DownloadPagesAsync().ConfigureAwait(false);
            NewsArticle[] news = await maker.BuildAsync(downloader.Pages).ConfigureAwait(false);
            //NewsArticle[] news = maker.Build(downloader.Pages);
            return news;
        }
    }
}
