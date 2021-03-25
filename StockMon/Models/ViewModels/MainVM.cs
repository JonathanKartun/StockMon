using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MVVM;
using StockMon.Engines;
using StockMon.Models.Cells;
using StockMon.Models.SQLite;
using StockMon.Models.ViewModels.Commands;
using Xamarin.Forms;

namespace StockMon.Models.ViewModels
{
    public class MainVM: BaseViewModel
    {
        public ObservableCollection<ChartRow> ChartListData { get; set; }
        AddStockNavigationCommand addStockNavCommand { get; set; }

        public MainVM()
        {
            ChartListData = new ObservableCollection<ChartRow>();
            addStockNavCommand = new AddStockNavigationCommand(this);
            ChartListData.CollectionChanged += (s,e)=>
            { System.Diagnostics.Debug.WriteLine("Only Triggers if Removing/Adding"); };
        }

        public async Task UpdateStockEntries()
        {
            List<StockListEntries> stockItems = SQLiteDatabaseEngine<StockListEntries>.ReadRecordToList();
            if (stockItems.Count == 0)
            {
                return;
            }

            var chartData = await ChartRow.ConvertStockEntriesToChartData(stockItems);
            UpdateToReplaceListData(chartData);
        }

        public async void DeleteChartRow(ChartRow chartRow)
        {
            var stockName = chartRow.StockName;
            var stockCode = chartRow.StockCode;
            var deleted = await SQLiteDatabaseEngine<StockListEntries>.DeleteRecord(row => row.StockCode == stockCode);
            if (!deleted.IsValid)
            {
                var error = deleted.GetErrorResponse();
                await App.Current.MainPage.DisplayAlert("Error Deleting", "Error in deleting row", "OK");
                return;
            }
            ChartListData.Remove(chartRow);
        }

        private void UpdateToReplaceListData(List<ChartRow> chartData)
        {
            if (ChartListData == null) return;
            for (int i = 0; i < chartData.Count; i++)
            {
                var chartMatch = ChartListData.DefaultIfEmpty(null).FirstOrDefault(p => p!=null && p.StockCode == chartData[i].StockCode);

                if (chartMatch != null)
                {
                    // Replace only if necessary - Trigger doesn't work if just replacing items directly on ObservableCollection
                    if (!chartData[i].Equals(chartMatch))
                    {
                        ChartListData.Remove(chartMatch);
                        ChartListData.Insert(i, chartData[i]);
                    }
                } else
                {
                    ChartListData.Insert(i, chartData[i]);
                }
            }
        }

        public void NavigateToAddStockPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new AddStockPage());
        }

        public override void ApplyCommand(ToolbarItem button)
        {
            button.Command = addStockNavCommand;
        }
    }
}
