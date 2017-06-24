﻿using System;
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
            if ((i + 1) == list.Count && nbPheromoneMaisonMax == 0)
            {
                return list[Hasard.Next(0, (list.Count - 1))];
            }
            return list[optimal];
        }
        public CaseAbstrait MiseAjour(CaseAbstrait refCase)
        {
            if (refCase.ContientFourmiliere())
            {
                return refCase;
            }
            else
            {
                refCase.AjouterPheromoneNourriture();
                List<CaseAbstrait> list = refCase.CasesAdjacentes();
                CaseAbstrait caseOptimal = CaseMaisonOptimal(list);
                return caseOptimal;
            }
        }
    }
}
