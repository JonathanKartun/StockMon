using System;
using StockMon.Models.JSON;
using StockMon.Models.ViewModels.MVVMHelpers;
using Xamarin.Forms;

namespace StockMon
{
    public partial class AddStockPage : ContentPage
    {
        AddStockVM ViewModel;
        public AddStockPage()
        {
            InitializeComponent();
            ViewModel = new AddStockVM();
            ViewModel.ApplyCommand(SearchButton);
            ViewModel.StockSearchInput = StockSearchInput;
            BindingContext = ViewModel;
        }

        void StockQueryResultsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Quote Item = (Quote)e.Item;
            ViewModel.AddSelectedItemFromList(Item);
        }

        void StockSearchInput_Completed(object sender, EventArgs e)
        {
            ViewModel.QueryStockCodeName();
        }
    }
}
