using Xamarin.Forms;

namespace MVVM
{
    public abstract class BaseViewModel : ObservableObject
    {
        virtual public void ApplyCommand(Button button) { }
        virtual public void ApplyCommand(ToolbarItem button) { }
    }
}
