using System;
using System.Collections.ObjectModel;

namespace FourmiliereSolution
{
    public class Fourmi
    {

        static Random Hazard = new Random();
        public string Nom { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public ObservableCollection<Etape> EtapesList { get; set; }

        public Fourmi(string _Nom = "Anonyme", int _X = 10, int _Y = 10)
        {
            Nom = _Nom;
            X = Hazard.Next(_X);
            Y = Hazard.Next(_Y);

            EtapesList = new ObservableCollection<Etape>();
            int nbX = Hazard.Next(10);
            for (int i = 0; i < nbX; i++)
            {
                EtapesList.Add(new Etape());
            }
        }

        public override string ToString()
        {
            return "(Fourmi): " + Nom;
        }

        public void AvanceHazard(int dimX, int dimY)
        {
            int newX = X + Hazard.Next(3) - 1;
            int newY = Y + Hazard.Next(3) - 1;
            if (newX >= 0 && newX < dimX) X = newX;
            if (newY >= 0 && newY < dimY) Y = newY;        
        }
    }
}