using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockMon.Models.JSON;

namespace StockMon.Services.Logic
{
    public class YahooAPIQuery
    {
        public static async Task<List<Result>> GetChartData(string stockCheck, string region, string language, string intervalCheck, string rangeCheck)
        {
            List<Result> chartCollection = new List<Result>();

            var url = ChartData.generateChartURL(stockCheck, region, language, intervalCheck, rangeCheck);

            Console.WriteLine($"Accessing URL: {url}");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();

                    var chartData = JsonConvert.DeserializeObject<ChartData>(json);
                    chartCollection = chartData.chart.result as List<Result>;
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return chartCollection;
        }

        public static async Task<List<Quote>> GetStockSearchQueryData(string queryCheck, string region, string language, int quotescount, int newscount, bool enableFuzzyQuery)
        {
            List<Quote> chartCollection = new List<Quote>();

            var url = StockSearch.generateStockSearchQueryURL(queryCheck, region, language, quotescount, newscount, enableFuzzyQuery);

            Console.WriteLine($"Accessing URL: {url}");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();

                    var stockSearchData = JsonConvert.DeserializeObject<StockSearch>(json);
                    chartCollection = stockSearchData.quotes as List<Quote>; //chartData.chart.result as List<Result>;
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                Console.WriteLine(ex.Message);
            }

            return chartCollection;
        }
    }
}
