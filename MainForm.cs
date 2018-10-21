using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MIAM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            using (MiamDbContext context = new MiamDbContext())
            {
                int nb = ((int)DayOfWeek.Friday -
                          (int)DateTime.Today.DayOfWeek + 7) % 7;
                DateTime dateTime2 = DateTime.Today.AddDays(nb);
                List<Repas> repas = context.Repas.Where(x => DateTime.Today <= x.Date
                                                             && x.Date <= dateTime2).ToList();
                if (!repas.Any())
                {
                    dataGridView1.DataSource = new List<Repas>();
                    MessageBox.Show("Pas de repas!");
                }
                else
                    dataGridView1.DataSource = repas;
            }
        }
    }

    [TestClass]
    public class DojoTest
    {
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
                new Repas {Date = new DateTime(2018,10,26),Plat = "Repas du samedi"},
                new Repas {Date = new DateTime(2018,10,26),Plat = "Repas du dimanche"},
            };

            DateTime aujourdhuiCEstJeudi = new DateTime(2018, 10, 25);

            // Act
            List<Repas> repasAffiches = null;

            // Assert
            List<Repas> repasAttendus = new List<Repas>()
            {
                new Repas {Date = new DateTime(2018,10,25),Plat = "Repas du jeudi"},
                new Repas {Date = new DateTime(2018,10,26),Plat = "Repas du vendredi"},
            };

            repasAffiches.Should().BeEquivalentTo(repasAttendus);
        }
    }
}