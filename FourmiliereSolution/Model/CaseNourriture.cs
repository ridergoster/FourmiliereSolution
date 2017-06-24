using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class CaseNourriture : CaseAbstrait
    {
        public Nourriture Nourriture { get; set; }

        public CaseNourriture(Terrain _RefTerrain, int _cordX, int _cordY) : base(_RefTerrain, _cordX, _cordY)
        {

        }

        // lance le MiseAjour de base pour déplacement de fourmi puis  lance MiseAjour nourriture
        public override void MiseAjour()
        {
            foreach(Fourmi fourmi in Fourmis)
            {
                if (fourmi.StrategieFourmi is StrategieRechercheNourriture)
                {
                    if(Nourriture.EstVide()) break;
                    Nourriture.Manger();
                    fourmi.StrategieFourmi = new StrategieRetourMaison();
                }
            }
            base.MiseAjour();
            if (Nourriture.EstVide()) this.RefTerrain.CasesNourritureTrigger.Add(this.AdapteurNormal());
        }

        public override bool ContientNourriture()
        {
            return true;
        }

        public override bool ContientFourmiliere()
        {
            return false;
        }
    }
}
