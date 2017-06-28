using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class Fourmiliere
    {
        // CONTIENT REFERENCE SUR LA CASE
        private CaseAbstrait RefCase { get; set; }
        // CONTIENT UN NBNOURRITURE RECOLTE
        public int NombreNourritures { get; set; } = 0;
        // CONTIENT UN NBTOUR
        public int NombreTours { get; set; } = 0;

        public Fourmiliere(CaseAbstrait _RefCase, int fourmisDefaut)
        {
            RefCase = _RefCase;
            for (int i = 0; i < fourmisDefaut; i ++)
            {
                RefCase.Fourmis.Add(new Fourmi(RefCase));
            }
        }

        // FONCTION: ADD / SUPP NOURRITURE && GET / SET NBNOURRITURE
        public void AjouterNourriture()
        {
            NombreNourritures++;
        }

        // FONCTION GENERE FOURMI => GENERE UNE FOURMI SUR LA CASE AVEC PLUS OU MOINS DE CHANCE SELON LE NB NOURRITURE


        // FONCTION MiseAjour => CONSOMME 1 NOURRITURE TOUT LES X tour puis appel genere fourmi
        public void MiseAjour()
        {
            NombreTours++;
            AjouterFourmis();
        }

        private void SupprimerNourriture()
        {
            if (NombreNourritures > 0)
                NombreNourritures --;
        }

        private void AjouterFourmis()
        {
            if (NombreNourritures > 0)
                RefCase.AjouterEnAjoutFourmi(new Fourmi(RefCase));
            if (NombreTours % 10 == 0)
                RefCase.AjouterEnAjoutFourmi(new Fourmi(RefCase));
            SupprimerNourriture();
        }
    }
}
