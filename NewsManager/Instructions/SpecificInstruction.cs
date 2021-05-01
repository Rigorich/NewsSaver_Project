using GeneralClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NewsManager.Instructions
{
    public abstract class SpecificInstruction : DownloaderInstruction, IParseInstruction
    {
        public virtual NewsArticle Parse(InternetPage page)
        {
            string html = page.HTML;

            string text;
            try
            {
                text = GetArticleText(html);
            }
            catch
            {
                text = IParseInstruction.UndefinedString;
            }
            if (string.IsNullOrWhiteSpace(text))
            {
                text = IParseInstruction.UndefinedString;
            }

            string name;
            try
            {
                name = GetArticleName(html);
            }
            catch
            {
                name = IParseInstruction.UndefinedString;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                name = IParseInstruction.UndefinedString;
            }

            string strDate;
            DateTime date;
            try
            {
                (strDate, date) = GetArticleDate(html);
            }
            catch
            {
                strDate = IParseInstruction.UndefinedString;
                date = IParseInstruction.UndefinedDate;
            }
            if (string.IsNullOrWhiteSpace(strDate))
            {
                strDate = IParseInstruction.UndefinedString;
                date = IParseInstruction.UndefinedDate;
            }

            return new NewsArticle(page.URL, page.HTML, name, text, date);
        }
        protected virtual string GetArticleName(string html)
        {
            throw new NotImplementedException();
        }
        protected virtual (string, DateTime) GetArticleDate(string html)
        {
            throw new NotImplementedException();
        }
        protected virtual string GetArticleText(string html)
        {
            throw new NotImplementedException();
        }
    }

    internal static class StringExtension
    {
        public static string RegexReplace(this string str, string pattern, string replacement)
        {
            return Regex.Replace(str, pattern, replacement);
        }
        public static string DeleteScripts(this string str)
        {
            string pattern = @"<script.*?</script>";
            string cleared = Regex.Replace(str, pattern, "");
            return cleared;
        }
        public static string ReplaceHtmlSymbols(this string str)
        {
            return System.Web.HttpUtility.HtmlDecode(str);
        }
        public static string DeleteBlanks(this string str)
        {
            char[] endlines = "\r\n".ToCharArray();
            string[] text = str.Split(endlines);
            var paragraphs = new List<string>();
            foreach (string parag in text)
            {
                string trimmed = parag.Trim().Trim(endlines);
                if (trimmed != string.Empty)
                {
                    paragraphs.Add(trimmed);
                }
            }
            return string.Join("\n", paragraphs);
        }
    }
}