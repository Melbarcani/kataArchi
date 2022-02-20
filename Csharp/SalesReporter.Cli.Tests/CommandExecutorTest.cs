using System.Collections.Generic;
using NFluent;
using SalesReporterKata;
using Xunit;

namespace SalesReporter.Cli.Tests;

public class CommandExecutorTest
{
    [Fact]
    void CommandExecutorShouldReturnPrintInstance()
    {
        var ordersList = new List<OrderDto>(){createExpectedOrder()};
        var commandResult = CommandExecutorStrategyFactory.Create("print", ordersList);
        Check.That(commandResult).IsInstanceOf<PrintData>();
        Check.That(commandResult).IsNotInstanceOf<CreateReport>();
        
    }
    
    private static OrderDto createExpectedOrder()
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