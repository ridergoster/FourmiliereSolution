using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class FabriqueGeneral
    {
        public Terrain GenererTerrain(int number)
        {
            return new Terrain(number);
        }
        public void PopulerTerrain(Terrain terrain, int number)
        {
            terrain.Cases = new CaseAbstrait[number, number];
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    terrain.Cases[i, j] = new CaseNormal(terrain, i, j);
                }
            }
        }

        public void AjouterFourmiliere(Terrain terrain, int posFX, int posFY)
        {
            CaseFourmiliere caseFourmiliere = new CaseFourmiliere(terrain, posFX, posFY);
            terrain.Cases[posFX, posFY] = caseFourmiliere;
            caseFourmiliere.Fourmiliere = new Fourmiliere(terrain.Cases[posFX, posFY]);
        }

        public void GenererPremiereFourmis(Terrain terrain, int posFX, int posFY, int nbFourmis)
        {
            CaseFourmiliere caseFourmiliere = (CaseFourmiliere)terrain.Cases[posFX, posFY];
            for(int i = 0; i < nbFourmis; i++)
            {
                caseFourmiliere.Fourmis.Add(new Fourmi(caseFourmiliere));
            }
        }
    }
}
