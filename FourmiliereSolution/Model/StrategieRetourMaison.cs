using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class StrategieRetourMaison : StrategieFourmi
    {
        public bool Manger { get; } = true;

        public void update()
        {
            throw new NotImplementedException();
        }
    }
}
