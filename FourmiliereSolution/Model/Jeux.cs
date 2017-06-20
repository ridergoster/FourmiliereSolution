using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Jeux
    {
        // CONTIENT UN TERRAIN
        private Terrain ZoneTerrain { get; set; }
        // CONTIENT DES PARAMS MODIFIABLES
        private KeyValuePair<string, string> Parametres;

        public Jeux(Terrain _ZoneTerrain, KeyValuePair<string, string> _Parametres)
        {
            ZoneTerrain = _ZoneTerrain;
            Parametres = _Parametres;
        }

        // LOAD GAME
        public void ChargerJeu() { }
      
        // SAVE GAME
        public void SauvegarderJeu() { }
    }
}
