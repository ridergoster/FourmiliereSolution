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
        }
    }
}
