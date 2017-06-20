using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourmiliereSolution.Model
{
    class Case
    {
        // CONTIENT UNE REF SUR LE TERRAIN 
        // CONTIENT UN NB DE PHEROMONE "MAISON"
        // CONTIENT UN NB DE PHEROMONE "NOURRITURE"
        // CONTIENT UN ARRAY DE FOURMI SUR LA CASE
        // CONTIENT UNE NOURRITURE SUR LA CASE ==> class herit (caseNourriture)?
        // CONTIENT UNE FOURMILIERE SUR LA CASE ==> class herit (caseFourmiliere) ?

        // FONCTION : CONTIENT NOURRITURE ?
        // FONCTION : CONTIENT FOURMILIERE ?
        // FONCTION : AJOUTER / SUPP FOURMI && SET / GET FOURMILIST
        // FONCTION : AJOUTER / SUPP PHEROMONE MAISON NOURRITURE && SET / GET
        // FONCTION : SET / GET NOURRITURE   
        // FONCTION : SET / GET FOURMILIERE ==> herit ?

        // FONCTION UPDATE => 
        //      1. lance le update pour toute les fourmi pour les faire bouger
        //      2. lance le update pour bouffe / fourmilière si class herit
        //      3. lance fonction draw ? ou depuis le jeux ?
        // FONCTION DRAW CASE 
    }
}
