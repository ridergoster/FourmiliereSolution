using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Fourmiliere
    {
        // CONTIENT REFERENCE SUR LA CASE
        private Case RefCase { get; set; }
        // CONTIENT UN NBNOURRITURE RECOLTE
        private int NombreNourritures { get; set; }

        public Fourmiliere(Case _RefCase, int _NombreNourritures)
        {
            RefCase = _RefCase;
            NombreNourritures = _NombreNourritures;
        }

        // FONCTION: ADD / SUPP NOURRITURE && GET / SET NBNOURRITURE
        

        // FONCTION GENERE FOURMI => GENERE UNE FOURMI SUR LA CASE AVEC PLUS OU MOINS DE CHANCE SELON LE NB NOURRITURE


        // FONCTION UPDATE => CONSOMME 1 NOURRITURE TOUT LES X tour puis appel genere fourmi
        public void Update()
        {
            if (NombreNourritures > 0)
            {
                NombreNourritures--;
            }
        }
    }
}
