using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tp4partie4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp4partie4.Models;

namespace Tp4partie4.ViewModels.Tests
{
    [TestClass()]
    public class UtilisateursViewModelTests
    {
        //[TestMethod()]
        //public void UtilisateursViewModelTest_Constructor_RightRoutes()
        //{
        //    UtilisateurViewModel viewModel = new();
        //    Assert.AreEqual("Utilisateurs", viewModel.ROUTE_CONTROLLEUR, "Route controlleur pas bonne");
        //    Assert.AreEqual("GetById", viewModel.ROUTE_GETBYID, "Route getbyid pas bonne");
        //    Assert.AreEqual("GetByEmail", viewModel.ROUTE_GETBYEMAIL, "Route getbyemail pas bonne");
        //}

        [TestMethod()]
        public void SearchUtilisateurActionTest_ReturnsRightItem()
        {
            UtilisateurViewModel viewModel = new();
            Utilisateur utilisateur = new()
            {
                UtilisateurId = 2,
                Nom = "Gwendolin",
                Prenom = "Dominguez",
                Mobile = "0724970555",
                Mail = "gdominguez0@washingtonpost.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de gom",
                CodePostal = "73420",
                Ville = "Voglans",
                Pays = "France",
                Latitude = 45.622154F,
                Longitude = 5.8853216F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            };

            viewModel.SearchMail = utilisateur.Mail;
            viewModel.SearchUtilisateurAction().Wait();

            Assert.AreEqual(utilisateur, viewModel.UtilisateurSearch, "Utilisateurs pas égaux");
        }

        [TestMethod()]
        public void ModifyUtilisateurActionTest_ReturnsNoError()
        {
            UtilisateurViewModel viewModel = new();
            viewModel.UtilisateurSearch = new()
            {
                UtilisateurId = 2,
                Nom = "Gwendolin",
                Prenom = "Dominguez",
                Mobile = "0724970555",
                Mail = "gdominguez0@washingtonpost.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de gom",
                CodePostal = "73420",
                Ville = "Voglans",
                Pays = "France",
                Latitude = 45.622154F,
                Longitude = 5.8853216F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            };

            viewModel.ModifyUtilisateurAction().Wait();
        }

        [TestMethod()]
        public void ClearUtilisateurActionTest_UtilisateurNull()
        {
            UtilisateurViewModel viewModel = new();
            viewModel.UtilisateurSearch = new()
            {
                Nom = "GRANDJEAN",
                Prenom = "Kévin"
            };

            viewModel.ClearUtilisateurAction();

            Assert.IsNull(viewModel.UtilisateurSearch, "Utilisateur pas clear");
        }

        [TestMethod()]
        public void AddUtilisateurActionTest_ReturnsNewId()
        {
            UtilisateurViewModel viewModel = new();
            viewModel.UtilisateurSearch = new()
            {
                Nom = "Grandjean",
                Prenom = "Kévin",
                Mobile = "0630244856",
                Mail = $"kevin.grandjean{new Random().Next(16_777_216)}@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F
            };

            viewModel.AddUtilisateurAction().Wait();

            Assert.AreNotEqual(0, viewModel.UtilisateurSearch.UtilisateurId, "Id toujours à zéro");
        }
    }
}