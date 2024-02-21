using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tp4partie4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp4partie4.Models;

namespace Tp4partie4.Services.Tests
{
    [TestClass()]
    public class WSServiceTests
    {
        [TestMethod()]
        public void ConstructeurTest()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
        }

        [TestMethod()]
        public void GetAllAsyncTest()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            List<Utilisateur> utilisateurs = new List<Utilisateur>()
        {
            new()
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            },
            new()
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
            },
            new()
            {
                UtilisateurId = 3,
                Nom = "Randolph",
                Prenom = "Richings",
                Mobile = "0630271158",
                Mail = "rrichings1@naver.com",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            }
        };

            var result = service.GetAllAsync().Result;

            Assert.IsInstanceOfType(result, typeof(IEnumerable<Utilisateur>), "Pas un IEnumerable<Utilisateur>");
            CollectionAssert.AreEqual(utilisateurs, result.Where(u => u.UtilisateurId <= 3).OrderBy(u => u.UtilisateurId).ToList(), "Utilisateurs pas égales");
        }

        [TestMethod()]
        public void GetAsyncTest_ExistingIdPassed_ReturnsRightItem()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            Utilisateur utilisateur = new()
            {
                UtilisateurId = 3,
                Nom = "Randolph",
                Prenom = "Richings",
                Mobile = "0630271158",
                Mail = "rrichings1@naver.com",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            };

            var result = service.GetAsync("GetById", 3).Result;

            Assert.IsInstanceOfType(result, typeof(Utilisateur), "Pas un Utilisateur");
            Assert.AreEqual(utilisateur, result, "Utilisateurs pas égaux");
        }

        [TestMethod()]
        public void GetAsyncTest_ExistingEmailPassed_ReturnsRightItem()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            Utilisateur utilisateur = new()
            {
                UtilisateurId = 3,
                Nom = "Randolph",
                Prenom = "Richings",
                Mobile = "0630271158",
                Mail = "rrichings1@naver.com",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            };

            var result = service.GetAsync("GetByEmail", "rrichings1@naver.com").Result;

            Assert.IsInstanceOfType(result, typeof(Utilisateur), "Pas un Utilisateur");
            Assert.AreEqual(utilisateur, result, "Utilisateurs pas égaux");
        }

        [TestMethod()]
        public void GetAsyncTest_UnknownIdPassed_ReturnsErrorNotFound()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            TestActionException_StatusCode(() => { var result = service.GetAsync("GetById", 300_000).Result; }, HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public void GetAsyncTest_UnknownEmailPassed_ReturnsErrorNotFound()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            TestActionException_StatusCode(() => { var result = service.GetAsync("GetByEmail", "rrichings120@naver.com").Result; }, HttpStatusCode.NotFound);
        }

        [TestMethod()]
        public void PostAsyncTest_ValidModel_ReturnsRightItem()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            Utilisateur utilisateur = new()
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

            var result = service.PostAsync(utilisateur).Result;

            Assert.IsInstanceOfType(result, typeof(Utilisateur), "Pas un utilisateur");

            utilisateur.UtilisateurId = result.UtilisateurId;
            utilisateur.DateCreation = result.DateCreation;

            Assert.AreEqual(utilisateur, result, "Utilisateurs pas égaux");
        }

        [TestMethod()]
        public void PostAsyncTest_InvalidModel_ReturnsErrorBadRequest()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            Utilisateur utilisateur = new()
            {
                Nom = "Grandjean",
                Prenom = "Kévin",
                Mobile = "0630244856",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F
            };

            TestActionException_StatusCode(() => { var result = service.PostAsync(utilisateur).Result; }, HttpStatusCode.BadRequest);
        }

        [TestMethod()]
        public void PutAsyncTest_ValidId_ValidModel_ReturnsNoError()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            Utilisateur utilisateur = new()
            {
                UtilisateurId = 3,
                Nom = "Randolph",
                Prenom = "Richings",
                Mobile = "0630271158",
                Mail = "rrichings1@naver.com",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            };

            service.PutAsync(3, utilisateur).Wait();
        }

        [TestMethod()]
        public void PutAsyncTest_ValidId_InvalidModel_ReturnsErrorBadRequest()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            Utilisateur utilisateur = new()
            {
                Nom = "Randolph",
                Prenom = "Richings",
                Mobile = "0630271158",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            };

            TestActionException_StatusCode(() => { service.PutAsync(3, utilisateur).Wait(); }, HttpStatusCode.BadRequest);
        }

        [TestMethod()]
        public void PutAsyncTest_InvalidId_ValidModel_ReturnsErrorBadRequest()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            Utilisateur utilisateur = new()
            {
                UtilisateurId = 2,
                Nom = "Randolph",
                Prenom = "Richings",
                Mobile = "0630271158",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                Mail = "rrichings1@naver.com",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            };

            TestActionException_StatusCode(() => { service.PutAsync(3, utilisateur).Wait(); }, HttpStatusCode.BadRequest);
        }

        [TestMethod()]
        public void DeleteAsyncTest_ExistingIdPassed_ReturnsNoError()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            Utilisateur utilisateur = new()
            {
                Nom = "Randolph",
                Prenom = "Richings",
                Mobile = "0630271158",
                Pwd = "Toto12345678!",
                Rue = "Route des charmottes d'en bas",
                Mail = "moby_dick@naver.com",
                CodePostal = "74890",
                Ville = "Bons-en-Chablais",
                Pays = "France",
                Latitude = 46.25732F,
                Longitude = 6.367676F,
                DateCreation = DateTime.Parse("2024-02-19T00:00:00")
            };
            var result = service.PostAsync(utilisateur).Result;

            service.DeleteAsync(result.UtilisateurId).Wait();
        }

        [TestMethod()]
        public void DeleteAsyncTest_UnknownIdPassed_ReturnsErrorNotFound()
        {
            WSService<Utilisateur> service = new("Utilisateurs");
            TestActionException_StatusCode(() => { service.DeleteAsync(300_000).Wait(); }, HttpStatusCode.NotFound);
        }

        private void TestActionException_StatusCode(Action action, HttpStatusCode code)
        {
            try { action.Invoke(); }
            catch (AggregateException e)
            {
                Assert.IsInstanceOfType(e.InnerException, typeof(HttpRequestException), "Pas un HttpRequestException");
                Assert.AreEqual(code, ((HttpRequestException)e.InnerException).StatusCode, $"Pas un {code}");
                return;
            }

            Assert.Fail("Pas d'exception");
        }
    }
}