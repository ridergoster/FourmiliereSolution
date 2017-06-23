using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class StrategieRetourMaison : StrategieFourmi
    {
        public bool Manger { get; set; } = true;
        public bool Trigger { get; set; } = false;

        static Random Hasard = new Random();
        private Case CaseMaisonOptimal(List<Case> list)
        {
            int i = 0;
            int optimal = 0;
            int nbPheromoneMaisonMax = 0;
            foreach (Case caseItem in list)
            {
                if (caseItem.ContientFourmiliere == true)
                {
                    optimal = i;
                    break;
                }
                if (caseItem.PheromoneMaison > nbPheromoneMaisonMax)
                {
                    nbPheromoneMaisonMax = caseItem.PheromoneMaison;
                    optimal = i;
                }
                i++;
            }
            if ((i + 1) == list.Count && nbPheromoneMaisonMax == 0)
            {
                return list[Hasard.Next(0, (list.Count - 1))];
            }
            return list[optimal];
        }
        public Case MiseAjour(Case refCase)
        {
            if (refCase.ContientFourmiliere)
            {
                // enlever nourrituer a la case et trigger a true pour changer de strategie
                Trigger = true;
                Manger = false;
                return refCase;
            }
            else
            {
                List<Case> list = refCase.CasesAdjacentes();
                Case caseOptimal = CaseMaisonOptimal(list);
                return caseOptimal;
            }
        }
    }
}
