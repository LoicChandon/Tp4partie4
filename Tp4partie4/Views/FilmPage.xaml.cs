using Microsoft.UI.Xaml.Controls;

using Tp4partie4.ViewModels;

namespace Tp4partie4.Views;

public sealed partial class FilmPage : Page
{
    public FilmViewModel ViewModel
    {
        get;
    }

    public FilmPage()
    {
        ViewModel = App.GetService<FilmViewModel>();
        InitializeComponent();
    }
}
