using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class StrategieRechercheNourriture : StrategieFourmi
    {
        public bool Manger { get; set;  } = false;
        public bool Trigger { get; set; } = false;
        private Case CaseNourritureOptimal(List<Case> list)
        {
            int i = 0;
            int optimal = 0;
            int nbPheromoneNourritureMax = 0;
            foreach(Case caseItem in list)
            {
                if (caseItem.ContientNourriture())
                {
                    optimal = i;
                    break;
                }
                if (caseItem.PheromoneNourriture > nbPheromoneNourritureMax)
                {
                    nbPheromoneNourritureMax = caseItem.PheromoneNourriture;
                    optimal = i;
                }
                i++;
            }
            return list[optimal];
        } 
        public Case update(Case refCase)
        {
            if(refCase.ContientNourriture())
            {
                // enlever nourrituer a la case et trigger a true pour changer de strategie
                Trigger = true;
                Manger = true;
                return refCase;
            } else
            {
                List<Case> list = refCase.CasesAdjacentes();
                Case caseOptimal = CaseNourritureOptimal(list);
                return caseOptimal;
            }
        }
    }
}
