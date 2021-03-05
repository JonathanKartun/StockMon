using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microcharts;
using StockMon.Models.SQLite;
using StockMon.Services;

namespace StockMon.Models.Cells
{
    public class ChartRow
    {
        public string CurrencySymbol { get; set; }
        public string StockName { get; set; }
        public string StockCode { get; set; }
        public string VisualStockCode { get; set; }
        public double CurrentValue { get; set; }
        public double MinValue { get; set; }
        public double HighValue { get; set; }
        public float CurrentRate { get; set; }
        public double FirstRangeValue { get; set; }
        public List<double?> ChartData { get; set; }

        public LineChart TheChart { get; set; }

        //Helper function to convert to the ListView Chart Row format
        public static async Task<List<ChartRow>> ConvertStockEntriesToChartData(List<StockListEntries> entries)
        {
            List<ChartRow> tempData = new List<ChartRow>();

            foreach (var entry in entries)
            {
                tempData.Add(await ChartDataService.QueryStockInformation(entry.StockLongName, entry.StockCode));
            }
            return tempData;
        }
    }
}
