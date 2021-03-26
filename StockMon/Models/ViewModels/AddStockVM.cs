using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using MVVM;
using StockMon.Engines;
using StockMon.Models.JSON;
using StockMon.Models.SQLite;
using StockMon.Models.ViewModels.Commands;
using StockMon.Services.Logic;
using Xamarin.Forms;

namespace StockMon.Models.ViewModels.MVVMHelpers
{
    public class AddStockVM: BaseViewModel
    {
        public ObservableCollection<Quote> StockQueryResultData { get; set; }
        AddStockQueryCommand AddStockQueryCommand;

        public Entry StockSearchInput { get; set; }

        private string searchQuery;
        public string SearchQuery
        {
            get => searchQuery;
            set => SetProperty(ref searchQuery, value, onChanged: () => { });
        }

        public AddStockVM()
        {
            StockQueryResultData = new ObservableCollection<Quote>();
            AddStockQueryCommand = new AddStockQueryCommand(this);
        }

        public async void QueryStockCodeName()
        {
            string searchFor = searchQuery;
            string region = "US";
            string language = "en-US";

            DismissKeyboard();
            if (searchFor == null || searchFor.Length == 0)
            {
                await App.Current.MainPage.DisplayAlert("Query Error", "Cannot query for nothing", "OK");
                return;
            }

            searchFor = WebUtility.UrlEncode(searchFor.Trim());

            var stockQueryResultList = await YahooAPIQuery.GetStockSearchQueryData(searchFor, region, language, 12, 3, false);

            Quote.UpdateUsableStockNamesForResults(ref stockQueryResultList);

            if (stockQueryResultList.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("No Results", $"The query \"{searchQuery}\" yielded no results.", "OK");
                return;
            }

            StockQueryResultData.Clear();
            stockQueryResultList.ForEach(stock => StockQueryResultData.Add(stock));
        }

        public async void AddStockInfoToDatabase(string StockName, string StockCode)
        {
            StockListEntries entry = new StockListEntries
            {
                StockLongName = StockName,
                StockCode = StockCode
            };

            bool didSave = await AddPostEntry(entry);
            if (didSave)
                await App.Current.MainPage.DisplayAlert("Success!", "Successfully added a Stock Entry", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Failure!", "Failed to add a Stock Entry", "OK");
        }

        private async Task<bool> AddPostEntry(StockListEntries stockItem)
        {
            //using (SQLiteConnection conn = new SQLiteConnection(Constants.SQLiteDbLocation))
            //{
            //    conn.CreateTable<StockListEntries>();
            //    rows = conn.Insert(stockItem);
            //}
            var saveResult = await SQLiteDatabaseHelper<StockListEntries>.SaveRecord(stockItem);
            return saveResult.IsValid;
        }

        public async void AddSelectedItemFromList(Quote item)
        {
            var StockName = item.name;
            var StockCode = item.symbol;
            var ShouldAdd = await App.Current.MainPage.DisplayAlert("Add Stock?", $"Do you want to add the selected stock?\r\n\r\n{StockName}\r\n\r\nCode:  {StockCode}", "Yes", "No");

            if (ShouldAdd)
            {
                //Query if entry exists before adding again...
                var queryResult = SQLiteDatabaseHelper<StockListEntries>.ReadRecordToList(record => record.StockCode == StockCode);

                if (queryResult.Count > 0)
                {
                    await App.Current.MainPage.DisplayAlert("Already added", $"You already have '{StockCode}' in your list.", "OK");
                    return;
                }
                AddStockInfoToDatabase(StockName, StockCode);
            }
        }

        public void DismissKeyboard()
        {
            StockSearchInput?.Unfocus();
        }

        public override void ApplyCommand(Button button)
        {
            button.Command = AddStockQueryCommand;
        }
    }
}
