namespace SalesReporterKata;

public class CSVParser : Parser
{
    private static char CSV_SEPARATOR = ',';
    private string[] contentLines;

    public CSVParser(string file) : base(file)
    {
        contentLines = getAllLines();
    }

    private String[] getAllLines()
    {
        return File.ReadAllLines(file);
    }

    public List<string> parseHeader()
    {
        var columnInfos = new List<string>();
        //build the header of the table with column names from our data file  
        foreach (var columName in contentLines[0].Split(','))
        {
            columnInfos.Add(columName);
        }
        return columnInfos;
    }

    public IEnumerable<string> parseData()
    {
        return contentLines.Skip(1);
    }

    public override List<OrderDto> CreateOrdersList()
    {
        List<OrderDto> ordersList = new List<OrderDto>();
        foreach (string line in contentLines)
        {
            var cells = line.Split(CSV_SEPARATOR);
            OrderDto orderData = new OrderDto();
            orderData.OrderId = cells[0];
            orderData.UserName = cells[1];
            orderData.NumberOfItems = cells[2];
            orderData.TotalOfBasket = cells[3];
            orderData.DayOfBuy = cells[4];
            ordersList.Add(orderData);
        }

        return ordersList;
    }
}