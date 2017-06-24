using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public abstract class CaseAbstrait
    {
        public int CordX { get; set; }
        public int CordY { get; set; }
        // CONTIENT UNE REF SUR LE TERRAIN
        public Terrain RefTerrain { get; set; }
        // CONTIENT UN NB DE PHEROMONE "MAISON"
        public int PheromoneMaison { get; set; } = 0;
        public int PheromoneNourriture { get; set; } = 0;
        public int FacteurPheromoneMaison { get; set; } = 4;
        public int FacteurPheromoneNourriture { get; set; } = 6;
        // CONTIENT UN NB DE PHEROMONE "NOURRITURE"
        public int nbTours { get; set; } = 0;
        // CONTIENT UN ARRAY DE FOURMI SUR LA CASE
        public List<Fourmi> Fourmis { get; set; } = new List<Fourmi>();
        public List<Fourmi> FourmisEnAjout { get; set; } = new List<Fourmi>();
        public List<Fourmi> FourmisASupprimer { get; set; } = new List<Fourmi>();

        public CaseAbstrait(Terrain _RefTerrain, int _cordX, int _cordY)
        {
            RefTerrain = _RefTerrain;
            CordX = _cordX;
            CordY = _cordY;
        }

        public void AjouterPheromoneMaison(int value)
        {
            PheromoneMaison += value;
        }

        public void SupprimerPheromoneMaison()
        {
            if (PheromoneMaison > 0) PheromoneMaison--;
        }

        public void AjouterPheromoneNourriture(int value)
        {
            PheromoneNourriture += value;
        }

        public void SupprimerPheromoneNourriture()
        {
            if (PheromoneNourriture > 0) PheromoneNourriture--;
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
            Fourmis.AddRange(FourmisEnAjout);
            FourmisEnAjout.Clear();
        }
        public void SupprimerFourmi()
        {

            Fourmis.RemoveAll(fourmi => FourmisASupprimer.Contains(fourmi));
            FourmisASupprimer.Clear();
        }

        public List<CaseAbstrait> CasesAdjacentes()
        {
            List<CaseAbstrait> casesAdjacentes = new List<CaseAbstrait>();
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

        public abstract bool ContientNourriture();
        public abstract bool ContientFourmiliere();

        public bool ContientFourmis()
        {
            return Fourmis.Count > 0;
        }
        public virtual void MiseAjour()
        {
            nbTours++;
            SupprimerPheromoneMaison();
            SupprimerPheromoneNourriture();
            if (this is CaseNourriture && this.ContientNourriture() == false || this is CaseFourmiliere && this.ContientFourmiliere() == false)
            {
                // transforme en case normal
            }
            foreach(Fourmi fourmi in Fourmis)
            {
                fourmi.MiseAjour();
            }
        }
    }
}
