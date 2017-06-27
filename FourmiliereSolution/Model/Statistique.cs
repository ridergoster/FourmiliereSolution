using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class Statistique: Observeur
    {
        private int nombreFourmis = 0;
        public int NombreFourmis
        {
            get => nombreFourmis;
            set
            {
                nombreFourmis = value;
                OnPropertyChanged("NombreFourmis");
            }
        }

        private int nombreTours;
        public int NombreTours
        {
            get => nombreTours;
            set
            {
                nombreTours = value;
                OnPropertyChanged("NombreTours");
            }
        }

        private int nombreNourriture;
        public int NombreNourriture
        {
            get => nombreNourriture;
            set
            {
                nombreNourriture = value;
                OnPropertyChanged("NombreNourriture");
            }
        }

        private CaseAbstrait caseSelect;

        public CaseAbstrait CaseSelect
        {
            get { return caseSelect; }
            set
            {
                caseSelect = value;
                OnPropertyChanged("CaseSelect");
            }
        }

        public Terrain RefTerrain { get; set; }
  
        public Statistique(Terrain _refTerrain)
        {
            RefTerrain = _refTerrain;
        }

        public void MiseAjour()
        {
            int nbFourmis = 0;
            foreach (CaseAbstrait caseAbs in RefTerrain.Cases)
            {
                nbFourmis += caseAbs.Fourmis.Count();
            }
            NombreFourmis = nbFourmis;

            NombreTours = RefTerrain.NbTours;

            if (CaseSelect != null)
            {
                int nbNourriture = 0;
                if(CaseSelect is CaseNourriture)
                {
                    nbNourriture = ((CaseNourriture)CaseSelect).Nourriture.Poids;
                }
                else if(CaseSelect is CaseFourmiliere)
                {
                    nbNourriture = ((CaseFourmiliere)CaseSelect).Fourmiliere.NombreNourritures;
                }
                foreach(Fourmi Fourmi in CaseSelect.Fourmis)
                {
                    if(Fourmi.StrategieFourmi is StrategieRetourMaison)
                    {
                        nbNourriture++;
                    }
                }
                NombreNourriture = nbNourriture;
            }
        }
    }
}
