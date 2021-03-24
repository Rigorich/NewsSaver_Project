using System;
using GeneralClasses;
using System.Text.RegularExpressions;

namespace Server
{
    public class SpecificInstruction_1 : DownloaderInstruction, IParseInstruction
    {
        public override string MainPageUrl { get; protected set; } = "https://belaruspartisan.by/";
        public override int CrawlDepth { get; protected set; } = 1;
        public override bool IsNeedToDownload(string url)
        {
            var regexOfNewsPage = new Regex(@"\S+belaruspartisan\.by/\w+/\w+/$");
            return regexOfNewsPage.IsMatch(url);
        }

        public NewsArticle Parse(InternetPage page)
        {
            string sampleText = "Система Pullenti разрабатывается с первого апреля 2011 года российским программистом Михаилом Жуковым, проживающим в Москве на Красной площади в доме номер один на втором этаже. Конкурентов у него много: Abbyy, Yandex, ООО \"Russian Context Optimizer\" (RCO) и другие фирмы. Он планирует продать SDK за 1.120.000.001,99 (миллиард сто двадцать миллионов один рубль 99 копеек) рублей, без НДС.";
            return new NewsArticle(page.URL, page.HTML, "SampleName", sampleText, DateTime.Now);
        }
    }
}
