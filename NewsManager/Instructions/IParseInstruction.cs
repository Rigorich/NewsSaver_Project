using System;
using GeneralClasses;

namespace NewsManager.Instructions
{
    public interface IParseInstruction
    {
        public NewsArticle Parse(InternetPage page);
    }
}
