using ETL.Orders.BLL;
using ETL.Orders.DAL;
using ETL.Orders.DAL.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
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

    [SetUp]
    public async Task OneTimeSetUpAsync()
    {
        _msSqlContainer = new MsSqlBuilder()
            .WithPassword("yourStrong(!)Password")
            .Build();

        await _msSqlContainer.StartAsync();

        _connectionString = _msSqlContainer.GetConnectionString();

        var optionsBuilder = new DbContextOptionsBuilder<InternetStoreContext>();
        optionsBuilder.UseSqlServer(_connectionString);

        _context = new InternetStoreContext(optionsBuilder.Options);

        await _context.Database.EnsureCreatedAsync();
        await SeedTestDataAsync();
    }

    private async Task SeedTestDataAsync()
    {
        var user1 = new User
        {
            FirstName = "Иван",
            LastName = "Иванов",
            Email = "abc@email.com",
            Salt = [],
            HashedPassword = [],
            Address = "202 Pine St",
            City = "Smalltown",
            State = "Smallstate",
            PostalCode = "55667",
            Country = "USA",
            PhoneNumber = "555-7890"
        };

        var user2 = new User
        {
            FirstName = "Виктор",
            LastName = "Петров",
            Email = "xyz@email.com",
            Salt = [],
            HashedPassword = [],
            Address = "202 Pine St",
            City = "Smalltown",
            State = "Smallstate",
            PostalCode = "55667",
            Country = "USA",
            PhoneNumber = "555-7890"
        };

        var product1 = new Product
        {
            ProductName = "LG 1755",
            Description = "Description for Product I",
            Price = 12000.75M,
            StockQuantity = 100,
            Category = "Category 5"
        };

        var product2 = new Product
        {
            ProductName = "Xiomi 12X",
            Description = "Description for Product I",
            Price = 42000.75M,
            StockQuantity = 100,
            Category = "Category 5"
        };

        var product3 = new Product
        {
            ProductName = "Noname 14232",
            Description = "Description for Product I",
            Price = 1.70M,
            StockQuantity = 100,
            Category = "Category 5"
        };

        var product4 = new Product
        {
            ProductName = "Noname 222",
            Description = "Description for Product I",
            Price = 3.14M,
            StockQuantity = 100,
            Category = "Category 5"
        };

        _context.Users.AddRange(user1, user2);
        _context.Products.AddRange(product1, product2, product3, product4);

        await _context.SaveChangesAsync();
    }

    [TearDown]
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
    public async Task Test_ProcessXmlFile_ShouldProcessPurchases()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<XmlFileProcessingService>>();
        var purchaseRepository = new PurchaseRepository(_context);
        var purchaseItemRepository = new PurchaseItemRepository(_context);
        var userRepository = new UserRepository(_context);
        var productRepository = new ProductRepository(_context);
        var purchaseService = new PurchaseService(purchaseRepository, purchaseItemRepository, userRepository, productRepository);
        var xmlFileProcessingService = new XmlFileProcessingService(mockLogger.Object, purchaseService);
        var testFilePath = @"test_data1.xml";

        // Act
        await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        var purchases = await _context.Purchases
            .Include(p => p.PurchaseItems)
            .ThenInclude(pi => pi.Product)
            .Include(p => p.User)
            .ToListAsync();

        purchases.Should().NotBeNull();
        purchases.Count.Should().Be(2);
    }

    [Test]
    public async Task Test_ProcessXmlFile_ShouldProcessWhenUserInDifferentPosition()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<XmlFileProcessingService>>();
        var purchaseRepository = new PurchaseRepository(_context);
        var purchaseItemRepository = new PurchaseItemRepository(_context);
        var userRepository = new UserRepository(_context);
        var productRepository = new ProductRepository(_context);
        var purchaseService = new PurchaseService(purchaseRepository, purchaseItemRepository, userRepository, productRepository);
        var xmlFileProcessingService = new XmlFileProcessingService(mockLogger.Object, purchaseService);
        var testFilePath = @"test_data2.xml";

        // Act
        await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        var purchases = await _context.Purchases
            .Include(p => p.PurchaseItems)
            .ThenInclude(pi => pi.Product)
            .Include(p => p.User)
            .ToListAsync();

        purchases.Should().NotBeNull();
        purchases.Count.Should().Be(2);
    }

    [Test]
    public async Task Test_ProcessXmlFile_ShouldProcessWhenRegDateAndFioDoesNotExistInXml()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<XmlFileProcessingService>>();
        var purchaseRepository = new PurchaseRepository(_context);
        var purchaseItemRepository = new PurchaseItemRepository(_context);
        var userRepository = new UserRepository(_context);
        var productRepository = new ProductRepository(_context);
        var purchaseService = new PurchaseService(purchaseRepository, purchaseItemRepository, userRepository, productRepository);
        var xmlFileProcessingService = new XmlFileProcessingService(mockLogger.Object, purchaseService);
        var testFilePath = @"test_data3.xml";

        // Act
        await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        var purchases = await _context.Purchases
            .Include(p => p.PurchaseItems)
            .ThenInclude(pi => pi.Product)
            .Include(p => p.User)
            .ToListAsync();

        purchases.Should().NotBeNull();
        purchases.Count.Should().Be(2);
    }

    [Test]
    public async Task Test_ProcessXmlFile_ShouldPutDataAndProcessDuplucateProperly()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<XmlFileProcessingService>>();
        var purchaseRepository = new PurchaseRepository(_context);
        var purchaseItemRepository = new PurchaseItemRepository(_context);
        var userRepository = new UserRepository(_context);
        var productRepository = new ProductRepository(_context);
        var purchaseService = new PurchaseService(purchaseRepository, purchaseItemRepository, userRepository, productRepository);
        var xmlFileProcessingService = new XmlFileProcessingService(mockLogger.Object, purchaseService);
        var testFilePath = @"test_data4.xml";

        // Act
        await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        var purchases = await _context.Purchases
            .Include(p => p.PurchaseItems)
            .ThenInclude(pi => pi.Product)
            .Include(p => p.User)
            .ToListAsync();

        purchases.Should().NotBeNull();
        purchases.Count.Should().Be(1);
    }

    [Test]
    public async Task Test_ProcessXmlFile_InvalidXmlStructure_ShouldThrowExceptionOnInvalidFIO()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<XmlFileProcessingService>>();
        var purchaseRepository = new PurchaseRepository(_context);
        var purchaseItemRepository = new PurchaseItemRepository(_context);
        var userRepository = new UserRepository(_context);
        var productRepository = new ProductRepository(_context);
        var purchaseService = new PurchaseService(purchaseRepository, purchaseItemRepository, userRepository, productRepository);
        var xmlFileProcessingService = new XmlFileProcessingService(mockLogger.Object, purchaseService);
        var testFilePath = @"test_data5_invalid_fio.xml";

        // Act
        Func<Task> act = async () => await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        await act.Should().ThrowAsync<FormatException>();
    }

    [Test]
    public async Task Test_ProcessXmlFile_MissingRequiredTags_User()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<XmlFileProcessingService>>();
        var purchaseRepository = new PurchaseRepository(_context);
        var purchaseItemRepository = new PurchaseItemRepository(_context);
        var userRepository = new UserRepository(_context);
        var productRepository = new ProductRepository(_context);
        var purchaseService = new PurchaseService(purchaseRepository, purchaseItemRepository, userRepository, productRepository);
        var xmlFileProcessingService = new XmlFileProcessingService(mockLogger.Object, purchaseService);
        var testFilePath = @"test_data6_invalid_user.xml";

        // Act
        Func<Task> act = async () => await xmlFileProcessingService.ProcessFile(testFilePath);

        // Assert
        await act.Should().ThrowAsync<FormatException>();
    }
}

