namespace SalesReporterKata;

public class OrderDto
{
    public string OrderId
    {
        get => OrderId;
        set => OrderId = value;
    }
    public string UserName
    {
        get => UserName;
        set => UserName = value;
    }
    public string NumberOfItems
    {
        get => NumberOfItems;
        set => NumberOfItems = value;
    }
    public string TotalOfBasket
    {
        get => TotalOfBasket;
        set => TotalOfBasket = value;
    }
    public string DayOfBuy
    {
        get => DayOfBuy;
        set => DayOfBuy = value;
    }
}