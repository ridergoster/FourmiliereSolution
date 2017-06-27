using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution
{
    public class AProposViewModel : Observeur
    {
        public string Copyright { get { return "\n\rCette application est sous aucune licence"; } }
        public string DateApplication { get { return DateTime.Now.ToString();  } }
        public string Auteur {  get { return "\n\rVincent Kocupyr\n\rBaptiste Deléris\n\rRémi Ollivier"; } }
    }
}
