using static SalesReporterKata.Constants;

namespace SalesReporterKata;

public static class CommandExecutorStrategyFactory
{
    public static ICommandExecutorStrategy Create(string command, List<OrderDto> ordersList)
    {
        if (command == Program.Commands.print.ToString())
        {
            return new PrintData(ordersList);
        }
        if (command == Program.Commands.report.ToString())
        {
            return new CreateReport(ordersList);
        }

        return new UnknownCommandMessage();
    }
}

public interface ICommandExecutorStrategy
{
    public string Execute();
}

public class CreateReport : ICommandExecutorStrategy
{
    private Report report;
    private List<OrderDto> ordersList;
    public CreateReport(List<OrderDto> ordersList)
    {
        report = new Report();
        this.ordersList = ordersList;
    }

    public string Execute()
    {
        report.NumberOfSales = ordersList.Count();
        HashSet<string> clients = new HashSet<string>();
        foreach (var order in ordersList)
        {
            clients.Add(order.Client);
            report.TotalItemsSold += order.NumberOfItems; 
            report.TotalSalesAmount += order.TotalOfBasket;  
        }

        report.NumberOfClients = clients.Count;
        report.AverageAmountSale = Math.Round(report.TotalSalesAmount / report.NumberOfSales, 2);
        report.AverageItemPrice = Math.Round(report.TotalSalesAmount / report.TotalItemsSold, 2);
        return CreateDisplay();
    }

    private string  CreateDisplay()
    {
        var dataToDisplay = SALES_VIEWER_TITLE + "\r\n";
        dataToDisplay += $"+{new String('-', 45)}+"+ "\r\n";
        dataToDisplay += $"| {NUMBER_OF_SALES.PadLeft(30)} | {report.NumberOfSales.ToString().PadLeft(10)} |"+ "\r\n";
        dataToDisplay += $"| {NUMBER_OF_CLIENTS.PadLeft(30)} | {report.NumberOfClients.ToString().PadLeft(10)} |"+ "\r\n";
        dataToDisplay += $"| {TOTAL_ITEMS_SOLD.PadLeft(30)} | {report.TotalItemsSold.ToString().PadLeft(10)} |"+ "\r\n";
        dataToDisplay +=
            $"| {TOTAL_SALES_AMOUNT.PadLeft(30)} | {Math.Round(report.TotalSalesAmount, 2).ToString().PadLeft(10)} |"+ "\r\n";
        dataToDisplay += $"| {AVERAGE_AMOUNT_SALE.PadLeft(30)} | {report.AverageAmountSale.ToString().PadLeft(10)} |"+ "\r\n";
        dataToDisplay += $"| {AVERAGE_ITEM_PRICE.PadLeft(30)} | {report.AverageItemPrice.ToString().PadLeft(10)} |"+ "\r\n";
        dataToDisplay += $"+{new String('-', 45)}+"+ "\r\n";
        
        return dataToDisplay;
    }
}

public class PrintData : ICommandExecutorStrategy
{
    private string TopBottomLine;
    private string HeaderTitles;
    private List<OrderDto> ordersList;

    public PrintData(List<OrderDto> ordersList)
    {
        this.ordersList = ordersList;
        InitTitles(ordersList[0].dataTitles);
    }

    public string Execute()
    {
        var dataToDisplay = SALES_VIEWER_TITLE + "\r\n";
        dataToDisplay += CreateHeaderTitles() + "\r\n";
        dataToDisplay += AppendDataToTable();
        dataToDisplay += TopBottomLine + "\r\n";
        return dataToDisplay;
    }


    private void InitTitles(List<string> columnNames)
    {
        HeaderTitles = String.Join(
            " | ",
            columnNames.Select(val => val.PadLeft(16)));
    }

    private string CreateHeaderTitles()
    {
        CreateLine();
        var titlesDisplay = TopBottomLine + "\r\n";
        titlesDisplay = String.Concat(titlesDisplay, CreateExternalPillars(), "\r\n");
        titlesDisplay += TopBottomLine;
        return titlesDisplay;
    }

    private string AppendDataToTable()
    {
        var tableLines = "";
        foreach (var order in ordersList)
        {
            var tableLine = String.Join(
                " | ",
                order.GetValues().Select(
                    (val) => val.PadLeft(16)));
            tableLines += $"| {tableLine} |\r\n";
        }

        return tableLines;
    }

    private string CreateExternalPillars()
    {
        return "| " + HeaderTitles + " |";
    }

    private void CreateLine()
    {
        TopBottomLine = "+" + new String('-', HeaderTitles.Length + 2) + "+";
    }
}

class UnknownCommandMessage : ICommandExecutorStrategy
{
    public string Execute()
    {
        return
            SALES_VIEWER_TITLE + "\r\n" + @$"[ERR] your command is not valid 
Help: 
    - [print]  : show the content of our commerce records in data.csv
    - [report] : show a summary from data.csv records "+ "\r\n";
    }
}