using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Terrain
    {
        // CONTIENT UN ARRAY 2D DE CASE DE X SUR Y TAILLE
        public CaseAbstrait[,] Cases;

        public Terrain(CaseAbstrait[,] _Cases)
        {
            Cases = _Cases;
        }

        // FONCTION MiseAjour
        public void MiseAjour()
        {
            foreach(CaseAbstrait refCase in Cases)
            {
                refCase.MiseAjour(); // mettre à jour la case
            }
            foreach(CaseAbstrait refCase in Cases)
            {
                refCase.AjouterFourmi(); // ajouter fourmi post update
                refCase.SupprimerFourmi(); // supprimer formi post update
            }
        }

        // FONCTION DRAW
        public void DessineTerrain()
        {

        }
    }
}
