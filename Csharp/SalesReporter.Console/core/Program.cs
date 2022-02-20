using SalesReporterKata;
using static Program.Commands;
using static SalesReporterKata.Constants;

public static class Program
{
    public enum Commands
    {
        print,
        report,
        unknown
    }

    private static string ExtractCommand(string[] args)
    {
        return args.Length > 0 ? args[0] : unknown.ToString();
    }

    private static string ExtractFile(string[] args)
    {
        string filePath = "C:/data.csv";
        return args.Length >= 2 ? args[1] : filePath;
    }

    public static void Main(string[] args)
    {
        ConsoleOutputCreator consoleCreator = new ConsoleOutputCreator();
        Console.WriteLine(consoleCreator.Title);
        string command = ExtractCommand(args);
        string file = ExtractFile(args);
        CSVParser parser = new CSVParser(file);


        if (command == print.ToString())
        {
            //get the header line 
            var columnNames = parser.parseHeader();
            var dataLines = parser.parseData();
            PrintData printData = new PrintData();
            var dataToDisplay = printData.CreateDisplayer(columnNames, dataLines);
            Console.Write(dataToDisplay);
        }
        else if (command == report.ToString())
        {
            //get all the lines without the header in the first line  
            var otherLines = parser.parseData();
            //declare variables for our conters  
            int numberOfsales = 0, numberOfSoldItems = 0;
            double averageAmountSale = 0.0, averageItemPrice = 0.0, totalSellsAmount = 0;
            HashSet<string> clients = new HashSet<string>();
            DateTime last = DateTime.MinValue;
            //do the counts for each line  
            foreach (var line in otherLines)
            {
                //get the cell values for the line  
                var cells = line.Split(',');
                numberOfsales++; //increment the total of sales  
                //to count the number of clients, we put only distinct names in a hashset //then we'll count the number of entries
                if (!clients.Contains(cells[1])) clients.Add(cells[1]);
                numberOfSoldItems += int.Parse(cells[2]); //we sum the total of items sold here  
                totalSellsAmount += double.Parse(cells[3]); //we sum the amount of each sell  
                //we compare the current cell date with the stored one and pick the higher last = DateTime.Parse(cells[4]) > last ? DateTime.Parse(cells[4]) : last;  
            }

            //we compute the average basket amount per sale  
            averageAmountSale = Math.Round(totalSellsAmount / numberOfsales, 2);
            //we compute the average item price sold  
            averageItemPrice = Math.Round(totalSellsAmount / numberOfSoldItems, 2);
            Console.WriteLine($"+{new String('-', 45)}+");
            Console.WriteLine($"| {NUMBER_OF_SALES.PadLeft(30)} | {numberOfsales.ToString().PadLeft(10)} |");
            Console.WriteLine($"| {NUMBER_OF_CLIENTS.PadLeft(30)} | {clients.Count.ToString().PadLeft(10)} |");
            Console.WriteLine($"| {TOTAL_ITEMS_SOLD.PadLeft(30)} | {numberOfSoldItems.ToString().PadLeft(10)} |");
            Console.WriteLine(
                $"| {TOTAL_SALES_AMOUNT.PadLeft(30)} | {Math.Round(totalSellsAmount, 2).ToString().PadLeft(10)} |");
            Console.WriteLine($"| {AVERAGE_AMOUNT_SALE.PadLeft(30)} | {averageAmountSale.ToString().PadLeft(10)} |");
            Console.WriteLine($"| {AVERAGE_ITEM_PRICE.PadLeft(30)} | {averageItemPrice.ToString().PadLeft(10)} |");
            Console.WriteLine($"+{new String('-', 45)}+");
        }
        else
        {
            PrintErrorLog logError = new PrintErrorLog();
            Console.WriteLine(logError.Create());
        }
    }
}