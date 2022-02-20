namespace SalesReporterKata;

static class DisplayCreatorStrategyFactory
{
    public static IDisplayCreatorStrategy Create(string command)
    {
        if (command == Program.Commands.print.ToString())
        {
            return new PrintData();
        }

        return new PrintErrorLog();
    }
}

public interface IDisplayCreatorStrategy
{
}

class PrintData : IDisplayCreatorStrategy
{
    private string TopBottomLine;
    private string headerTitles;

    public string CreateDisplayer(List<string> columnName, IEnumerable<string> dataLines)
    {
        InitTitles(columnName);
        var dataToDisplay = CreateHeaderTitles() + "\r\n";;
        dataToDisplay += AppendDataToTable(dataLines);
        dataToDisplay += TopBottomLine+ "\r\n";
        return dataToDisplay;
    }


    private void InitTitles(List<string> columnNames)
    {
        headerTitles = String.Join(
            " | ",
            columnNames.Select(val => val.PadLeft(16)));
    }
    public string CreateHeaderTitles()
    {
        
        CreateLine(headerTitles);
        var titlesDisplay = TopBottomLine + "\r\n";
        titlesDisplay = String.Concat(titlesDisplay,CreateExternalPillars(headerTitles),"\r\n");
        titlesDisplay += TopBottomLine ;
        return titlesDisplay;
    }

    public string AppendDataToTable(IEnumerable<string> dataLines)
    {
        var tableLines = "";
        foreach (string line in dataLines)  
        { 
            //extract columns from our csv line and add all these cells to the line  
            var tableLine  = String.Join(
                " | ", 
		            
                line.Split(',').Select(
                    (val,ind) => val.PadLeft(16)));
            tableLines += $"| {tableLine} |\r\n";
        }

        return tableLines;
    }

    private string CreateExternalPillars(string headerTitles)
    {
        return "| " + headerTitles + " |";
    }

    private void CreateLine(string headerTitles)
    {
        TopBottomLine = "+" + new String('-', headerTitles.Length + 2) + "+";
    }
}

class PrintErrorLog : IDisplayCreatorStrategy
{
    public string Create()
    {
        return @$"[ERR] your command is not valid 
Help: 
    - [print]  : show the content of our commerce records in data.csv
    - [report] : show a summary from data.csv records ";  
    }
}