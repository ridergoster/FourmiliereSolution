#Fourmilière Solution

Projet de fourmilière en C#


stratégie fourmi:

(par défaut) chercher :

1 - IF case actuel contient nourriture

&nbsp; &nbsp; &nbsp; &nbsp; 1.1 - prendre 1 nourriture au paquet et passer en stratégie "rentrer"

&nbsp; &nbsp; &nbsp; &nbsp; ELSE

&nbsp; &nbsp; &nbsp; &nbsp; 1.2 - poser 1 phéromone "maison" sur la case actuel

2 - IF case adjacente contient phéromone "nourriture"

&nbsp; &nbsp; &nbsp; &nbsp; 2.1 - aller sur la case avec le poids phéromone "nourriture" le + fort

&nbsp; &nbsp; &nbsp; &nbsp; ELSE

&nbsp; &nbsp; &nbsp; &nbsp; 2.2 - aller sur une case au piffometre

rentrer :

1 - IF case actuel est la maison

&nbsp; &nbsp; &nbsp; &nbsp; 1.1 Rajouter 1 nourriture à la maison

&nbsp; &nbsp; &nbsp; &nbsp; ELSE

&nbsp; &nbsp; &nbsp; &nbsp; 1.2 poser phéromone "nourriture" sur la CASE

2 - IF case adjacente contient phéromone "maison"

&nbsp; &nbsp; &nbsp; &nbsp; 2.1 - Aller sur la case avec le poids phéromone "maison" le + fort

&nbsp; &nbsp; &nbsp; &nbsp; ELSE

&nbsp; &nbsp; &nbsp; &nbsp; 2.2 - aller sur une case au piffometre
