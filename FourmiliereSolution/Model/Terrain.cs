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
        public Case[,] Cases;

        public Terrain(Case[,] _Cases)
        {
            Cases = _Cases;
        }

        // FONCTION MiseAjour
        public void MiseAjour()
        {
            foreach(Case refCase in Cases)
            {
                refCase.MiseAjour();
            }
            foreach(Case refCase in Cases)
            {
                refCase.AjouterFourmi();
                refCase.SupprimerFourmi();
            }
        }

        // FONCTION DRAW
        public void DessineTerrain()
        {

        }
    }
}
