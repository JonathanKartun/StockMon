using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using StockMon.Helpers.Converters;
using StockMon.Helpers.Extensions;
using StockMon.Models.Cells;
using StockMon.Models.JSON;
using StockMon.Services.Logic;
using static StockMon.Helpers.Constants;

namespace StockMon.Services
{
    class ChartDataService
    {
        public static async Task<ChartRow> QueryStockInformation(string StockName, string stockCode)
        {
            ChartRow RowData = new ChartRow();

            string searchFor = WebUtility.UrlEncode(stockCode.Trim());
            string region = "US";
            string language = "en-US";
            string interval = Intervals.Days1.GetDescriptionString();
            string range = Intervals.Months3.GetDescriptionString();

            List<Result> chartData = await YahooAPIQuery.GetChartData(searchFor, region, language, interval, range);
            int takeIndex = 0;
            if (chartData == null || chartData.Count == 0 )
            {
                return CreateErrorObject(StockName, stockCode);
            }

            Result result = chartData[takeIndex];

            double prevClose = result.meta.chartPreviousClose;
            double regMarketPrice = result.meta.regularMarketPrice;

            RowData.CurrencySymbol = CurrencyConverter.CurrencyCodeToSymbol(result.meta.currency);
            RowData.StockName = StockName;
            RowData.StockCode = stockCode;
            RowData.VisualStockCode = stockCode;
            RowData.CurrentValue = regMarketPrice;
            RowData.FirstRangeValue = prevClose;
            RowData.CurrentRate = (float)(prevClose / regMarketPrice * 100.0f);
            RowData.ChartData = result.indicators.adjclose[takeIndex].adjclose as List<double?>;

            if (RowData.ChartData != null)
            {
                RowData.MinValue = (double)RowData.ChartData.Min();
                RowData.HighValue = (double)RowData.ChartData.Max();

                var chartEntries = RowData.ChartData.Where(chart => chart != null).Select(chart => new Microcharts.ChartEntry((float)chart - (float)prevClose)).ToList();
                RowData.TheChart = new LineChart { Entries = chartEntries, LineMode = LineMode.Straight, PointMode = PointMode.Square, BackgroundColor = SKColors.Transparent, LineAreaAlpha = 0, AnimationDuration = new TimeSpan(0), IsAnimated = false };
            }

            return await Task.FromResult(RowData);
        }

        static ChartRow CreateErrorObject(string StockName, string stockCode)
        {
            ChartRow RowData = new ChartRow();
            List<Result> chartData = new List<Result>();
            Result badResult = new Result();
            badResult.meta = new Meta();
            badResult.meta.chartPreviousClose = -1;
            badResult.meta.regularMarketPrice = -1;
            RowData.CurrencySymbol = "?";
            RowData.StockName = "? " + StockName + "\r\n> FAILED LOADING";
            RowData.StockCode = stockCode;
            RowData.VisualStockCode = stockCode + " - FAILED TO LOAD!";
            RowData.CurrentValue = -1;
            RowData.FirstRangeValue = -1;
            RowData.CurrentRate= -1.0f;
            RowData.MinValue = -1.0f;
            RowData.HighValue = -1.0f;

            var cd = new[] { new Microcharts.ChartEntry(0.0f) };
            RowData.TheChart = new LineChart { Entries = cd };
            return RowData;
        }
    }
}
