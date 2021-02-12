using System;
using StockMon.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockMon
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        //Launcher for SQLiteDB from Platform
        public App(string dbLocation)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            Constants.SQLiteDbLocation = dbLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
