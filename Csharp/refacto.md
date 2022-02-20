Changer le chemin du fichier ./ en C:/

- Rennomage des variables :

    number1 => numberOfSales
    number2 => numberOfSoldItems
    number3 => totalSellsAmount
    number4 => averageAmountSales
    number5 => averageItemPrice
    line1 => headerCSV
    headerString => headerTitles
    otherLines => dataLines
    etc....

- Externalisation des valeurs : 

    Creation enum Commande avec 3 valeurs "print", "report" et "unknown"
    Creation filePath contenant le lien vers le fichier
    Creation d'une classe Constant 
    
- Création du Parser :
    
    Parser : interface 
    CSVParser : qui s'occupe de lire un fichier CSV

- Refacto possible pas fait:
    
    Externaliser tout l'affichage dans une classe externe qui gerera
    le pattern d'affichage Puis créer une interface qui s'occupe d'afficher ou envoyer le output,
    Dans notre cas elle sera implémentée pour afficher dans la console.

- Correction bug number of clients, ajustement GoldenMaster


## Tests

Le test couvre 97% mais on a pas implémenté tous les tests, l'esentiel est testé ;)

    ParserTest "input"
    Affichage *GoldenMaster* "output"
    CommandExecutorTest "traitement" => Incomplet mais l'idée c'est de check toutes les méthodes publiques