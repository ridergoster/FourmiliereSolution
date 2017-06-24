using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class CaseFourmiliere : CaseAbstrait
    {
        public Fourmiliere Fourmiliere { get; set; }

        public CaseFourmiliere(Terrain _RefTerrain, int _cordX, int _cordY) : base(_RefTerrain, _cordX, _cordY)
        {

        }

        // lance le MiseAjour de base pour déplacement de fourmi puis  lance MiseAjour fourmiliere
        public override void MiseAjour()
        {
            Fourmiliere.MiseAjour();
            foreach (Fourmi fourmi in Fourmis)
            {
                if (fourmi.StrategieFourmi is StrategieRetourMaison)
                {
                    Fourmiliere.AjouterNourriture();
                    fourmi.StrategieFourmi = new StrategieRechercheNourriture();
                }
            }
            base.MiseAjour();
        }

        public override bool ContientNourriture()
        {
            return false;
        }

        public override bool ContientFourmiliere()
        {
            return true;
        }
    }
}
