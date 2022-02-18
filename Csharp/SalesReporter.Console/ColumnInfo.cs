namespace SalesReporterKata;

public class ColumnInfo
{
    private int Index = 0;
    public string Name { get; }

    public ColumnInfo(string name)
    {
        Index = ++Index;
        Name = name;
    }
}