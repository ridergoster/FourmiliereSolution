using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class CaseNourriture : Case
    {
        private Nourriture nourriture;
        public Nourriture Nourriture { get => nourriture; set => nourriture = value; }
        public CaseNourriture(Case _refCase, Nourriture nourriture) : base(_refCase.RefTerrain, _refCase.CordX, _refCase.CordY)
        {
            ContientNourriture = true;
            Nourriture = nourriture;
        }

        // lance le MiseAjour de base pour déplacement de fourmi puis  lance MiseAjour nourriture
        public override void MiseAjour()
        {
            base.MiseAjour();
        }

        // dessine une nourriture
        public override void draw()
        {
            base.draw();
        }
    }
}
