using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class CaseNourriture : CaseAbstrait
    {
        public Nourriture Nourriture { get; set; }

        public CaseNourriture(CaseAbstrait _refCase, Nourriture nourriture) : base(_refCase.RefTerrain, _refCase.CordX, _refCase.CordY)
        {
            Nourriture = nourriture;
        }

        // lance le MiseAjour de base pour déplacement de fourmi puis  lance MiseAjour nourriture
        public override void MiseAjour()
        {
            foreach(Fourmi fourmi in Fourmis)
            {
                if (fourmi.StrategieFourmi is StrategieRechercheNourriture)
                {
                    fourmi.StrategieFourmi = new StrategieRetourMaison();
                    /**
                    if (Nourriture.Manger())
                    {
                    }
                    else
                    {
                        Nourriture = null;
                        break;
                    } **/
                }
            }
            base.MiseAjour();
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
