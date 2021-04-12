using GeneralClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsManager.Instructions
{
    public abstract class SpecificInstruction : DownloaderInstruction, IParseInstruction
    {
        public abstract NewsArticle Parse(InternetPage page);
    }
}
