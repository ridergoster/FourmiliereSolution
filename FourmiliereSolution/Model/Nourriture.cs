using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Nourriture
    {
        // CONTIENT REFERENCE SUR LA CASE
        private Case RefCase { get; set; }
        // CONTIENT UN POIDS (x fois que des fourmis peut ramasser la nourriture avant d'être détruit)
        private int Poids { get; set; }

        public Nourriture(Case _RefCase, int _Poids)
        {
            RefCase = _RefCase;
            Poids = _Poids;
        }

        // FONCTION MANGER => enlève 1 au poids
        public void Manger()
        {
            Poids--;
        }

        // FONCTION MiseAjour => si poids 0: supprime de la case
        public void MiseAjour()
        {
            if (Poids == 0)
            {
                // Delete
                ;
            }
        }
    }
}
