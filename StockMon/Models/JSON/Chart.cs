using System;
using System.Collections.Generic;
using StockMon.Helpers;
using Xamarin.Forms;

namespace StockMon.Models.JSON
{
    public class ChartData
    {
        public Chart chart { get; set; }

        public static string generateChartURL(string stockCheck, string region, string language, string intervalCheck, string rangeCheck)
        {
            //ref: https://query1.finance.yahoo.com/v8/finance/chart/BTC-USD?region=US&lang=en-US&includePrePost=false&interval=1d&range=5d
            return string.Format(Constants.YAHOO_CHART_QUERY, stockCheck, region, language, "false", intervalCheck, rangeCheck);
        }
    }

    public class Pre
    {
        public string timezone { get; set; }
        public int end { get; set; }
        public int start { get; set; }
        public int gmtoffset { get; set; }
    }

    public class Regular
    {
        public string timezone { get; set; }
        public int end { get; set; }
        public int start { get; set; }
        public int gmtoffset { get; set; }
    }

    public class Post
    {
        public string timezone { get; set; }
        public int end { get; set; }
        public int start { get; set; }
        public int gmtoffset { get; set; }
    }

    public class CurrentTradingPeriod
    {
        public Pre pre { get; set; }
        public Regular regular { get; set; }
        public Post post { get; set; }
    }

    public class Meta
    {
        public string currency { get; set; }
        public string symbol { get; set; }
        public string exchangeName { get; set; }
        public string instrumentType { get; set; }
        public long? firstTradeDate { get; set; }
        public long regularMarketTime { get; set; }
        public int gmtoffset { get; set; }
        public string timezone { get; set; }
        public string exchangeTimezoneName { get; set; }
        public double regularMarketPrice { get; set; }
        public double chartPreviousClose { get; set; }
        public int priceHint { get; set; }
        public CurrentTradingPeriod currentTradingPeriod { get; set; }
        public string dataGranularity { get; set; }
        public string range { get; set; }
        public IList<string> validRanges { get; set; }
    }

    public class StocksQuote
    {
        public IList<double?> high { get; set; }
        public IList<double?> open { get; set; }
        public IList<object> volume { get; set; }
        public IList<double?> close { get; set; }
        public IList<double?> low { get; set; }
    }

    public class Adjclose
    {
        public IList<double?> adjclose { get; set; }
    }

    public class Indicators
    {
        public IList<StocksQuote> quote { get; set; }
        public IList<Adjclose> adjclose { get; set; }
    }

    public class Result
    {
        public Meta meta { get; set; }
        public IList<long> timestamp { get; set; }
        public Indicators indicators { get; set; }
    }

    public class Chart
    {
        public IList<Result> result { get; set; }
        public object error { get; set; }
    }
}

