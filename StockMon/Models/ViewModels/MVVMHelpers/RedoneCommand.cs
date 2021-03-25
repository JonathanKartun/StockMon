using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVM
{
    public abstract class RedoneCommand : ICommand
    {
        virtual public INotifyPropertyChanged _UnderlyingViewModel { get; set; }
        private Command Command { get; set; }

        public RedoneCommand(INotifyPropertyChanged viewModel)
        {
            this._UnderlyingViewModel = viewModel;
            Command = new Command(Execute, CanExecute);
            viewModel.PropertyChanged += (s, e) => { ChangeCanExecute(); };
        }

        public void ApplyActionToButton(Button button)
        {
            button.Command = Command;
        }

        public void ChangeCanExecute()
        {
            Command.ChangeCanExecute();
        }

        abstract public event EventHandler CanExecuteChanged;
        abstract public bool CanExecute(object parameter);
        abstract public void Execute(object parameter);
    }
}
