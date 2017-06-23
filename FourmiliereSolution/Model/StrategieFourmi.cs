using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    interface StrategieFourmi
    {
        bool Manger { get; set; }
        bool Trigger { get; set; }
        Case MiseAjour(Case refCase);
    }
}
