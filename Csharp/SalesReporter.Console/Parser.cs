namespace SalesReporterKata;

public class Parser
{
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
}