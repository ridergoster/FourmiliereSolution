using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class StrategieRetourMaison : StrategieFourmi
    {
        public bool Manger { get; set; } = true;
        public bool Trigger { get; set; } = false;

        public Case update(Case refCase)
        {
            throw new NotImplementedException();
        }
    }
}
