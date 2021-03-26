using System;
using System.ComponentModel;
using MVVM;
using StockMon.Models.ViewModels.MVVMHelpers;

namespace StockMon.Models.ViewModels.Commands
{
    public class AddStockQueryCommand: RedoneCommand
    {
        AddStockVM ViewModel;
        public AddStockQueryCommand(INotifyPropertyChanged viewModel) : base(viewModel)
        {
            ViewModel = (AddStockVM)viewModel;
        }

        public override event EventHandler CanExecuteChanged;

        public override bool CanExecute(object parameter)
        {
            if (parameter == null) return false;
            if (ViewModel.SearchQuery.Trim().Length > 0)
            {
                return true;
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            ViewModel.QueryStockCodeName();
        }
    }
}
