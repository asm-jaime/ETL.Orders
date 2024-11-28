using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ETL.Orders;

public class Program
{
    static void Main(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
            .Build();

        var builder = new ServiceCollection()
            .AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

        builder.AddDbContext<InternetStoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        var serviceProvider = builder.BuildServiceProvider();

        using(var context = serviceProvider.GetRequiredService<InternetStoreContext>())
        {
            Console.WriteLine("Database connection successful.");
        }
    }
}

