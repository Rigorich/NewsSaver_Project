using System;

namespace GeneralClasses
{
    public class InternetPage
    {
        public string URL { get; protected set; }
        public string HTML { get; protected set; }

        public InternetPage(string url, string html)
        {
            URL = url;
            HTML = html;
        }
    }
}
