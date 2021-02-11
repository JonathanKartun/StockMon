using System;
using System.Collections.Generic;
using Microcharts;

namespace StockMon.Models.Cells
{
    public class ChartRow
    {
        public string CurrencySymbol { get; set; }
        public string StockName { get; set; }
        public string StockCode { get; set; }
        public double CurrentValue { get; set; }
        public double MinValue { get; set; }
        public double HighValue { get; set; }
        public float CurrentRate { get; set; }
        public double FirstRangeValue { get; set; }
        public List<double?> ChartData { get; set; }

        public LineChart TheChart { get; set; }
    }
}
