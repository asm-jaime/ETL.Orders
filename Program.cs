using ETL.Orders.FileProcessingService;
using ETL.Orders.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ETL.Orders;

public class Program
{
    static void Main(string[] args)
    {
        if(args.Length == 0)
        {
            Console.WriteLine("Please provide the path to the data.xml file as an argument.");
            return;
        }

        var filePath = args[0];
        if(!File.Exists(filePath))
        {
            Console.WriteLine($"The file '{filePath}' does not exist.");
            return;
        }

        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection().AddLogging(builder => { builder.AddConsole(); builder.AddDebug(); });
        services.AddDbContext<InternetStoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddSingleton<IFileProcessingService, XmlFileProcessingService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<IPurchaseItemRepository, PurchaseItemRepository>();
        var serviceProvider = services.BuildServiceProvider();

        using(var context = serviceProvider.GetRequiredService<InternetStoreContext>())
        {
            Console.WriteLine("Database connection successful.");
        }
    }
}

