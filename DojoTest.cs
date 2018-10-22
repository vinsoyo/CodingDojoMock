using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MIAM
{
    [TestClass]
    public class DojoTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void
            GIVEN_UnRepasChaqueJourEtOnEstJeudi_WHEN_AfficherRepas_THEN_DevraitAfficherRepasDuJeudiEtDuVendredi()
        {
            // Arrange
            List<Repas> unRepasPourChaqueJourDeLaSemaine = new List<Repas>
            {
                new Repas {Date = new DateTime(2018, 10, 22), Plat = "Repas du lundi"},
                new Repas {Date = new DateTime(2018, 10, 23), Plat = "Repas du mardi"},
                new Repas {Date = new DateTime(2018, 10, 24), Plat = "Repas du mercredi"},
                new Repas {Date = new DateTime(2018, 10, 25), Plat = "Repas du jeudi"},
                new Repas {Date = new DateTime(2018, 10, 26), Plat = "Repas du vendredi"},
                new Repas {Date = new DateTime(2018, 10, 27), Plat = "Repas du samedi"},
                new Repas {Date = new DateTime(2018, 10, 28), Plat = "Repas du dimanche"},
            };

            DateTime aujourdhuiCEstJeudi = new DateTime(2018, 10, 25);
            MockRepasView mockRepasView = new MockRepasView(TestContext);
            SproutClass sproutClass = new SproutClass(mockRepasView, new InMemoryDbContext(unRepasPourChaqueJourDeLaSemaine));

            // Act
            sproutClass.RafraichirListeRepas(aujourdhuiCEstJeudi);

            // Assert
            List<Repas> repasAffiches = mockRepasView.DerniersRepasAffiches;
            List<Repas> repasAttendus = new List<Repas>()
            {
                new Repas {Date = new DateTime(2018, 10, 25), Plat = "Repas du jeudi"},
                new Repas {Date = new DateTime(2018, 10, 26), Plat = "Repas du vendredi"},
            };

            repasAffiches.Should().BeEquivalentTo(repasAttendus);
        }

        [TestMethod]
        public void GIVEN_AucunRepas_WHEN_AfficherRepas_THEN_DevraitAfficherMessageAUtilisateur()
        {
            MockRepasView mockRepasView = new MockRepasView(TestContext);
            List<Repas> aucunRepas = new List<Repas>();
            Mock<IUIFeedBack> mock = new Mock<IUIFeedBack>();
            SproutClass sproutClass = new SproutClass(mockRepasView, new InMemoryDbContext(aucunRepas), mock.Object);

            // Act
            sproutClass.RafraichirListeRepas();

            // assert
            mock.Verify(x=>x.AfficherMessage(It.Is<string>(message => message.Contains("Pas de repas"))), Times.Once);
        }
    }
}