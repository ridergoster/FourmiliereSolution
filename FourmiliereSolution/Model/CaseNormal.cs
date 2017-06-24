using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public class CaseNormal: CaseAbstrait
    {
        public CaseNormal(Terrain _RefTerrain, int _cordX, int _cordY) : base(_RefTerrain, _cordX, _cordY)
        {
        }

        public override bool ContientNourriture()
        {
            return false;
        }

        public override bool ContientFourmiliere()
        {
            return false;
        }
    }
}
