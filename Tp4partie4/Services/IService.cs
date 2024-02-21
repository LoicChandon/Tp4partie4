using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp4partie4.Models;

namespace Tp4partie4.Services;
internal interface IService
{
    Task<IEnumerable<Utilisateur>> GetAllUtilisateursAsync ();
    Task<Utilisateur> GetByIdAsync (int id);
    Task<Utilisateur> GetByEmailAsync(string email);
    Task<Utilisateur> PostUtilisateurAsync(Utilisateur utilisateur);
    Task PutUtilisateurAsync(int id ,Utilisateur utilisateur);
    Task DeleteUtilisateurAsync(int id);
}
