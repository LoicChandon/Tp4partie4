using Microsoft.UI.Xaml.Controls;

using Tp4partie4.ViewModels;

namespace Tp4partie4.Views;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel
    {
        get;
    }

    public HomePage()
    {
        ViewModel = App.GetService<HomeViewModel>();
        InitializeComponent();
    }
}
