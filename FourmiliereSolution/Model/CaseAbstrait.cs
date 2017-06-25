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

        public void SupprimerPheromoneMaison(int valeur)
        {
            if (PheromoneMaison > valeur)
            {
                PheromoneMaison -= valeur;
            }
            else
            {
                PheromoneMaison = 0;
            } 
        }

        public void AjouterPheromoneNourriture(int value)
        {
            PheromoneNourriture += value;
        }

        public void SupprimerPheromoneNourriture(int valeur)
        {
            if (PheromoneNourriture > valeur)
            {
                PheromoneNourriture -= valeur;
            }
            else
            {
                PheromoneNourriture = 0;
            }
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
            foreach(Fourmi fourmi in Fourmis)
            {
                fourmi.MiseAjour();
            }
            //SupprimerPheromoneMaison(1);
            //SupprimerPheromoneNourriture(1);
        }

        public CaseNormal AdapteurNormal()
        {
            CaseNormal CaseNormal = new CaseNormal(this.RefTerrain, this.CordX, this.CordY);
            CaseNormal.FacteurPheromoneMaison = this.FacteurPheromoneMaison;
            CaseNormal.FacteurPheromoneNourriture = this.FacteurPheromoneNourriture;
            CaseNormal.PheromoneMaison = this.PheromoneMaison;
            CaseNormal.PheromoneNourriture = this.PheromoneNourriture;
            CaseNormal.Fourmis = this.Fourmis;
            CaseNormal.FourmisASupprimer = this.FourmisASupprimer;
            CaseNormal.FourmisEnAjout = this.FourmisEnAjout;
            foreach (Fourmi fourmi in CaseNormal.Fourmis) fourmi.RefCase = CaseNormal;
            foreach (Fourmi fourmi in CaseNormal.FourmisEnAjout) fourmi.RefCase = CaseNormal;
            CaseNormal.nbTours = this.nbTours;
            return CaseNormal;
        }

        public CaseNourriture AdapteurNourriture(int poids)
        {
            CaseNourriture CaseNourriture = new CaseNourriture(this.RefTerrain, this.CordX, this.CordY);
            CaseNourriture.FacteurPheromoneMaison = this.FacteurPheromoneMaison;
            CaseNourriture.FacteurPheromoneNourriture = this.FacteurPheromoneNourriture;
            CaseNourriture.PheromoneMaison = this.PheromoneMaison;
            CaseNourriture.PheromoneNourriture = this.PheromoneNourriture;
            CaseNourriture.Fourmis = this.Fourmis;
            CaseNourriture.FourmisASupprimer = this.FourmisASupprimer;
            CaseNourriture.FourmisEnAjout = this.FourmisEnAjout;
            foreach (Fourmi fourmi in CaseNourriture.Fourmis) fourmi.RefCase = CaseNourriture;
            foreach (Fourmi fourmi in CaseNourriture.FourmisEnAjout) fourmi.RefCase = CaseNourriture;
            CaseNourriture.nbTours = this.nbTours;
            CaseNourriture.Nourriture = new Nourriture(CaseNourriture, poids);
            return CaseNourriture;
        }

        public CaseFourmiliere AdapteurFourmiliere(int nbFourmis)
        {
            CaseFourmiliere CaseFourmiliere = new CaseFourmiliere(this.RefTerrain, this.CordX, this.CordY);
            CaseFourmiliere.FacteurPheromoneMaison = this.FacteurPheromoneMaison;
            CaseFourmiliere.FacteurPheromoneNourriture = this.FacteurPheromoneNourriture;
            CaseFourmiliere.PheromoneMaison = this.PheromoneMaison;
            CaseFourmiliere.PheromoneNourriture = this.PheromoneNourriture;
            CaseFourmiliere.Fourmis = this.Fourmis;
            CaseFourmiliere.FourmisASupprimer = this.FourmisASupprimer;
            CaseFourmiliere.FourmisEnAjout = this.FourmisEnAjout;
            foreach (Fourmi fourmi in CaseFourmiliere.Fourmis) fourmi.RefCase = CaseFourmiliere;
            foreach (Fourmi fourmi in CaseFourmiliere.FourmisEnAjout) fourmi.RefCase = CaseFourmiliere;
            CaseFourmiliere.nbTours = this.nbTours;
            CaseFourmiliere.Fourmiliere = new Fourmiliere(CaseFourmiliere, nbFourmis);
            return CaseFourmiliere;
        }

    }
}
