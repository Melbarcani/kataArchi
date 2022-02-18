using SalesReporterKata;
using static Program.Commands;
using static SalesReporterKata.Constant;

public static class Program
{
	public enum Commands
	{
		print,
		report,
		unknown
	}
	//lots of comments!
	public static void Main(string[] args)
	{

		string filePath = "C:/data.csv";
		
		//add a title to our app  
		Console.WriteLine(SALES_VIEWER_TITLE);
		//extract the command name from the args  
		string command = args.Length > 0 ? args[0] : unknown.ToString();  
		string file = args.Length >= 2 ? args[1] : filePath;
		Parser parser = new Parser(file);
		//read content of our data file  
		//[2012-10-30] rui : actually it only works with this file, maybe it's a good idea to pass file //name as parameter to this app later?  
		//if command is print  
		if (command == print.ToString())  
		{  
			 //get the header line  
			 string headerCSV = parser.parseHeader();  
			 //get other content lines  
			 var dataLines = parser.parseData();
			 foreach (string line in dataLines)
			 {
				 Console.WriteLine(line);
			 }
			 var columnInfos = new List<(int index, int size, string name)>();
			 //build the header of the table with column names from our data file  
			 int i = 0;
			 foreach (var columName in headerCSV.Split(','))
			 {
				 columnInfos.Add((i++, columName.Length, columName));
			 }

			 var headerTitles  = String.Join(
				 " | ", 
				 columnInfos.Select(x=>x.name).Select(
					 (val,ind) => val.PadLeft(16)));
			 Console.WriteLine("+" + new String('-', headerTitles.Length + 2) + "+");
			 Console.WriteLine("| " + headerTitles + " |");
			 Console.WriteLine("+" + new String('-', headerTitles.Length +2 ) + "+");

			 //then add each line to the table  
			 foreach (string line in dataLines)  
			 { 
				 //extract columns from our csv line and add all these cells to the line  
				 var cells = line.Split(',');
				 var tableLine  = String.Join(
		            " | ", 
		            
		            line.Split(',').Select(
			            (val,ind) => val.PadLeft(16)));
	            Console.WriteLine($"| {tableLine} |");
			 } 
			 Console.WriteLine("+" + new String('-', headerTitles.Length+2) + "+");

			// if command is report
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
			 { //get the cell values for the line  
	 			var cells = line.Split(',');  
	 			numberOfsales++;//increment the total of sales  
	 			//to count the number of clients, we put only distinct names in a hashset //then we'll count the number of entries
	 			if (!clients.Contains(cells[1])) clients.Add(cells[1]);  
	 			numberOfSoldItems += int.Parse(cells[2]);//we sum the total of items sold here  
	 			totalSellsAmount += double.Parse(cells[3]);//we sum the amount of each sell  
	 			//we compare the current cell date with the stored one and pick the higher last = DateTime.Parse(cells[4]) > last ? DateTime.Parse(cells[4]) : last;  
			 } 
			 //we compute the average basket amount per sale  
			 averageAmountSale = Math.Round(totalSellsAmount / numberOfsales,2);  
			 //we compute the average item price sold  
			 averageItemPrice = Math.Round(totalSellsAmount / numberOfSoldItems,2);  
			 Console.WriteLine($"+{new String('-',45)}+");
			 Console.WriteLine($"| {NUMBER_OF_SALES.PadLeft(30)} | {numberOfsales.ToString().PadLeft(10)} |");
			 Console.WriteLine($"| {NUMBER_OF_CLIENTS.PadLeft(30)} | {clients.Count.ToString().PadLeft(10)} |");
			 Console.WriteLine($"| {TOTAL_ITEMS_SOLD.PadLeft(30)} | {numberOfSoldItems.ToString().PadLeft(10)} |");
			 Console.WriteLine($"| {TOTAL_SALES_AMOUNT.PadLeft(30)} | {Math.Round(totalSellsAmount,2).ToString().PadLeft(10)} |");
			 Console.WriteLine($"| {AVERAGE_AMOUNT_SALE.PadLeft(30)} | {averageAmountSale.ToString().PadLeft(10)} |");
			 Console.WriteLine($"| {AVERAGE_ITEM_PRICE.PadLeft(30)} | {averageItemPrice.ToString().PadLeft(10)} |");
			 Console.WriteLine($"+{new String('-',45)}+");
		}  
		else  
		{  
 			 Console.WriteLine("[ERR] your command is not valid ");  
			 Console.WriteLine("Help: ");  
			 Console.WriteLine("    - [print]  : show the content of our commerce records in data.csv");  
			 Console.WriteLine("    - [report] : show a summary from data.csv records ");  
		}
	}
}
