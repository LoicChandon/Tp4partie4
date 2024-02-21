using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Tp4partie4.Models;
using Windows.Media.Protection.PlayReady;
using static System.Net.WebRequestMethods;

namespace Tp4partie4.Services;
public class WSService : IService
{
    private HttpClient httpClient;
    public WSService()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7073/api/Utilisateurs/");
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
    }


    public async Task DeleteUtilisateurAsync(int id)
    {
        var response = await httpClient.DeleteAsync(id.ToString());
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync()
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<Utilisateur>>("");
        return response;
    }
    public async Task<Utilisateur> GetByEmailAsync(string email)
    {
        var response = await httpClient.GetFromJsonAsync<Utilisateur>($"GetUtilisateurByEmail/{email}");
        return response;

    }
    public async Task<Utilisateur> GetByIdAsync(int id)
    {
        var response = await httpClient.GetFromJsonAsync<Utilisateur>($"GetUtilisateurById/{id}");
        return response;
    }
    public async Task<Utilisateur> PostUtilisateurAsync(Utilisateur utilisateur)
    {
        var response = await httpClient.PostAsJsonAsync("",utilisateur);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Utilisateur>();
    }
    public async Task PutUtilisateurAsync(int id, Utilisateur utilisateur)
    {
        var response = await httpClient.PutAsJsonAsync(id.ToString(), utilisateur);
        response.EnsureSuccessStatusCode();
    }
}
