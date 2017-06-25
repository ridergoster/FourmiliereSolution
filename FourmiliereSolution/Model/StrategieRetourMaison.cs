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

        static Random Hasard = new Random();
        private CaseAbstrait CaseMaisonOptimal(List<CaseAbstrait> list)
        {
            int i = 0;
            int optimal = 0;
            int nbPheromoneMaisonMax = 0;
            foreach (CaseAbstrait caseItem in list)
            {
                if (caseItem is CaseFourmiliere)
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
            if (i == list.Count && nbPheromoneMaisonMax == 0 && i != optimal)
            {
                return list[Hasard.Next(0, list.Count)];
            }
            return list[optimal];
        }
        public CaseAbstrait MiseAjour(Fourmi fourmi)
        {
            if (fourmi.RefCase.ContientFourmiliere())
            {
                return fourmi.RefCase;
            }
            else
            {
                fourmi.RefCase.AjouterPheromoneNourriture(fourmi.Vie);
                fourmi.RefCase.SupprimerPheromoneMaison(fourmi.Vie);
                List<CaseAbstrait> list = fourmi.RefCase.CasesAdjacentes();
                if (!(fourmi.RefCase is CaseNourriture)) list.Remove(fourmi.OldCase);
                CaseAbstrait caseOptimal = CaseMaisonOptimal(list);
                return caseOptimal;
            }
        }
    }
}
