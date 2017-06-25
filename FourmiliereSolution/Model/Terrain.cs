using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class Terrain
    {
        // CONTIENT UN ARRAY 2D DE CASE DE X SUR Y TAILLE
        public CaseAbstrait[,] Cases { get; set; }
        public List<CaseAbstrait> CasesNourritureTrigger { get; set; } = new List<CaseAbstrait>();
        public FabriqueGeneral FabriqueGeneral { get; set; } = new FabriqueGeneral();
        public int Size;

        public Terrain(int number)
        {
            Size = number;
            Cases = new CaseAbstrait[number, number];
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    Cases[i, j] = new CaseNormal(this, i, j);
                }
            }
        }

        // FONCTION MiseAjour
        public void MiseAjour()
        {
            foreach(CaseAbstrait refCase in Cases)
            {
                refCase.MiseAjour(); // mettre à jour la case
            }

            if(CasesNourritureTrigger.Count > 0)
            {
                foreach(CaseAbstrait caseReset in CasesNourritureTrigger)
                {
                    Cases[caseReset.CordX, caseReset.CordY] = caseReset;
                    FabriqueGeneral.AjouterNourritureAuHasard(this, Size, 10);
                }
                CasesNourritureTrigger.Clear();
            }
            foreach(CaseAbstrait refCase in Cases)
            {
                refCase.AjouterFourmi(); // ajouter fourmi post update
                refCase.SupprimerFourmi(); // supprimer formi post update
            }
        }
    }
}
