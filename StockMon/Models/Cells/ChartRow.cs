using System.Collections.Generic;
using System.Threading.Tasks;
using Microcharts;
using MVVM;
using StockMon.Models.SQLite;
using StockMon.Services;
using Xamarin.Forms;

namespace StockMon.Models.Cells
{
    public class ChartRow: BaseViewModel
    {
        private string currencySymbol;
        private string stockName;
        private string stockCode;
        private string visualStockCode;
        private double currentValue;
        private double minValue;
        private double highValue;
        private float currentRate;
        private double firstRangeValue;
        private List<double?> chartData;
        private LineChart theChart;
        private bool failedRetrival;

        public string CurrencySymbol
        {
            get => currencySymbol;
            set => SetProperty(ref currencySymbol, value, onChanged: () => { });
        }
        public string StockName
        {
            get => stockName;
            set => SetProperty(ref stockName, value, onChanged: () => { });
        }

        public string StockCode
        {
            get => stockCode;
            set => SetProperty(ref stockCode, value, onChanged: () => { });
        }

        public string VisualStockCode
        {
            get => visualStockCode;
            set => SetProperty(ref visualStockCode, value, onChanged: () => { });
        }

        
        public double CurrentValue
        {
            get => currentValue;
            set => SetProperty(ref currentValue, value, onChanged: () => { });
        }

        public double MinValue
        {
            get => minValue;
            set => SetProperty(ref minValue, value, onChanged: () => { });
        }

        public double HighValue
        {
            get => highValue;
            set => SetProperty(ref highValue, value, onChanged: () => { });
        }

        public float CurrentRate
        {
            get => currentRate;
            set => SetProperty(ref currentRate, value, onChanged: () => { });
        }

        public double FirstRangeValue
        {
            get => firstRangeValue;
            set => SetProperty(ref firstRangeValue, value, onChanged: () => { });
        }

        public List<double?> ChartData
        {
            get => chartData;
            set => SetProperty(ref chartData, value, onChanged: () => { });
        }

        public LineChart TheChart
        {
            get => theChart;
            set => SetProperty(ref theChart, value, onChanged: () => { });
        }

        public bool FailedRetrival
        {
            get => failedRetrival;
            set => SetProperty(ref failedRetrival, value, onChanged: () => { });
        }

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

        public override bool Equals(object obj)
        {
            ChartRow other = obj as ChartRow;

            if (obj == null)
            {
                return false;
            }
            if (!(obj is ChartRow))
            {
                return false;
            }
            return (this.CurrentValue == other.CurrentValue)
                && (this.StockCode == other.StockCode)
                && (this.MinValue == other.MinValue)
                && (this.HighValue == other.HighValue)
                && (this.CurrentRate == other.CurrentRate);
        }

        public override int GetHashCode()
        {
            return StockName.GetHashCode() ^ StockCode.GetHashCode();
        }
    }
}
