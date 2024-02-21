using System.Security.Cryptography.X509Certificates;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tp4partie4.Models;
using Tp4partie4.Services;

namespace Tp4partie4.ViewModels;

public partial class UtilisateurViewModel : ObservableRecipient
{
    public UtilisateurViewModel()
    {
        BtnAddUtilisateurCommand = new RelayCommand(()=>AddUtilisateurAction());
        BtnModifyUtilisateurCommand = new RelayCommand(()=>ModifyUtilisateurAction());
        BtnSearchUtilisateurCommand = new RelayCommand(()=>SearchUtilisateurAction());
        BtnClearUtilisateurCommand = new RelayCommand(ClearUtilisateurAction);
        UtilisateurSearch = new Utilisateur();
        //je peux car Task --> ()=>SearchUtilisateurAction()
        //SearchUtilisateurAction().Wait();
    }

    public void ClearUtilisateurAction()
    {
        UtilisateurSearch = new Utilisateur();
        App.MessageAsync("C bon chef", "Les données ont bien été clear.");
    }
    public async Task SearchUtilisateurAction()
    {
        WSService service = new WSService();
        try
        {
            UtilisateurSearch = await service.GetByEmailAsync(SearchMail);
            //App.MessageAsync("Information", "Voici les utilisateurs");
        }
        catch (Exception e)
        {
            if (e is HttpRequestException ex && ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                App.MessageAsync("Pas trouvé", SearchMail);
            }
            else 
            App.MessageAsync("Erreur", e.Message);
        }
    }
    //idk why ca marche pas
    public async Task ModifyUtilisateurAction()
    {
        WSService service = new WSService();
        try
        {
            await service.PutUtilisateurAsync(UtilisateurSearch.UtilisateurId,UtilisateurSearch);
        }
        catch (Exception e)
        {
            App.MessageAsync("Erreur", e.Message);
        }
    }
    //pas oublier de changer void en Task
    public async Task AddUtilisateurAction()
    {
        WSService service = new WSService();
        try
        {
            UtilisateurSearch = await service.PostUtilisateurAsync(UtilisateurSearch);
            App.MessageAsync("Information",$"Utilisateur {UtilisateurSearch.Nom} ajouté");
        }
        catch (Exception e)
        {
            App.MessageAsync("Erreur", e.Message);
        }

    }
    private string searchMail;

    public string SearchMail
    {
        get
        {
            return searchMail;
        }
        set
        {
            searchMail = value;
            OnPropertyChanged();
        }
    }


    public IRelayCommand BtnSearchUtilisateurCommand{ get; set; }
    public IRelayCommand BtnModifyUtilisateurCommand
    {
        get; set;
    }
    public IRelayCommand BtnAddUtilisateurCommand
    {
        get; set;
    }
    public IRelayCommand BtnClearUtilisateurCommand
    {
        get; set;
    }
    private Utilisateur utilisateurSearch;

    public Utilisateur UtilisateurSearch
    {
        get
        {
            return utilisateurSearch;
        }
        set
        {
            utilisateurSearch = value;
            OnPropertyChanged();
        }
    }

}
