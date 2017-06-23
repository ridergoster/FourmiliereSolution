using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Fourmi
    {
        static Random Hasard = new Random();

        // CONTIENT REFERENCE SUR LA CASE
        private Case RefCase { get; set; }

        // CONTIENT UN NB VIE
        private int Vie { get; set; } = Hasard.Next(50, 100);

        // CONTIENT STRATEGIE 
        private StrategieFourmi StrategieFourmi { get; set; } = new StrategieRechercheNourriture();

        public Fourmi(Case _RefCase)
        {
            RefCase = _RefCase;
        }

        // FONCTION MiseAjour => via stratégie cherche selon les case adjacentes celle avec 
        // le plus de phéromone maison ou nourriture
        public void MiseAjour()
        {
            Case newCase = StrategieFourmi.MiseAjour(RefCase);
            if (StrategieFourmi.Trigger)
            {
                if (StrategieFourmi.Manger)
                {
                    StrategieFourmi = new StrategieRetourMaison();
                } 
                else
                {
                    StrategieFourmi = new StrategieRechercheNourriture();
                }
            }
            else
            {
                RefCase.AjouterASupprimerFourmi(this);
                RefCase = newCase;
                RefCase.AjouterEnAjoutFourmi(this);
            }
        }

        public bool PorteNourriture()
        {
            return StrategieFourmi.Manger;
        }
    }
}
