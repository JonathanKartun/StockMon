using System;
using System.Collections.Generic;
using System.Net;
using SQLite;
using StockMon.Engines;
using StockMon.Helpers;
using StockMon.Models.JSON;
using StockMon.Models.SQLite;
using StockMon.Services.Logic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StockMon
{
    public partial class AddStockPage : ContentPage
    {
        List<Models.JSON.Quote> stockQueryResultList = null;

        public AddStockPage()
        {
            InitializeComponent();
        }

        void Search_Clicked(System.Object sender, System.EventArgs e)
        {
            DismissKeyboard();
            BeginQuery();
        }

        async void BeginQuery()
        {
            string searchFor = StockSearchInput.Text;
            string region = "US";
            string language = "en-US";

            if (searchFor == null || searchFor.Length == 0)
            {
                await DisplayAlert("Query Error", "Cannot query for nothing", "OK");
                return;
            }

            searchFor = WebUtility.UrlEncode(searchFor.Trim());  //Encode for Posting

            stockQueryResultList = await YahooAPIQuery.GetStockSearchQueryData(searchFor, region, language, 12, 3, false);

            Quote.FixNamesForResults(stockQueryResultList);
            StockQueryResultsListView.ItemsSource = stockQueryResultList;

            if (stockQueryResultList.Count == 0)
            {
                await DisplayAlert("No Results", $"The query \"{StockSearchInput.Text}\" yielded no results.", "OK");
                return;
            }
        }

        async void StockQueryResultsListView_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            Quote qItem = (Quote)e.Item;
            var StockName = qItem.name;
            var StockCode = qItem.symbol;
            var ShouldAdd = await DisplayAlert("Add Stock?", $"Do you want to add the selected stock?\r\n\r\n{StockName}\r\n\r\nCode:  {StockCode}", "Yes", "No");

            if (ShouldAdd)
            {
                //Query if entry exists before adding again...
                var queryResult = SQLiteDatabaseEngine<StockListEntries>.ReadRecordToList(record => record.StockCode == StockCode);
                
                if (queryResult.Count > 0)
                {
                    await DisplayAlert("Already added", $"You already have '{StockCode}' in your list.", "OK" );
                    return;
                }
                AddToDatabase(StockName, StockCode);
            }
        }

        void AddToDatabase(String StockName, String StockCode)
        {
            StockListEntries entry = new StockListEntries
            {
                StockLongName = StockName,
                StockCode = StockCode
            };

            int rows = AddPostEntry(entry);
            if (rows > 0)
                DisplayAlert("Success!", "Successfully added a Stock Entry", "OK");
            else
                DisplayAlert("Failure!", "Failed to add a Stock Entry", "OK");
        }

        private int AddPostEntry(StockListEntries stockItem)
        {
            int rows;
            using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
            {
                conn.CreateTable<StockListEntries>();
                rows = conn.Insert(stockItem);
            }
            return rows;
        }

        void DismissKeyboard()
        {
            StockSearchInput.Unfocus();
        }

        void StockSearchInput_Completed(System.Object sender, System.EventArgs e)
        {
            DismissKeyboard();
            BeginQuery();
        }

    }
}
