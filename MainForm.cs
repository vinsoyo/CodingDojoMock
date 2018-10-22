using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MIAM
{
    public partial class MainForm : Form, IRepasView
    {
        private readonly SproutClass _sproutClass;

        public MainForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            _sproutClass = new SproutClass(this);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _sproutClass.RafraichirListeRepas();
        }

        public void EffacerRepas()
        {
            dataGridView1.DataSource = new List<Repas>();
        }

        public void AfficherRepas(List<Repas> repas)
        {
            dataGridView1.DataSource = repas;
        }
    }

    public interface IRepasView
    {
        void EffacerRepas();
        void AfficherRepas(List<Repas> repas);
    }

    public class SproutClass
    {
        private readonly IRepasView _mainForm;

        public SproutClass(IRepasView mainForm)
        {
            _mainForm = mainForm;
        }

        public void RafraichirListeRepas()
        {
            using (MiamDbContext context = new MiamDbContext())
            {
                int nb = ((int) DayOfWeek.Friday -
                          (int) DateTime.Today.DayOfWeek + 7) % 7;
                DateTime dateTime2 = DateTime.Today.AddDays(nb);
                List<Repas> repas = context.Repas.Where(x => DateTime.Today <= x.Date
                                                             && x.Date <= dateTime2).ToList();
                if (!repas.Any())
                {
                    _mainForm.EffacerRepas();
                    MessageBox.Show("Pas de repas!");
                }
                else
                    _mainForm.AfficherRepas(repas);
            }
        }
    }

    [TestClass]
    public class DojoTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GIVEN_UnRepasChaqueJourEtOnEstJeudi_WHEN_AfficherRepas_THEN_DevraitAfficherRepasDuJeudiEtDuVendredi()
        {
            // Arrange
            List<Repas> unRepasPourChaqueJourDeLaSemaine = new List<Repas>
            {
                new Repas {Date = new DateTime(2018,10,22),Plat = "Repas du lundi"},
                new Repas {Date = new DateTime(2018,10,23),Plat = "Repas du mardi"},
                new Repas {Date = new DateTime(2018,10,24),Plat = "Repas du mercredi"},
                new Repas {Date = new DateTime(2018,10,25),Plat = "Repas du jeudi"},
                new Repas {Date = new DateTime(2018,10,26),Plat = "Repas du vendredi"},
                new Repas {Date = new DateTime(2018,10,27),Plat = "Repas du samedi"},
                new Repas {Date = new DateTime(2018,10,28),Plat = "Repas du dimanche"},
            };

            DateTime aujourdhuiCEstJeudi = new DateTime(2018, 10, 25);
            MockRepasView mockRepasView = new MockRepasView(TestContext);
            SproutClass sproutClass = new SproutClass(mockRepasView);

            // Act
            sproutClass.RafraichirListeRepas();

            // Assert
            List<Repas> repasAffiches = mockRepasView.DerniersRepasAffiches;
            List<Repas> repasAttendus = new List<Repas>()
            {
                new Repas {Date = new DateTime(2018,10,25),Plat = "Repas du jeudi"},
                new Repas {Date = new DateTime(2018,10,26),Plat = "Repas du vendredi"},
            };

            repasAffiches.Should().BeEquivalentTo(repasAttendus);
        }
    }

    public class MockRepasView : IRepasView
    {
        private readonly TestContext _testContext;

        public MockRepasView(TestContext testContext)
        {
            _testContext = testContext;
        }

        public void EffacerRepas()
        {
            throw new NotImplementedException();
        }

        public void AfficherRepas(List<Repas> repas)
        {
            foreach (Repas unRepas in repas)
            {
                _testContext.WriteLine("Repas Affiche :"+ unRepas.Plat);
            }
            DerniersRepasAffiches = repas.ToList();
        }

        public List<Repas> DerniersRepasAffiches { get; set; }
    }
}