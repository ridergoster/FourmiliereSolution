using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class Nourriture
    {
        // CONTIENT REFERENCE SUR LA CASE
        private CaseAbstrait RefCase { get; set; }
        // CONTIENT UN POIDS (x fois que des fourmis peut ramasser la nourriture avant d'être détruit)
        public int Poids { get; set; }

        public Nourriture(CaseAbstrait _RefCase, int _Poids)
        {
            RefCase = _RefCase;
            Poids = _Poids;
        }

        // FONCTION MANGER => enlève 1 au poids
        public void Manger()
        {
            Poids--;
        }

        public bool EstVide()
        {
            return Poids <= 0;
        }
     }
}
