using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution
{
    public class AProposViewModel : Observeur
    {
        public string Copyright { get { return "Machin"; } }
        public string DateApplication { get { return DateTime.Now.ToString();  } }
        public string Auteur {  get { return "Vincent Chose"; } }
    }
}
