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
        // CONTIENT DES COORDONEES
        private int cordX;
        public int CordX { get => cordX; set => cordX = value; }

        private int cordY;
        public int CordY { get => cordY; set => cordY = value; }

        // CONTIENT UNE REF SUR LE TERRAIN
        private Terrain refTerrain;
        public Terrain RefTerrain{ get => refTerrain; set => refTerrain = value; }

        // CONTIENT UN NB DE PHEROMONE "MAISON"
        private int pheromoneMaison;
        public int PheromoneMaison { get => pheromoneMaison; set => pheromoneMaison = value; }

        // CONTIENT UN NB DE PHEROMONE "NOURRITURE"
        private int pheromoneNourriture;
        public int PheromoneNourriture { get => pheromoneNourriture; set => pheromoneNourriture = value; }

        // CONTIENT UN ARRAY DE FOURMI SUR LA CASE
        private List<Fourmi> fourmis;
        public List<Fourmi> Fourmis { get => fourmis; set => fourmis = value; }

        private List<Fourmi> fourmisEnAjout;
        public List<Fourmi> FourmisEnAjout { get => fourmisEnAjout; set => fourmisEnAjout = value; }

        private List<Fourmi> fourmisASupprimer;
        public List<Fourmi> FourmisASupprimer { get => fourmisASupprimer; set => fourmisASupprimer = value; }


        private bool contientNourriture = false;
        public bool ContientNourriture { get => contientNourriture; set => contientNourriture = value; }

        private bool contientFourmiliere = false;
        public bool ContientFourmiliere { get => contientFourmiliere; set => contientFourmiliere = value; }

        public Case(Terrain _RefTerrain, int _cordX, int _cordY)
        {
            RefTerrain = _RefTerrain;
            CordX = _cordX;
            CordY = _cordY;
        }

        public void AjouterASupprimerFourmi(Fourmi NouvelleFourmi)
        {
            FourmisASupprimer.Add(NouvelleFourmi);
        }
        public void AjouterEnAjoutFourmi(Fourmi NouvelleFourmi)
        {
            FourmisEnAjout.Add(NouvelleFourmi);
        }

        public void AjouterFourmi()
        {
            fourmis.AddRange(fourmisEnAjout);
            fourmisEnAjout.Clear();
        }

        public void SupprimerFourmi()
        {

            Fourmis.RemoveAll(fourmi => FourmisASupprimer.Contains(fourmi));
            FourmisASupprimer.Clear();
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

        public List<Case> CasesAdjacentes()
        {
            List<Case> casesAdjacentes = new List<Case>();
            if (this.CordX > 0)
            {
                casesAdjacentes.Add(RefTerrain.Cases[this.CordX - 1, this.CordY]);
            }
            if (this.CordX < (RefTerrain.Cases.GetLength(0) - 1))
            {
                casesAdjacentes.Add(RefTerrain.Cases[this.CordX + 1, this.CordY]);
            }
            if (this.CordY > 0)
            {
                casesAdjacentes.Add(RefTerrain.Cases[this.CordX, this.CordY - 1]);
            }
            if (this.CordY < (RefTerrain.Cases.GetLength(1) - 1))
            {
                casesAdjacentes.Add(RefTerrain.Cases[this.CordX, this.CordY + 1]);
            }

            return casesAdjacentes;
        }

        // FONCTION UPDATE => 
        // lance le update pour les fourmis standard
        public virtual void MiseAjour()
        {
            foreach(Fourmi fourmi in Fourmis)
            {
                fourmi.MiseAjour();
            }
        }

        // FONCTION DRAW CASE 
        // DESSINE FOURMI SI CONTIENT FOURMI
        public virtual void draw()
        {

        }
    }
}
