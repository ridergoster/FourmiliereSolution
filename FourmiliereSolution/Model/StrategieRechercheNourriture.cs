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
            Console.WriteLine("MOVING _");
            if (i == list.Count && nbPheromoneNourritureMax == 0 && i != optimal)
            {
                return list[Hasard.Next(0, list.Count)];
            }
            return list[optimal];
        } 
        public CaseAbstrait MiseAjour(Fourmi fourmi)
        {
            if (fourmi.RefCase.ContientNourriture())
            {
                return fourmi.RefCase;
            }
            else
            {
                fourmi.RefCase.AjouterPheromoneMaison(fourmi.Vie);
                List<CaseAbstrait> list = fourmi.RefCase.CasesAdjacentes();
                list.Remove(fourmi.OldCase);
                CaseAbstrait caseOptimal = CaseNourritureOptimal(list);
                return caseOptimal;
            }
        }
    }
}
