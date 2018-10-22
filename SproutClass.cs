using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MIAM
{
    public class SproutClass
    {
        private readonly IRepasView _mainForm;
        private readonly ContextFactory _contextFactory;

        public SproutClass(IRepasView mainForm, ContextFactory contextFactory)
        {
            _mainForm = mainForm;
            _contextFactory = contextFactory;
        }

        public void RafraichirListeRepas()
        {
            RafraichirListeRepas(DateTime.Today);
        }

        public void RafraichirListeRepas(DateTime dateTime)
        {
            using (IMiamDbContext context = _contextFactory.CreateContext())
            {
                int nb = ((int) DayOfWeek.Friday -
                          (int) dateTime.DayOfWeek + 7) % 7;
                DateTime dateTime2 = dateTime.AddDays(nb);
                List<Repas> repas = context.Repas.Where(x => dateTime <= x.Date
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
}