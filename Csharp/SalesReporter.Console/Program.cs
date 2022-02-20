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
    public static class App
    {
        public static void Run(string command, string file)
        {
            Parser parser = new CSVParser(file);
            var ordersList = parser.CreateOrdersList();
            var dataToDisplay = CommandExecutorStrategyFactory.Create(command, ordersList).Execute();
            Console.Write(dataToDisplay);
        }
    }

    public static void Main(string[] args)
    {
        string command = ExtractCommand(args);
        string file = ExtractFile(args);
        App.Run(command, file);
    }
}