using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Fourmi
    {
        // CONTIENT REFERENCE SUR LA CASE
        private Case RefCase { get; set; }
        // CONTIENT UN NB VIE
        private int Vie { get; set; }
        // CONTIENT BOOL MANGER
        private bool Mange { get; set; }

        public Fourmi(Case _RefCase, bool _Mange)
        {
            Random Hasard = new Random();

            RefCase = _RefCase;
            this.Vie = Hasard.Next(10, 100);
            Mange = _Mange;
        }

        // FONCTION UPDATE => via stratégie cherche selon les case adjacentes celle avec 
        // le plus de phéromone maison ou nourriture
        public void UpdateFourmi() { }
    }
}
