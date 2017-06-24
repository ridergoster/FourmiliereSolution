using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class StrategieRechercheNourriture : StrategieFourmi
    {
        static Random Hasard = new Random();
        private CaseAbstrait CaseNourritureOptimal(List<CaseAbstrait> list)
        {
            int i = 0;
            int optimal = 0;
            int nbPheromoneNourritureMax = 0;
            foreach(CaseAbstrait caseItem in list)
            {
                if (caseItem is CaseNourriture)
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
            if ((i + 1) == list.Count && nbPheromoneNourritureMax == 0)
            {
                return list[Hasard.Next(0, (list.Count - 1))];
            }
            return list[optimal];
        } 
        public CaseAbstrait MiseAjour(CaseAbstrait refCase)
        {
            if (refCase.ContientNourriture())
            {
                return refCase;
            }
            else
            {
                refCase.AjouterPheromoneMaison();
                List<CaseAbstrait> list = refCase.CasesAdjacentes();
                CaseAbstrait caseOptimal = CaseNourritureOptimal(list);
                return caseOptimal;
            }
        }
    }
}
