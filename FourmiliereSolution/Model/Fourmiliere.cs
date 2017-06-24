﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Fourmiliere
    {
        // CONTIENT REFERENCE SUR LA CASE
        private CaseAbstrait RefCase { get; set; }
        // CONTIENT UN NBNOURRITURE RECOLTE
        private int NombreNourritures { get; set; } = 0;
        // CONTIENT UN NBTOUR
        private int NombreTours { get; set; } = 0;

        public Fourmiliere(CaseAbstrait _RefCase)
        {
            RefCase = _RefCase;
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
            RefCase.AjouterEnAjoutFourmi(new Fourmi(RefCase));
            NombreTours = ((NombreTours + 1) % 20);

            if (NombreTours == 0)
            {
                NombreNourritures--;
            }
        }
    }
}
