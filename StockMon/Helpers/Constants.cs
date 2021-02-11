using System;
using System.ComponentModel;
using StockMon.Helpers.Extensions;

namespace StockMon.Helpers
{
    public class Constants
    {
        public const string YAHOO_CHART_QUERY = "https://query1.finance.yahoo.com/v8/finance/chart/{0}?region={1}&lang={2}&includePrePost={3}&interval={4}&range={5}";

        public const string YAHOO_STOCK_SEARCH_QUERY = "https://query1.finance.yahoo.com/v1/finance/search?q={0}&region={1}&lang={2}&quotesCount={3}&newsCount={4}&enableFuzzyQuery={5}&quotesQueryId=tss_match_phrase_query&multiQuoteQueryId=multi_quote_single_token_query&newsQueryId=news_cie_vespa&enableCb=true&enableNavLinks=true&enableEnhancedTrivialQuery=true";

        public enum Intervals
        {
            [Description("1m")] Minutes1,
            [Description("2m")] Minutes2,
            [Description("5m")] Minutes5,
            [Description("15m")] Minutes15,
            [Description("30m")] Minutes30,
            [Description("60m")] Minutes60,
            [Description("90m")] Minutes90,
            [Description("1h")] Hours1,
            [Description("1d")] Days1,
            [Description("5d")] Days5,
            [Description("1wk")] Weeks1,
            [Description("1mo")] Months1,
            [Description("3mo")] Months3
        }
    }
}
