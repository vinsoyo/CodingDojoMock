using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
            _sproutClass = new SproutClass(this, new ContextFactory());
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
}