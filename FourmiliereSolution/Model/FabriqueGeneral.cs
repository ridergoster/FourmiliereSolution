using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class FabriqueGeneral
    {
        public static Random Hasard = new Random();

        public void AjouterFourmiliere(Terrain terrain, int posFX, int posFY, int nbFourmis)
        {
            CaseAbstrait refCase;
            if ((refCase = terrain.Cases[posFX, posFY]) is CaseNormal)
                terrain.Cases[posFX, posFY] = refCase.AdapteurFourmiliere(nbFourmis);
            terrain.Cases[posFX, posFY] = terrain.Cases[posFX, posFY].AdapteurFourmiliere(nbFourmis);
        }

        public void AjouterFourmiliereAuHasard(Terrain terrain, int tailleMax, int nbFourmis)
        {
            CaseAbstrait refCase;
            while (!((refCase = terrain.Cases[Hasard.Next(0, tailleMax), Hasard.Next(0, tailleMax)]) is CaseNormal))
            {
            }
            terrain.Cases[refCase.CordX, refCase.CordY] = refCase.AdapteurFourmiliere(nbFourmis);
        }

        public void AjouterNourritureAuHasard(Terrain terrain, int tailleMax, int poids)
        {
            CaseAbstrait refCase;
            while (!((refCase = terrain.Cases[Hasard.Next(0, tailleMax), Hasard.Next(0, tailleMax)]) is CaseNormal))
            {
            }
            terrain.Cases[refCase.CordX, refCase.CordY] = refCase.AdapteurNourriture(poids);
        }

        public void AjouterNourriture(Terrain terrain, int posFX, int posFY, int poids)
        {
            CaseAbstrait refCase;
            if ((refCase = terrain.Cases[posFX, posFY]) is CaseNormal)
                terrain.Cases[posFX, posFY] = refCase.AdapteurNourriture(poids);
            terrain.Cases[posFX, posFY] = terrain.Cases[posFX, posFY].AdapteurNourriture(poids);
        }

        public void GenererFourmis(Terrain terrain, int posX, int posY, int nbFourmis)
        {
            CaseAbstrait CaseAPopuler = terrain.Cases[posX, posY];
            for(int i = 0; i < nbFourmis; i++)
            {
                CaseAPopuler.Fourmis.Add(new Fourmi(CaseAPopuler));
            }
        }
    }
}
