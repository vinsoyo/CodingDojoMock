using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MIAM
{
    public class MockRepasView : IRepasView
    {
        private readonly TestContext _testContext;

        public MockRepasView(TestContext testContext)
        {
            _testContext = testContext;
        }

        public void EffacerRepas()
        {
            _testContext.WriteLine("Les repas ont été effacés");
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