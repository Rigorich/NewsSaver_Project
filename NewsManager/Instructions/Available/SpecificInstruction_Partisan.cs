using System;
using GeneralClasses;
using System.Text.RegularExpressions;

namespace NewsManager.Instructions.Available
{
    public class SpecificInstruction_Partisan : SpecificInstruction
    {
        public override string MainPageUrl { get; protected set; } = @"https://belaruspartisan.by/";
        public override int CrawlDepth { get; protected set; } = 1;
        public override bool IsNeedToDownload(string url)
        {
            Regex regexOfNewsPage = new Regex(@"\S+belaruspartisan\.by/\w+/\d+/$");
            return regexOfNewsPage.IsMatch(url);
        }

        public override NewsArticle Parse(InternetPage page)
        {
            string divArticle = @"<div itemscope itemtype=""http://schema.org/Article"" class=""pw article"">";
            string divDate = @"<div class=""date_block"">";
            string html = page.HTML;

            int beginArticle = html.IndexOf(divArticle);
            int beginDate = html.IndexOf(divDate);
            int endDate = html.IndexOf("</div>", beginDate);

            string article = page.HTML.Substring(beginArticle, endDate - beginArticle - 1);

            string name = GetArticleName(article);
            (string strDate, DateTime date) = GetArticleDate(article);

            string articleWithoutMarkup = article
                .Replace("<br />", "\n").Replace("<br>", "\n")
                .RegexReplace(@"<[^>]*>", "");

            string text = articleWithoutMarkup
                .ReplaceHtmlSymbols().DeleteBlanks()
                .Replace(name, "").Replace(strDate, "");

            return new NewsArticle(page.URL, page.HTML, name, text, date);
        }

        protected override string GetArticleName(string str)
        {
            string prefixOfName = @"<h1 itemprop=""name"" class=""name"">";
            string suffixOfName = @"</h1>";
            Regex regexOfName = new Regex(prefixOfName + ".*?" + suffixOfName, RegexOptions.Singleline);

            string rawName = regexOfName.Match(str).Value;
            string articleName = rawName
                .Replace(prefixOfName, "").Replace(suffixOfName, "")
                .ReplaceHtmlSymbols().DeleteBlanks();

            return articleName;
        }
        protected override (string, DateTime) GetArticleDate(string str)
        {
            string prefixOfDate = @"<span itemprop=""datePublished""" + @".*?" + @"class=""news-date-time news_date"">";
            string suffixOfDate = @"</span>";
            Regex regexOfDateLine = new Regex(prefixOfDate + @"[0-9\:\/\s]+" + suffixOfDate, RegexOptions.Singleline);

            string rawDateLine = regexOfDateLine.Match(str).Value;

            Regex regexOfDate = new Regex(@">[0-9\:\/\s]+<", RegexOptions.Singleline);

            string strDate = regexOfDate.Match(rawDateLine).Value
                .Replace(">", "").Replace("<", "").Trim();

            DateTime articleDate = DateTime.Parse(strDate);

            return (strDate, articleDate);
        }
    }

}
