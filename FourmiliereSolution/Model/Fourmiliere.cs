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
        private int NombreNourritures { get; set; } = 0;
        // CONTIENT UN NBTOUR
        private int NombreTours { get; set; } = 0;

        public Fourmiliere(Case _RefCase)
        {
            RefCase = _RefCase;
        }

        // FONCTION: ADD / SUPP NOURRITURE && GET / SET NBNOURRITURE
        

        // FONCTION GENERE FOURMI => GENERE UNE FOURMI SUR LA CASE AVEC PLUS OU MOINS DE CHANCE SELON LE NB NOURRITURE


        // FONCTION UPDATE => CONSOMME 1 NOURRITURE TOUT LES X tour puis appel genere fourmi
        public void UpdateFourmilliere()
        {
            NombreTours += (NombreTours + 1) % 20;

            if (NombreTours % 20 == 0)
            {
                NombreTours = 0;
                NombreNourritures--;
            }
        }
    }
}
