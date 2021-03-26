using System;
using StockMon.Models.Cells;
using StockMon.Models.ViewModels;
using Xamarin.Forms;

namespace StockMon
{
    public partial class MainPage : ContentPage
    {
        MainVM viewModel;
        public MainPage()
        {
            InitializeComponent();
            BasicSetup();
            viewModel = new MainVM();
            viewModel.ApplyCommand(AddStockToolButton);
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.UpdateStockEntries();
        }

        void BasicSetup()
        {
            Title = "Stock Mon";
        }

        private async void StockMarketListView_Refreshing(object sender, EventArgs e)
        {
            await viewModel.UpdateStockEntries();
            StockMarketListView.EndRefresh();
        }

        private void SettingsToolButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Settings", "Will be implementing custom settings in V2\n\n\nFeedback Welcomed\n\nJonathan Kartun", "Cool!");
        }

        #region ViewCell Swipe Menu Actions

        void MenuItem_OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is MenuItem item) {
                if (item.CommandParameter is ChartRow chartRow)
                {
                    viewModel.DeleteChartRow(chartRow);
                }
            }
        }

        #endregion
    }
}
