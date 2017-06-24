using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class Fourmi
    {
        static Random Hasard = new Random();
        // CONTIENT REFERENCE SUR LA CASE
        private CaseAbstrait RefCase { get; set; }
        // CONTIENT UN NB VIE
        public int Vie { get; set; } = Hasard.Next(50, 100);
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
            Vie--;
            if(Vie == 0)
            {
                RefCase.AjouterASupprimerFourmi(this);
                RefCase = null;
            }
            else
            {
                CaseAbstrait newCase = StrategieFourmi.MiseAjour(RefCase);
                if (newCase != RefCase)
                {
                    RefCase.AjouterASupprimerFourmi(this);
                    RefCase = newCase;
                    RefCase.AjouterEnAjoutFourmi(this);
                }
            }
        }
    }
}
