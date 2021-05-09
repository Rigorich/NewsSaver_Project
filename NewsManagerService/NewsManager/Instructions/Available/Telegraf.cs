using System;
using BaseClasses;
using System.Text.RegularExpressions;
using Abot2.Core;
using Abot2.Poco;
using System.Collections.Generic;
using System.Linq;

namespace NewsManager.Instructions.Available
{
    class Telegraf : SpecificInstruction
    {
        public override string MainPageUrl { get; protected set; } = @"https://telegraf.by/obshhestvo/";
        public override int CrawlDepth { get; protected set; } = 1;
        public override bool IsNeedToDownload(string url)
        {
            Regex regexOfNewsPage = new Regex(@"\S+telegraf\.by/obshhestvo/[\w-_]+/$");
            bool match = regexOfNewsPage.IsMatch(url.Replace("\"", ""));
            bool feed = url == @"https://telegraf.by/obshhestvo/feed/";
            return match && !feed;
        }

        public override IHtmlParser GetLinkExtractor() => new Extractor();
        protected class Extractor : IHtmlParser
        {
            public IEnumerable<HyperLink> GetLinks(CrawledPage crawledPage)
            {
                string html = crawledPage.Content.Text;
                string pattern = @"(href=)(http.*?)([>\s])";
                string fixedHtml = Regex.Replace(html, pattern,
                    m => $"{m.Groups[1].Value}\"{m.Groups[2].Value}\"{m.Groups[3].Value}");
                crawledPage.Content.Text = fixedHtml;
                var links = new AngleSharpHyperlinkParser().GetLinks(crawledPage);
                return links;
            }
        }

        protected override string GetArticleName(string html)
        {
            string prefixOfName = @"<h1 class=""zagolovok border-bottom pb-4 mt-2"">";
            string suffixOfName = @"</h1>";
            Regex regexOfName = new Regex(prefixOfName + ".*?" + suffixOfName, RegexOptions.Singleline);

            string rawName = regexOfName.Match(html).Value;
            string articleName = rawName
                .Replace(prefixOfName, "").Replace(suffixOfName, "")
                .ReplaceHtmlSymbols().DeleteBlanks();

            return articleName;
        }
        protected override (string, DateTime) GetArticleDate(string html)
        {
            string prefixOfDate = @"<div class=""btn btn-info"">";
            string suffixOfDate = @"</div>";
            Regex regexOfDate = new Regex(prefixOfDate + @".*?" + suffixOfDate, RegexOptions.Singleline);

            string strDateLine = regexOfDate.Match(html).Value;

            string strDate = strDateLine
                .Replace(prefixOfDate, "")
                .Replace(suffixOfDate, "")
                .Trim();

            DateTime articleDate = DateTime.Parse(strDate);

            return (strDate, articleDate);
        }
        protected override string GetArticleText(string html)
        {
            string prefixOfText = @"<div class=""w-100 text-posta"">";
            string suffixOfText = @"<div class=""at-below-post addthis_tool""";
            Regex regexOfText = new Regex(prefixOfText + ".*?" + suffixOfText, RegexOptions.Singleline);

            string rawText = regexOfText.Match(html).Value;
            string articleText = rawText
                .DeleteScripts()
                .Replace(prefixOfText, "").Replace(suffixOfText, "")
                .Replace("</p>", "\n").Replace("<p>", "\n")
                .RegexReplace(@"<[^>]*>", "")
                .ReplaceHtmlSymbols().DeleteBlanks();

            return articleText;
        }
    }
}
