using FourmiliereSolution.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Timers;
using System.Windows.Input;

namespace FourmiliereSolution
{
    public class MainViewModel : Observeur
    {

        private int dim;
        public int Dim
        {
            get { return dim; }
            set
            {
                dim = value;
                OnPropertyChanged("Dim");
            }
        }

        private Terrain terrain;
        public Terrain Terrain
        {
            get { return terrain; }
            set
            {
                terrain = value;
                OnPropertyChanged("Terrain");
            }
        }

        private Statistique statistique;
        public Statistique Statistique
        {
            get { return statistique; }
            set
            {
                statistique = value;
                OnPropertyChanged("Statistique");
            }
        }

        private string titreApplication;
        public string TitreApplication
        {
            get { return titreApplication; }
            set
            {
                titreApplication = value;
                OnPropertyChanged("TitreApplication");
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

        public bool Runnin { get; private set; }
        public int VitesseExec { get; set; }

        public MainViewModel(int _Dim = 50)
        {
            Dim = _Dim;
            Terrain = new Terrain(Dim);
            Statistique = new Statistique(Terrain);
            TitreApplication = "Fourmilière MOC 2";
            VitesseExec = 200;
        }

        internal void MiseAjour()
        {
            Terrain.MiseAjour();
            Statistique.MiseAjour();
           // Statistique = Statistique.GetStatistique();
        }
        internal void tourSuivant()
        {
            stop();
            MiseAjour();
        }

        internal void avance()
        {
            Runnin = true;

            while(Runnin)
            {
                Thread.Sleep(VitesseExec);
                MiseAjour();
            }
        }

        internal void stop()
        {
            Runnin = false;
        }
    }
}