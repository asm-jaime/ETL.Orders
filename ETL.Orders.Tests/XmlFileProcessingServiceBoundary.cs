using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ETL.Orders.Tests;

[TestFixture]
public partial class DatabaseTests
{
    [Test]
    public async Task Test_ProcessXmlFile_MinimumValues_ShouldProcessCorrectly()
    {
        // Arrange
        var xmlFileProcessingService = GetXmlFileProcessingService();
        var testFilePath = @"test_data7_minimum_values.xml";

        // Act
        await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        var purchases = await _context.Purchases.ToListAsync();
        purchases.Should().NotBeNull();
        purchases.Count.Should().Be(1);
        purchases.First().PurchaseItems.Select(pi => pi.UnitPrice).Sum().Should().Be(0M);
    }

    [Test]
    public async Task Test_ProcessXmlFile_MaximumValues_ShouldProcessCorrectly()
    {
        // Arrange
        var xmlFileProcessingService = GetXmlFileProcessingService();
        var testFilePath = @"test_data8_maximum_values.xml";

        // Act
        await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        var purchases = await _context.Purchases.ToListAsync();
        purchases.Should().NotBeNull();
        purchases.Count.Should().Be(1);
        purchases.First().PurchaseItems.Select(pi => pi.UnitPrice).Sum().Should().Be(99999999.99M);
    }


    [Test]
    public async Task Test_ProcessXmlFile_EmptyOrders_ShouldNotCreateAnyPurchase()
    {
        // Arrange
        var xmlFileProcessingService = GetXmlFileProcessingService();
        var testFilePath = @"test_data9_empty.xml";

        // Act
        await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        var purchases = await _context.Purchases.ToListAsync();
        purchases.Should().BeEmpty();
    }
}

