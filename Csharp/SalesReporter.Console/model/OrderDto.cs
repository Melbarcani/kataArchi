namespace SalesReporterKata;

public class OrderDto
{
    public List<string> dataTitles { get; }

    public List<string> GetValues()
    {
        return new List<string> {OrderId, Client, NumberOfItems.ToString(), TotalOfBasket.ToString("F"), DayOfBuy};
    }
    public string OrderId
    {
        get;
        set;
    }
    public string Client
    {
        get;
        set;
    }
    public int NumberOfItems
    {
        get;
        set;
    }
    public double TotalOfBasket
    {
        get;
        set;
    }
    public string DayOfBuy
    {
        get;
        set;
    }
    public OrderDto()
    {
        dataTitles = new List<string> {"orderid", "userName", "numberOfItems", "totalOfBasket", "dateOfBuy"};
    }
}