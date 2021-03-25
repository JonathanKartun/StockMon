using System;
using System.ComponentModel;
using MVVM;

namespace StockMon.Models.ViewModels.Commands
{
    public class AddStockNavigationCommand : RedoneCommand
    {
        readonly MainVM ViewModel;
        public AddStockNavigationCommand(INotifyPropertyChanged viewModel) : base(viewModel)
        {
            ViewModel = (MainVM)viewModel;
        }

        public override event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            ViewModel.NavigateToAddStockPage();
        }
    }
}
