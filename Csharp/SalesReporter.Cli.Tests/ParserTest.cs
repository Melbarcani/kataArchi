using System;
using System.Collections.Generic;
using NFluent;
using SalesReporterKata;
using Xunit;

namespace SalesReporter.Cli.Tests;

public class ParserTest
{
    [Fact]
    public void ParseHeaderTest()
    {
        string file = "./data.csv";
        Parser parser = new Parser(file);
        Check.That(parser.parseHeader()).IsEqualTo("orderid,userName,numberOfItems,totalOfBasket,dateOfBuy");
    }

    [Fact]
    public void ParseDataTest()
    {
        string file = "./data.csv";
        Parser parser = new Parser(file);

        string[] goldenData =
        {
            "1, peter, 3, 123.00, 2021-11-30",
            "2, paul, 1, 433.50, 2021-12-11",
            "3, peter, 1, 329.99, 2021-12-18",
            "4, john, 5, 467.35, 2021-12-30",
            "5, john, 1, 88.00, 2022-01-04"
        };
        var dataLines = parser.parseData();
        int i = 0;
        foreach (string line in dataLines)
        {
            Check.That(line).IsEqualTo(goldenData[i]);
            i++;
        }
    }
}