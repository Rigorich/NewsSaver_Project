using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeneralClasses;
using NewsManager.Instructions;
using Pullenti.Ner;

namespace NewsManager
{
    class NewsMaker
    {
        public NewsArticle[] News { get; protected set; }
        public IParseInstruction Instruction { get; protected set; }

        public NewsMaker(IParseInstruction instruction)
        {
            Instruction = instruction;
        }

        public void ProcessPages(InternetPage[] pages)
        {
            ParsePages(pages);
            AnalyzeTexts();
        }
        public async Task ProcessPagesAsync(InternetPage[] pages)
        {
            await Task.Run(() => ProcessPages(pages)).ConfigureAwait(false);
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
                article.Entities = AnalyzeText(article.Text);
            }
        }
    }
}
