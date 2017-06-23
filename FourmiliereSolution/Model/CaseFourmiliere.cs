using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class CaseFourmiliere : Case
    {
        private Fourmiliere fourmiliere;
        public Fourmiliere Fourmiliere { get => fourmiliere; set => fourmiliere = value; }
        public CaseFourmiliere(Case _refCase, Fourmiliere fourmiliere) : base(_refCase.RefTerrain, _refCase.CordX, _refCase.CordY)
        {
            ContientFourmiliere = true;
            Fourmiliere = fourmiliere;
        }

        // lance le MiseAjour de base pour déplacement de fourmi puis  lance MiseAjour fourmiliere
        public override void MiseAjour()
        {
            base.MiseAjour();
        }

        // dessine une fourmiliere
        public override void draw()
        {
            base.draw();
        }
    }
}
