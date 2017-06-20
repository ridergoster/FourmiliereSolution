using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Case
    {
        private int CordX { get; set; }
        private int CordY { get; set; }

        // CONTIENT UNE REF SUR LE TERRAIN
        private Terrain RefTerrain { get; set; }
        // CONTIENT UN NB DE PHEROMONE "MAISON"
        private int PheromoneMaison { get; set; } = 0;
        // CONTIENT UN NB DE PHEROMONE "NOURRITURE"
        private int PheromoneNourriture { get; set; } = 0;
        // CONTIENT UN ARRAY DE FOURMI SUR LA CASE
        private List<Fourmi> Fourmis { get; set; } = new List<Fourmi>();
        // CONTIENT UNE NOURRITURE SUR LA CASE ==> class herit (caseNourriture)?
        // CONTIENT UNE FOURMILIERE SUR LA CASE ==> class herit (caseFourmiliere) ?

        public Case(Terrain _RefTerrain, int _cordX, int _cordY)
        {
            RefTerrain = _RefTerrain;
            CordX = _cordX;
            CordY = _cordY;
        }

        // FONCTION : CONTIENT NOURRITURE ?
        public bool ContientNourriture()
        {
            return false;
        }

        // FONCTION : CONTIENT FOURMILIERE ?
        public bool ContientFourmilliere()
        {
            return false;
        }

        // FONCTION : AJOUTER / SUPP FOURMI && SET / GET FOURMILIST
        public void AjouterFourmi(Fourmi NouvelleFourmi)
        {
            Fourmis.Add(NouvelleFourmi);
        }

        public void SupprimerFourmi(Fourmi FourmiASupprimer)
        {
            if (Fourmis.Count > 0 && Fourmis.Contains(FourmiASupprimer))
            {
                Fourmis.Remove(FourmiASupprimer);
            }
        }

        // FONCTION : AJOUTER / SUPP PHEROMONE MAISON NOURRITURE && SET / GET
        public void AjouterPheromoneMaison()
        {
            PheromoneMaison++;
        }

        public void SupprimerPheromoneMaison()
        {
            PheromoneMaison--;
        }

        public void AjouterPheromoneNourriture()
        {
            PheromoneNourriture++;
        }

        public void SupprimerPheromoneNourriture()
        {
            PheromoneNourriture--;
        }

        public List<Case> CasesAdjacentes(Case refCase)
        {
            List<Case> casesAdjacentes = new List<Case>();
            if (refCase.CordX > 0)
            {
                casesAdjacentes.Add(RefTerrain.Cases[refCase.CordX - 1, refCase.CordY]);
            }
            if (refCase.CordX < (RefTerrain.Cases.GetLength(0) - 1))
            {
                casesAdjacentes.Add(RefTerrain.Cases[refCase.CordX + 1, refCase.CordY]);
            }
            if (refCase.CordY > 0)
            {
                casesAdjacentes.Add(RefTerrain.Cases[refCase.CordX, refCase.CordY - 1]);
            }
            if (refCase.CordY < (RefTerrain.Cases.GetLength(1) - 1))
            {
                casesAdjacentes.Add(RefTerrain.Cases[refCase.CordX, refCase.CordY + 1]);
            }

            return casesAdjacentes;
        }

        // FONCTION : SET / GET NOURRITURE
        // FONCTION : SET / GET FOURMILIERE ==> herit ?

        // FONCTION UPDATE => 
        //      1. lance le update pour toute les fourmi pour les faire bouger
        //      2. lance le update pour bouffe / fourmilière si class herit
        //      3. lance fonction draw ? ou depuis le jeux ?
        public void UpdateCase()
        {

        }

        // FONCTION DRAW CASE 
        public void DessinerCase()
        {

        }
    }
}
