using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GeneralClasses
{
    public class NewsArticle : InternetPage
    {
        public string Name { get; protected set; }
        public string Text { get; protected set; }
        public DateTime Date { get; protected set; }

        protected List<Pullenti.Ner.Referent> entities = null;
        [IgnoreDataMember]
        public List<Pullenti.Ner.Referent> Entities
        {
            get => entities;
            set
            {
                entities = value;
                xmlEntities = Pullenti.Ner.Core.SerializeHelper
                    .SerializeReferentsToXmlString(entities);
            }
        }

        protected string xmlEntities = null;
        public string XmlEntities
        {
            get => xmlEntities;
            set
            {
                xmlEntities = value;
                entities = Pullenti.Ner.Core.SerializeHelper
                    .DeserializeReferentsFromXmlString(xmlEntities);
            }
        }

        public NewsArticle(string url, string html, string name, string text, DateTime date) : base(url, html)
        {
            Name = name;
            Text = text;
            Date = date;
        }
    }
}
