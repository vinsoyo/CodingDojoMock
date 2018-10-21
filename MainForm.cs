using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

}