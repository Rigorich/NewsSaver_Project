using System;
using BaseClasses;

namespace NewsManager.Instructions
{
    public interface IParseInstruction
    {
        public NewsArticle Parse(InternetPage page);
        public static string UndefinedString => "N/A";
        public static DateTime UndefinedDate => DateTime.UnixEpoch;
    }
}
