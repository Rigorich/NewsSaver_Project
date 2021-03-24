﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeneralClasses;
using Pullenti.Ner;

namespace Server
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
                News[i] = ParsePage(page);
            }
        }

        protected Referent[] AnalyzeText(string text)
        {
            Processor processor = ProcessorService.CreateProcessor();
            AnalysisResult result = processor.Process(new SourceOfAnalysis(text));
            return result.Entities.ToArray();
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