#Fourmilière Solution

Projet de fourmilière en C#


stratégie fourmi:

(par défaut) chercher :
1 - IF case actuel contient nourriture
    1.1 - prendre 1 nourriture au paquet et passer en stratégie "rentrer"
    ELSE
    1.1 - poser 1 phéromone "maison" sur la case actuel
2 - IF case adjacente contient phéromone "nourriture"
    2.1 - aller sur la case avec le poids phéromone "nourriture" le + fort
    ELSE
    2.2 - aller sur une case au piffometre

rentrer :
1 - IF case actuel est la maison
    1.1 Rajouter 1 nourriture à la maison
    ELSE
    1.2 poser phéromone "nourriture" sur la CASE
2 - IF case adjacente contient phéromone "maison"
    2.1 - Aller sur la case avec le poids phéromone "maison" le + fort
    ELSE
    2.2 - aller sur une case au piffometre
