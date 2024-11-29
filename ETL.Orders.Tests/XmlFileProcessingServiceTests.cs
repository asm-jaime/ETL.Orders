using ETL.Orders.DAL;
using ETL.Orders.DAL.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace ETL.Orders.Tests;

[TestFixture]
public class DatabaseTests
{
    private MsSqlContainer _msSqlContainer;
    private string _connectionString;
    private InternetStoreContext _context;

    [OneTimeSetUp]
    public async Task OneTimeSetUpAsync()
    {
        // Start the MsSql Testcontainer
        _msSqlContainer = new MsSqlBuilder()
            .WithPassword("yourStrong(!)Password")
            .Build();

        await _msSqlContainer.StartAsync();

        _connectionString = _msSqlContainer.GetConnectionString();

        var optionsBuilder = new DbContextOptionsBuilder<InternetStoreContext>();
        optionsBuilder.UseSqlServer(_connectionString);

        _context = new InternetStoreContext(optionsBuilder.Options);

        await _context.Database.EnsureCreatedAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDownAsync()
    {
        if(_context != null)
        {
            await _context.DisposeAsync();
        }
        if(_msSqlContainer != null)
        {
            await _msSqlContainer.StopAsync();
            await _msSqlContainer.DisposeAsync();
        }
    }

    [Test]
    public async Task Test_AddAndRetrieveProduct_ShouldReturnInsertedProduct()
    {
        // Arrange
        var product = new Product
        {
            ProductName = "Test Product",
            Category = "Test Category",
            Price = 9.99M,
            DateAdded = DateTime.UtcNow
        };

        // Act
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var retrievedProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == "Test Product");

        // Assert
        retrievedProduct.Should().NotBeNull();
        retrievedProduct.ProductName.Should().Be("Test Product");
        retrievedProduct.Category.Should().Be("Test Category");
        retrievedProduct.Price.Should().Be(9.99M);
    }
}

