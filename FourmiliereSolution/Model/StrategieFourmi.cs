using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    interface StrategieFourmi
    {
        CaseAbstrait MiseAjour(CaseAbstrait refCase);
    }
}
