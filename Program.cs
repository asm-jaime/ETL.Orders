using ETL.Orders.BLL;
using ETL.Orders.BLL.Services;
using ETL.Orders.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ETL.Orders;

public class Program
{
    static async Task Main(string[] args)
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

        var services = new ServiceCollection();
        services.AddLogging(builder => { builder.AddConsole(); builder.AddDebug(); });
        services.AddDbContext<InternetStoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPurchaseItemRepository, PurchaseItemRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<ProductService>();
        services.AddScoped<PurchaseService>();
        services.AddScoped<UserService>();
        services.AddSingleton<IFileProcessingService, XmlFileProcessingService>();
        var serviceProvider = services.BuildServiceProvider();

        var fileProcessing = serviceProvider.GetRequiredService<IFileProcessingService>();
        await fileProcessing.ProcessFile(filePath);

        Console.WriteLine("Process file is successful complete.");
    }
}

