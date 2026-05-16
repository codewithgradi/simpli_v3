using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace simpli.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // 1. Find the path to  API project folder where the .env and appsettings live
        var apiPath = Path.Combine(Directory.GetCurrentDirectory(), "../simpli.Api");

        // 2. FORCE .NET to load your .env file into the system environment variables right now
        // This makes your environment variables accessible via Environment.GetEnvironmentVariable()
        var envFilePath = Path.Combine(apiPath, ".env");
        if (File.Exists(envFilePath))
        {
            DotNetEnv.Env.Load(envFilePath);
        }

        // 3. Build a temporary configuration manager
        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables() // This pulls the keys from  loaded .env file
            .Build();

        // 4. Extract your connection string from the environment variables
        // If your .env file has a line like: DevDB="Server=localhost;Database=..."
        var connectionString = Environment.GetEnvironmentVariable("DevDB")
                               ?? configuration.GetConnectionString("DevDB");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not find the 'DevDB' connection string in your .env file or environment variables.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}