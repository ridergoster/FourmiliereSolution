using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class Fourmi
    {
        // CONTIENT REFERENCE SUR LA CASE
        public CaseAbstrait RefCase { get; set; }
        public CaseAbstrait OldCase { get; set; }

        // CONTIENT UN NB VIE
        public int Vie { get; set; } = 100;
        // CONTIENT STRATEGIE 
        public StrategieFourmi StrategieFourmi { get; set; } = new StrategieRechercheNourriture();

        public Fourmi(CaseAbstrait _RefCase)
        {
            RefCase = _RefCase;
        }

        // FONCTION MiseAjour => via stratégie cherche selon les case adjacentes celle avec 
        // le plus de phéromone maison ou nourriture
        public void MiseAjour()
        {
            CaseAbstrait newCase = StrategieFourmi.MiseAjour(this);
            Vie--;

            if (Vie == 0)
            {
                RefCase.AjouterASupprimerFourmi(this);
                RefCase = null;
            }
            else if (newCase != RefCase)
            {
                RefCase.AjouterASupprimerFourmi(this);
                OldCase = RefCase;
                RefCase = newCase;
                RefCase.AjouterEnAjoutFourmi(this);
            }
        }
    }
}
