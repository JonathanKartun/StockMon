using System;
using System.Collections.Generic;
using StockMon.Services.Logic;
using Xamarin.Forms;

namespace StockMon
{
    public partial class AddStockPage : ContentPage
    {
        public AddStockPage()
        {
            InitializeComponent();
        }

        async void Search_Clicked(System.Object sender, System.EventArgs e)
        {
            string searchFor = StockSearchInput.Text;// "gme";
            string region = "US";
            string language = "en-US";
            var query = await YahooAPIQuery.GetStockSearchQueryData(searchFor, region, language, 6, 6, false);

            FixNamesForResults(query);
            StockQueryResultsListView.ItemsSource = query;
        }

        void FixNamesForResults(List<Models.JSON.Quote> query)
        {
            for (int i = query.Count - 1; i >=0; i--)
            {
                var q = query[i];
                var name = q.name;
                if (name == null)
                {
                    name = q.shortname;
                    if (name == null)
                    {
                        name = q.longname;
                    }
                }
                q.name = name;

                if (q.symbol == null || !q.isYahooFinance)
                {
                    query.Remove(q);
                    continue;
                }
            }
        }
        ViewCell lastCell;
        void ViewCell_Tapped(System.Object sender, System.EventArgs e)
        {
            //ViewCell selectedCell = (ViewCell)sender;
            //selectedCell.View.BackgroundColor = Color.Purple;
            ////ListV

            //if (lastCell != null) lastCell.View.BackgroundColor = Color.Transparent;
            //var viewCell = (ViewCell)sender;
            //if (viewCell.View != null)
            //{
            //    viewCell.View.BackgroundColor = Color.Yellow;
            //    lastCell = viewCell;
            //}

            //this.SetDynamicResource(Label.StyleProperty, "BackgroundColor");
            //this.SetDynamicResource(ListView.StyleProperty, "BackgroundColor");
            this.SetDynamicResource(Label.StyleProperty, "BackgroundColor");


        }
    }
}
