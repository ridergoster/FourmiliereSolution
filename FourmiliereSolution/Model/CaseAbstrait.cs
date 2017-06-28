using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public abstract class CaseAbstrait: Observeur
    {
        public int CordX { get; set; }
        public int CordY { get; set; }
        // CONTIENT UNE REF SUR LE TERRAIN
        public Terrain RefTerrain { get; set; }
        // CONTIENT UN NB DE PHEROMONE "MAISON"
        private int pheromoneMaison = 0;
        public int PheromoneMaison {
            get => pheromoneMaison;
            set {
                pheromoneMaison = value;
                OnPropertyChanged("PheromoneMaison");
            }
        }
        private int pheromoneNourriture = 0;
        public int PheromoneNourriture
        {
            get => pheromoneNourriture;
            set
            {
                pheromoneNourriture = value;
                OnPropertyChanged("PheromoneNourriture");
            }
        }        // CONTIENT UN NB DE PHEROMONE "NOURRITURE"
        public int NbTours { get; set; } = 0;
        // CONTIENT UN ARRAY DE FOURMI SUR LA CASE
        private ObservableCollection<Fourmi> fourmis = new ObservableCollection<Fourmi>();
        public ObservableCollection<Fourmi> Fourmis
        {
            get => fourmis;
            set
            {
                fourmis = value;
                OnPropertyChanged("Fourmis");
            }
        }
        public List<Fourmi> FourmisEnAjout { get; set; } = new List<Fourmi>();
        public List<Fourmi> FourmisASupprimer { get; set; } = new List<Fourmi>();

        public CaseAbstrait(Terrain _RefTerrain, int _cordX, int _cordY)
        {
            RefTerrain = _RefTerrain;
            CordX = _cordX;
            CordY = _cordY;
        }

        public void AjouterPheromoneMaison(int valeur)
        {
            PheromoneMaison += valeur;
        }

        public void SupprimerPheromoneMaison(int valeur)
        {
            PheromoneMaison = PheromoneMaison > valeur ? PheromoneMaison - valeur : 0;
        }

        public void AjouterPheromoneNourriture(int valeur)
        {
            PheromoneNourriture += valeur;
        }

        public void SupprimerPheromoneNourriture(int valeur)
        {
            PheromoneNourriture = PheromoneNourriture > valeur ? PheromoneNourriture - valeur : 0;
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
            foreach(Fourmi Fourmi in FourmisEnAjout)
            {
                Fourmis.Add(Fourmi);
            }
            FourmisEnAjout.Clear();
        }
        public void SupprimerFourmi()
        {
            foreach (Fourmi Fourmi in FourmisASupprimer)
            {
                Fourmis.Remove(Fourmi);
            }
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
            NbTours++;
            foreach(Fourmi fourmi in Fourmis)
            {
                fourmi.RefCase = this;
                if (fourmi.Vie == 0)
                {
                    FourmisASupprimer.Add(fourmi);
                }
                else
                {
                    fourmi.MiseAjour();
                }
            }
        }

        public CaseNormal AdapteurNormal()
        {
            CaseNormal CaseNormal = new CaseNormal(this.RefTerrain, this.CordX, this.CordY);
            CaseNormal.PheromoneMaison = this.PheromoneMaison;
            CaseNormal.PheromoneNourriture = this.PheromoneNourriture;
            CaseNormal.Fourmis = this.Fourmis;
            CaseNormal.FourmisASupprimer = this.FourmisASupprimer;
            CaseNormal.FourmisEnAjout = this.FourmisEnAjout;
            foreach (Fourmi fourmi in CaseNormal.Fourmis) fourmi.RefCase = CaseNormal;
            foreach (Fourmi fourmi in CaseNormal.FourmisEnAjout) fourmi.RefCase = CaseNormal;
            CaseNormal.NbTours = this.NbTours;
            return CaseNormal;
        }

        public CaseNourriture AdapteurNourriture(int poids)
        {
            CaseNourriture CaseNourriture = new CaseNourriture(this.RefTerrain, this.CordX, this.CordY);
            CaseNourriture.PheromoneMaison = this.PheromoneMaison;
            CaseNourriture.PheromoneNourriture = this.PheromoneNourriture;
            CaseNourriture.Fourmis = this.Fourmis;
            CaseNourriture.FourmisASupprimer = this.FourmisASupprimer;
            CaseNourriture.FourmisEnAjout = this.FourmisEnAjout;
            foreach (Fourmi fourmi in CaseNourriture.Fourmis) fourmi.RefCase = CaseNourriture;
            foreach (Fourmi fourmi in CaseNourriture.FourmisEnAjout) fourmi.RefCase = CaseNourriture;
            CaseNourriture.NbTours = this.NbTours;
            CaseNourriture.Nourriture = new Nourriture(CaseNourriture, poids);
            return CaseNourriture;
        }

        public CaseFourmiliere AdapteurFourmiliere(int nbFourmis)
        {
            CaseFourmiliere CaseFourmiliere = new CaseFourmiliere(this.RefTerrain, this.CordX, this.CordY);
            CaseFourmiliere.PheromoneMaison = this.PheromoneMaison;
            CaseFourmiliere.PheromoneNourriture = this.PheromoneNourriture;
            CaseFourmiliere.Fourmis = this.Fourmis;
            CaseFourmiliere.FourmisASupprimer = this.FourmisASupprimer;
            CaseFourmiliere.FourmisEnAjout = this.FourmisEnAjout;
            foreach (Fourmi fourmi in CaseFourmiliere.Fourmis) fourmi.RefCase = CaseFourmiliere;
            foreach (Fourmi fourmi in CaseFourmiliere.FourmisEnAjout) fourmi.RefCase = CaseFourmiliere;
            CaseFourmiliere.NbTours = this.NbTours;
            CaseFourmiliere.Fourmiliere = new Fourmiliere(CaseFourmiliere, nbFourmis);
            return CaseFourmiliere;
        }

    }
}
