using System;
using GeneralClasses;
using System.Text.RegularExpressions;

namespace NewsManager.Instructions
{
    public class SpecificInstruction_1 : SpecificInstruction
    {
        public override string MainPageUrl { get; protected set; } = "https://belaruspartisan.by/";
        public override int CrawlDepth { get; protected set; } = 1;
        public override bool IsNeedToDownload(string url)
        {
            var regexOfNewsPage = new Regex(@"\S+belaruspartisan\.by/\w+/\w+/$");
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

            string articleWithoutBr = article.Replace("<br />", "\n").Replace("<br>", "\n");
            string articleWithoutMarkup = Regex.Replace(articleWithoutBr, @"<[^>]*>", "");

            string text = articleWithoutMarkup
                .ReplaceHtmlSymbols().DeleteBlanks()
                .Replace(name, "").Replace(strDate, "");

            return new NewsArticle(page.URL, page.HTML, name, text, date);
        }

        protected string GetArticleName(string str)
        {
            var prefixOfName = @"<h1 itemprop=""name"" class=""name"">";
            var suffixOfName = @"</h1>";
            var regexOfName = new Regex(prefixOfName + ".*" + suffixOfName, RegexOptions.Singleline);

            string rawName = regexOfName.Match(str).Value;
            string articleName = rawName
                .Replace(prefixOfName, "").Replace(suffixOfName, "")
                .ReplaceHtmlSymbols().DeleteBlanks();

            return articleName;
        }
        protected (string, DateTime) GetArticleDate(string str)
        {
            var prefixOfDate = @"<span itemprop=""datePublished""" + @".*" + @"class=""news-date-time news_date"">";
            var suffixOfDate = @"</span>";
            var regexOfDateLine = new Regex(prefixOfDate + @"[0-9\:\/\s]+" + suffixOfDate, RegexOptions.Singleline);

            string rawDateLine = regexOfDateLine.Match(str).Value;

            var regexOfDate = new Regex(@">[0-9\:\/\s]+<", RegexOptions.Singleline);

            string strDate = regexOfDate.Match(rawDateLine).Value
                .Replace(">", "").Replace("<", "").Trim();

            DateTime articleDate = DateTime.Parse(strDate);

            return (strDate, articleDate);
        }
    }

    internal static class StringExtension
    {
        public static string ReplaceHtmlSymbols(this string str)
        {
            return str
                .Replace("&amp;", "&")
                .Replace("&quot;", "\"");
        }
        public static string DeleteBlanks(this string str)
        {
            string[] text = str.Split("\r\n".ToCharArray());
            var paragraphs = new System.Collections.Generic.List<string>();
            foreach (string parag in text)
            {
                string trimmed = parag.Trim().Trim(Environment.NewLine.ToCharArray());
                if (trimmed != string.Empty)
                {
                    paragraphs.Add(trimmed);
                }
            }
            return string.Join("\n", paragraphs);
        }
    }
}
