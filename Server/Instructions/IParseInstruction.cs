using System;
using GeneralClasses;

namespace Server
{
    interface IParseInstruction
    {
        public NewsArticle Parse(InternetPage page);
    }
}
