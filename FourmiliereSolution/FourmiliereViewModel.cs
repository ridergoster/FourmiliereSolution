using FourmiliereSolution.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Timers;
using System.Windows.Input;

namespace FourmiliereSolution
{
    public class FourmiliereViewModel : ViewModelBase
    {

        private int dim;
        private Terrain terrain;
        public int Dim
        {
            get { return dim; }
            set
            {
                dim = value;
                OnPropertyChanged("Dim");
            }
        }

        public Terrain Terrain
        {
            get { return terrain; }
            set
            {
                terrain = value;
                OnPropertyChanged("Terrain");
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

        private ObservableCollection<Fourmi> fourmisList;

        public ObservableCollection<Fourmi> FourmisList
        {
            get { return fourmisList; }
            set
            {
                fourmisList = value;
                OnPropertyChanged("FourmisList");
            }
        }

        private Fourmi fourmiSelect;

        public Fourmi FourmiSelect
        {
            get { return fourmiSelect; }
            set
            {
                fourmiSelect = value;
                OnPropertyChanged("FourmiSelect");
            }
        }

        public bool Runnin { get; private set; }
        public int VitesseExec { get; set; }

        internal void supprimerFourmi()
        {
            FourmisList.Remove(FourmiSelect);
        }

        public FourmiliereViewModel(int _Dim = 50)
        {
            Dim = _Dim;
            Terrain = new Terrain(Dim);
            TitreApplication = "Fourmilière MOC 2";
            VitesseExec = 200;
            FourmisList = new ObservableCollection<Fourmi>();
            FourmisList.Add(new Fourmi("Alain", Dim, Dim));
            FourmisList.Add(new Fourmi("Bernard", Dim, Dim));
            FourmisList.Add(new Fourmi("Claude", Dim, Dim));
            FourmisList.Add(new Fourmi("Dorian", Dim, Dim));
            FourmisList.Add(new Fourmi("Emilien", Dim, Dim));
            FourmisList.Add(new Fourmi("Francis", Dim, Dim));
        }

        internal void move()
        {
            foreach (var fourmi in FourmisList)
            {
                fourmi.AvanceHazard(Dim, Dim);
            }
        }

        internal void loseLife()
        {
            ObservableCollection<Fourmi> fourmisToDeleteList = new ObservableCollection<Fourmi>() ;
            foreach (var fourmi in FourmisList)
            {
                if (fourmi.LoseHealth())
                    fourmisToDeleteList.Add(fourmi);
            }
            foreach (var fourmi in fourmisToDeleteList)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    FourmisList.Remove(fourmi);
                });
            }
            OnPropertyChanged("FourmisList");
        }


        internal void tourSuivant()
        {
            stop();
            Terrain.MiseAjour();
        }

        internal void avance()
        {
            Runnin = true;

            while(Runnin)
            {
                Thread.Sleep(VitesseExec);
                Terrain.MiseAjour();
            }
        }

        internal void stop()
        {
            Runnin = false;
        }

        internal void ajouterFourmi()
        {
            FourmisList.Add(new Fourmi("N°" + (FourmisList.Count + 1)));
        }

        internal void populateFourmi()
        {
            FourmisList.Add(new Fourmi("N°" + (FourmisList.Count + 1)));
            while (FourmisList[FourmisList.Count - 1].EtapesList.Count < 8)
            {
                FourmisList.Add(new Fourmi("N°" + (FourmisList.Count + 1), Dim, Dim));
            }
        }

        private void removeFourmis(object sender, KeyEventArgs e)
        {
            if (Key.Delete == e.Key)
            {
                int idx = FourmisList.IndexOf(FourmiSelect) + 1;
                FourmisList.Remove(FourmiSelect);
                FourmiSelect = idx < FourmisList.Count ? FourmisList[idx + 1] : null;
            }
        }
    }
}