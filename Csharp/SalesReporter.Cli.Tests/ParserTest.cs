using System;
using System.Collections.Generic;
using NFluent;
using SalesReporterKata;
using Xunit;

namespace SalesReporter.Cli.Tests;

public class ParserTest
{
    [Fact]
    void ParseHeaderTest()
    {
        string file = "./data.csv";
        CSVParser parser = new CSVParser(file);
        Check.That(parser.parseHeader()).IsEqualTo(new List<string>(){"orderid", "userName", "numberOfItems", "totalOfBasket", "dateOfBuy"});
    }

    [Fact]
    void ParseDataTest()
    {
        string file = "./data.csv";
        CSVParser parser = new CSVParser(file);

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

    [Fact]
    void CreateReportDTO()
    {
        // Given
        string file = "./data.csv";
        CSVParser parser = new CSVParser(file);
        OrderDto expectedFirstOrder = CreateExpectedOrder();
        // When
        List<OrderDto> ordersList = parser.CreateOrdersList();
        // Then
        Check.That(ordersList.Count).IsEqualTo(5);
        Check.That(ordersList[0].Client).IsEqualTo(expectedFirstOrder.Client);
        Check.That(ordersList[0].dataTitles).IsEqualTo(expectedFirstOrder.dataTitles);
        Check.That(ordersList[0].DayOfBuy).IsEqualTo(expectedFirstOrder.DayOfBuy);
    }

    private static OrderDto CreateExpectedOrder()
    {
        OrderDto expectedFirstOrder = new OrderDto();
        expectedFirstOrder.OrderId = "1";
        expectedFirstOrder.Client = " peter";
        expectedFirstOrder.NumberOfItems = 3;
        expectedFirstOrder.TotalOfBasket = 123.00;
        expectedFirstOrder.DayOfBuy = " 2021-11-30";
        return expectedFirstOrder;
    }
}