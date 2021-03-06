using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StockMon.Helpers;

namespace StockMon.Models.JSON
{
    public class StockSearch
    {
        public IList<object> explains { get; set; }
        public int count { get; set; }
        public IList<Quote> quotes { get; set; }
        public IList<News> news { get; set; }
        public IList<object> nav { get; set; }
        public IList<object> lists { get; set; }
        public IList<object> researchReports { get; set; }
        public int totalTime { get; set; }
        public int timeTakenForQuotes { get; set; }
        public int timeTakenForNews { get; set; }
        public int timeTakenForAlgowatchlist { get; set; }
        public int timeTakenForPredefinedScreener { get; set; }
        public int timeTakenForCrunchbase { get; set; }
        public int timeTakenForNav { get; set; }
        public int timeTakenForResearchReports { get; set; }

        public static string generateStockSearchQueryURL(string queryCheck, string region, string language, int quotescount, int newscount, bool enableFuzzyQuery)
        {
            //query, region, lang, quotescount, newscount, enableFuzzyQuery
            //ref: https://query1.finance.yahoo.com/v1/finance/search?q=gme&lang=en-US&region=US&quotesCount=6&newsCount=4&enableFuzzyQuery=false&quotesQueryId=tss_match_phrase_query&multiQuoteQueryId=multi_quote_single_token_query&newsQueryId=news_cie_vespa&enableCb=true&enableNavLinks=true&enableEnhancedTrivialQuery=true
            return string.Format(Constants.YAHOO_STOCK_SEARCH_QUERY, queryCheck, region, language, quotescount, newscount, enableFuzzyQuery ? "true":"false");
        }
    }

    //[JsonObject(Title = "Quote")]
    public class Quote
    {
        public string exchange { get; set; }
        public string shortname { get; set; }
        public string quoteType { get; set; }
        public string symbol { get; set; }
        public string index { get; set; }
        public long score { get; set; }
        public string typeDisp { get; set; }
        public string longname { get; set; }
        public bool isYahooFinance { get; set; }
        public string name { get; set; }
        public string permalink { get; set; }

        public static void FixNamesForResults(List<Quote> query)
        {
            for (int i = query.Count - 1; i >= 0; i--)
            {
                var q = query[i];
                var name = q.name;
                if (name == null)
                {
                    name = q.shortname;
                    if (name == null)
                    {
                        name = q.longname;
                    }
                    if (name == null)
                    {
                        name = q.symbol; //If it's finally blank then use the symbol
                    }
                }
                q.name = name;

                if (q.symbol == null) // || !q.isYahooFinance)
                {
                    query.Remove(q);
                    continue;
                }
            }
        }
    }

    public class News
    {
        public string uuid { get; set; }
        public string title { get; set; }
        public string publisher { get; set; }
        public string link { get; set; }
        public int providerPublishTime { get; set; }
        public string type { get; set; }
    }
}

