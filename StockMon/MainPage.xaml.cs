using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockMon.Models.Cells;
using StockMon.Services;
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
            await BeginQuery();
        }

        void BasicSetup()
        {
            Title = "Stock Mon";
            StockMarketListView.Refreshing += StockMarketListView_Refreshing;
        }

        async Task BeginQuery()
        {
            List<ChartRow> tempData = new List<ChartRow>();
            tempData.Add(await ChartDataService.QueryStockInformation("Bitcoin USD", "BTC-USD"));
            tempData.Add(await ChartDataService.QueryStockInformation("Dogecoin USD", "DOGE-USD"));
            tempData.Add(await ChartDataService.QueryStockInformation("Stellar USD", "XLM-USD"));

            tempData.Add(await ChartDataService.QueryStockInformation("Tesla, Inc.", "TSLA"));
            tempData.Add(await ChartDataService.QueryStockInformation("AMS AG ", "AMS.SW"));
            tempData.Add(await ChartDataService.QueryStockInformation("GameStop Corp.", "GME"));
            tempData.Add(await ChartDataService.QueryStockInformation("AMC Entertainment Holdings, Inc.", "AMC"));
            tempData.Add(await ChartDataService.QueryStockInformation("Barrick Gold Corporation", "GOLD"));

            tempData.Add(await ChartDataService.QueryStockInformation("AtariToken USD", "ATRI-USD"));
            tempData.Add(await ChartDataService.QueryStockInformation("Virgin Galactic Holdings, Inc.", "SPCE"));
            tempData.Add(await ChartDataService.QueryStockInformation("Sundial Growers Inc.", "SNDL"));
            tempData.Add(await ChartDataService.QueryStockInformation("DraftKings Inc", "DKNG"));

            tempData.Add(await ChartDataService.QueryStockInformation("Wirecard AG - Frankfurt - Delayed Price.", "WDI.F"));
            tempData.Add(await ChartDataService.QueryStockInformation("Wirecard AG - Dusseldorf - Delayed Price.", "WDI.DU"));
            tempData.Add(await ChartDataService.QueryStockInformation("Wirecard AG - Berlin - Delayed Price.", "WDI.BE"));

            tempData.Add(await ChartDataService.QueryStockInformation("Aurora Cannabis Inc", "ACB"));

            listData = tempData;
            StockMarketListView.ItemsSource = null;
            StockMarketListView.ItemsSource = listData;
        }

        private async void StockMarketListView_Refreshing(object sender, EventArgs e)
        {
            await BeginQuery();
            StockMarketListView.EndRefresh();
        }

        private void AddStockToolbutton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddStockPage());
        }
    }
}
