using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMon.Engines;
using StockMon.Models.Cells;
using StockMon.Models.SQLite;
using Xamarin.Forms;

namespace StockMon
{
    public partial class MainPage : ContentPage
    {
        List<ChartRow> listData = new List<ChartRow>();

        public MainPage()
        {
            InitializeComponent();
            BasicSetup();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetSQLiteStockEntries();
        }

        void BasicSetup()
        {
            Title = "Stock Mon";
            StockMarketListView.Refreshing += StockMarketListView_Refreshing;
        }

        private async void StockMarketListView_Refreshing(object sender, EventArgs e)
        {
            await GetSQLiteStockEntries();
            StockMarketListView.EndRefresh();
        }

        private void SettingsToolButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Settings", "Will be implementing custom settings in V2\n\n\nFeedback Welcomed\n\nJonathan Kartun", "Cool!");
        }

        private void AddStockToolButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddStockPage());
        }

        private async Task GetSQLiteStockEntries()
        {
            List<StockListEntries> stockItems = SQLiteDatabaseEngine<StockListEntries>.ReadRecordToList();
            if (stockItems.Count == 0)
            {
                return;
            }
            listData = await ChartRow.ConvertStockEntriesToChartData(stockItems);
            StockMarketListView.ItemsSource = null;
            StockMarketListView.ItemsSource = listData;
        }

        #region ViewCell Swipe Menu Actions

        void MenuItem_OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is MenuItem item) {
                if (item.CommandParameter is ChartRow chartRow)
                {
                    DeleteChartRow(chartRow);
                }
            }
        }

        async void DeleteChartRow(ChartRow chartRow)
        {
            var stockName = chartRow.StockName;
            var stockCode = chartRow.StockCode;
            var deleted = await SQLiteDatabaseEngine<StockListEntries>.DeleteRecord(row => row.StockCode == stockCode);
            if (!deleted.IsValid)
            {
                var error = deleted.GetErrorResponse();
                await DisplayAlert("Error Deleting", "Error in deleting row", "OK");
                return;
            }
            //Remove from visual current list
            listData.Remove(chartRow);
            StockMarketListView.ItemsSource = null; //Triggers refresh
            StockMarketListView.ItemsSource = listData;
        }

        #endregion
    }
}
