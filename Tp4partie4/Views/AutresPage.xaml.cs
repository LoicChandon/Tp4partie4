using Microsoft.UI.Xaml.Controls;

using Tp4partie4.ViewModels;

namespace Tp4partie4.Views;

public sealed partial class AutresPage : Page
{
    public AutresViewModel ViewModel
    {
        get;
    }

    public AutresPage()
    {
        ViewModel = App.GetService<AutresViewModel>();
        InitializeComponent();
    }
}
