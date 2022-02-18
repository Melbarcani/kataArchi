namespace SalesReporterKata;

public class Parser
{
    private static char CSV_SEPARATOR = ',';
    private string file;
    private string[] contentLines;

    public Parser(string file)
    {
        this.file = file;
        contentLines = getAllLines();
    }

    private String[] getAllLines()
    {
        return File.ReadAllLines(file);
    }

    public String parseHeader()
    {
        return contentLines[0];
    }

    public IEnumerable<string> parseData()
    {
        return contentLines.Skip(1);
    }

    public List<OrderDto> ParseOrdersList()
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