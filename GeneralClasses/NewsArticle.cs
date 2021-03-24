using System;

namespace GeneralClasses
{
    public class NewsArticle : InternetPage
    {
        public string Name { get; protected set; }
        public string Text { get; protected set; }
        public DateTime Date { get; protected set; }
        public Pullenti.Ner.Referent[] Entities { get; set; } = null;

        public NewsArticle(string url, string html, string name, string text, DateTime date) : base(url, html)
        {
            Name = name;
            Text = text;
            Date = date;
        }
    }
}
