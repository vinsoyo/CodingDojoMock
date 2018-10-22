using System.Collections.Generic;

namespace MIAM
{
    public interface IRepasView
    {
        void EffacerRepas();
        void AfficherRepas(List<Repas> repas);
    }
}