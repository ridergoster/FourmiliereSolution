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

        public Terrain(Case[,] _Cases = new Case[20, 20])
        {
            Cases = _Cases;
        }

        // FONCTION UPDATE
        public void UpdateTerrain()
        {

        }

        // FONCTION DRAW
        public void DessineTerrain()
        {

        }
    }
}
