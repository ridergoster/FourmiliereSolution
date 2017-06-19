using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Timers;
using System.Windows.Input;

namespace FourmiliereSolution
{
    public class FourmiliereViewModel : ViewModelBase
    {

        private int dimX;
        private int dimY;

        public int DimX
        {
            get { return dimX; }
            set
            {
                dimX = value;
                OnPropertyChanged("DimX");
            }
        }

        public int DimY
        {
            get { return dimY; }
            set
            {
                dimY = value;
                OnPropertyChanged("DimY");
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

        public FourmiliereViewModel(int _DimX = 20, int _DimY = 20)
        {
            DimX = _DimX;
            DimY = _DimY;
            TitreApplication = "Fourmilière MOC 2";
            VitesseExec = 200;
            FourmisList = new ObservableCollection<Fourmi>();
            FourmisList.Add(new Fourmi("Alain", DimX, DimY));
            FourmisList.Add(new Fourmi("Bernard", DimX, DimY));
            FourmisList.Add(new Fourmi("Claude", DimX, DimY));
            FourmisList.Add(new Fourmi("Dorian", DimX, DimY));
            FourmisList.Add(new Fourmi("Emilien", DimX, DimY));
            FourmisList.Add(new Fourmi("Francis", DimX, DimY));
        }

        internal void move()
        {
            foreach (var fourmi in FourmisList)
            {
                fourmi.AvanceHazard(DimX, DimY);
            }
        }


        internal void tourSuivant()
        {
            stop();
            move();
        }

        internal void avance()
        {
            Runnin = true;

            while(Runnin)
            {
                Thread.Sleep(VitesseExec);
                move();
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
                FourmisList.Add(new Fourmi("N°" + (FourmisList.Count + 1), DimX, DimY));
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