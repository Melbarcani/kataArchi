Changer le chemin du fichier ./ en C:/

Rennomage des variables :

    number1 => numberOfSales
    number2 => numberOfSoldItems
    number3 => totalSellsAmount
    number4 => averageAmountSales
    number5 => averageItemPrice
    line1 => headerCSV
    headerString => headerTitles
    otherLines => dataLines
    etc....

Externalisation des valeurs : 

    Creation enum Commande avec 3 valeurs "print", "report" et "uknown"
    Creation filePath contenant le lien vers le fichier
    Creation d'une classe Constant 
    
Création du Parser :
    
    Parser qui s'occupe de lire le fichier

Factoriser otherLines

Refacto possible :
    
    Externaliser tout l'affichage dans une classe externe qui gerera
    le pattern d'affichage

Correction number of clients, ajustement GoldenMaster

partie print constructeur d'affichage
partie report Objet result + constructeur d'affichage


