using Microsoft.UI.Xaml.Controls;

using Tp4partie4.ViewModels;

namespace Tp4partie4.Views;

public sealed partial class UtilisateurPage : Page
{
    public UtilisateurViewModel ViewModel
    {
        get;
    }

    public UtilisateurPage()
    {
        ViewModel = App.GetService<UtilisateurViewModel>();
        DataContext= ViewModel;
        InitializeComponent();
    }
}
