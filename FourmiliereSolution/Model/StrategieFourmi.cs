using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    public interface StrategieFourmi
    {
        CaseAbstrait MiseAjour(Fourmi fourmi);
    }
}
