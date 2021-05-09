using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseClasses;
using NewsManager.Instructions;
using Pullenti.Ner;

namespace NewsManager
{
    class NewsBuilder
    {
        public NewsArticle[] News { get; protected set; }
        public IParseInstruction Instruction { get; protected set; }

        public NewsBuilder(IParseInstruction instruction)
        {
            Instruction = instruction;
        }

        public NewsArticle[] Build(InternetPage[] pages)
        {
            ParsePages(pages);
            AnalyzeTexts();
            return News;
        }
        public async Task<NewsArticle[]> BuildAsync(InternetPage[] pages)
        {
            return await Task.Run(() => Build(pages)).ConfigureAwait(false);
        }

        protected NewsArticle ParsePage(InternetPage page)
        {
            return Instruction.Parse(page);
        }
        protected void ParsePages(InternetPage[] pages)
        {
            News = new NewsArticle[pages.Length];
            for (int i = 0; i < pages.Length; i++)
            {
                InternetPage page = pages[i];
                try
                {
                    News[i] = ParsePage(page);
                }
                catch
                {
                    News[i] = new NewsArticle(
                        page.URL,
                        page.HTML,
                        IParseInstruction.UndefinedString,
                        IParseInstruction.UndefinedString,
                        IParseInstruction.UndefinedDate);
                }
            }
        }

        protected List<Referent> AnalyzeText(string text)
        {
            try
            {
                Processor processor = ProcessorService.CreateProcessor();
                AnalysisResult result = processor.Process(new SourceOfAnalysis(text));
                return result.Entities;
            }
            catch
            {
                return new List<Referent>();
            }
        }
        protected void AnalyzeTexts()
        {
            foreach (NewsArticle article in News)
            {
                article.Entities = AnalyzeText(article.Text).Select(e => e.ToString()).ToArray();
            }
        }
    }
}
