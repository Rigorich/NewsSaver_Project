using System;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;

namespace BaseClasses
{
    public class NewsArticle : InternetPage
    {
        [IgnoreDataMember]
        [AutoIncrement]
        public int Id { get; set; } = -1;

        public string Name { get; protected set; }
        public string Text { get; protected set; }
        public DateTime Date { get; protected set; }
        public string[] Entities { get; set; }

        public NewsArticle(string url, string html, string name, string text, DateTime date) : base(url, html)
        {
            Name = name;
            Text = text;
            Date = date;
        }
    }
}
