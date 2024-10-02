# P_323-PlotThatLines

## 1. Titre du projet

Plot that lines !

## 2. Description

Le but de ce projet est d'utiliser les connaissances acquises dans le module 323 en affichant sur un graphique 3 différentes monnaies.

## 3. Table des matières

- [Requirements](#4-requierements)
- [Installation](#5-installation)
- [Utilisation](#6-utilisation)
- [Planification](#7-planification)
- [Journal de travail](#8-journal-de-travail)
- [Technologie Utilisé](#9-technologie-utilisé)
- [Tests](#10-tests)
- [IA](#11-ia)
- [Conclusion](#12-conclusion)
- [Auteur(s)](#13-auteurs)
- [Licences](#14-licence)

## 4. Requierements

Vous avez besoin :

- C# : 12
- ScottPlot : 3

## 5. Installation

- Si vous n'avez pas Visual Studio 2022, installez-le à cette [adresse](https://visualstudio.microsoft.com/fr/downloads/)
- `git clone https://github.com/quemet/P_323-PlotThatLines.git`

## 6. Utilisation

- Aller dans le dossier **P_323-PlotThatLines**
- Encore une fois, aller dans le dossier **P_323-PlotThatLines**
- Double cliquer sur le fichier nommé **P_323-PlotThatLines.sln**
- Cliquer sur l'icône de triangle vert pour exécuter le code

## 7. Planification

Pour voir la planification, se rendre dans le dossier racine de ce projet et ouvrez le fichier nommé **JNLTRAV-QueMetroz.xlsm**, aller dans l'onglet **Planification**

## 8. Journal de travail

Pour voir le journal de travail, se rendre dans le dossier racine de ce projet et ouvrez le fichier nommé **JNLTRAV-QueMetroz.xlsm**, aller dans l'onglet **Journal de Travail**

## 9. Technologie Utilisé

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white) : v12<br>
![Markdown](https://img.shields.io/badge/markdown-%23000000.svg?style=for-the-badge&logo=markdown&logoColor=white) : v.0.30<br>

![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white) : v17.11.3<br>
![Visual Studio Code](https://img.shields.io/badge/Visual%20Studio%20Code-0078d7.svg?style=for-the-badge&logo=visual-studio-code&logoColor=white) : v1.9.31<br>

![Bitcoin](https://img.shields.io/badge/Bitcoin-000?style=for-the-badge&logo=bitcoin&logoColor=white)<br>
![Ethereum](https://img.shields.io/badge/Ethereum-3C3C3D?style=for-the-badge&logo=Ethereum&logoColor=white)

## 10. Tests

### Test Unitaire

| Fonction                | NmbTest | Résultat |
| ----------------------- | ------- | -------- |
| ReadPath                | 3       | Success  |
| RefreshDate             | 2       | Success  |
| ReturnCorrectFormatDate | 3       | Success  |

### Test Manuel

| Scénario                                                                                            | Résultat |
| ----------------------------------------------------------------------------------------------------| -------- |
| Si la date d'entrée est changée, alors la date de fin ne peut être inférieure à celle-ci.           | Success  |
| Si on entre deux dates qui sont antérieures à une cryptomonnaie, la crypto ne s'affiche pas.        | Success  |
| Si on met une date antérieure de tous les cryptos, un quadrillage blanc doit apparaître.            | Success  |
| Si on démarre l'application, alors trois courbes doites être dessiné.                               | Success  |
| Si on démarre l'application, alors trois courbes de couleur différente doit être dessiné.           | Success  |
| Si on démarre l'application, alors une légende sur les 3 monnaies doit apparaître.                  | Success  |
| Si on clique sur le bouton submit, alors le graphique doit avoir 3 courbes.                         | Success  |
| Si on clique sur le bouton submit, alors le graphique doit avoir 3 courbes de couleurs différentes. | Success  |
| Si on clique sur le bouton submit, alors le graphique doit avoir 3 courbes avec une légende.        | Success  |
| Si on clique sur le bouton submit, alors l'application ne crache pas.                               | Success  |

## 11. IA

Je n'ai pas utilisé l'intelligence artificielle durant ce projet, à part pour la structure de mon fichier README.md où j'ai demandé à CHATGPT de me dire quelle est la structure de ce fichier de manière optimale.

## 12. Conclusion

Mon projet reépond aux cahier des charges et utilisables avec la realease de mon [repo github](https://github.com/quemet/P_323-PlotThatLines).

### Conclusion personelle

Le projet m'a semblé assez facile au début mais au fur et à mesure du projet le prof rajoute des choses dans le projet.

### Amélioration du projet

Les pistes d'améliorations du projet :

- Récupérer les données d'une API.
- Demander le nombre de cryptomonnaie à l'utilisateur.
- Demander quelle crypto le graphe doit être affiché.

## 13. Auteur(s)

**Auteur** : [Quentin Métroz](https://github.com/quemet)

## 14. Licence

Ce projet est sous licence [MIT](https://github.com/quemet/P_323-PlotThatLines/blob/main/LICENSE.md).
