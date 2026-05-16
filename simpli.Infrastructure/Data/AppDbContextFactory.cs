using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace simpli.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // 1. Get the execution directory (which EF Core sets to simpli.Api)
        var currentDir = Directory.GetCurrentDirectory();
        var rootDirectory = currentDir;

        // 2. If we are inside the simpli.Api folder, step up to the root folder
        if (currentDir.EndsWith("simpli.Api") || currentDir.EndsWith("simpli.Api/"))
        {
            rootDirectory = Path.GetFullPath(Path.Combine(currentDir, ".."));
        }
        // If we are inside the infrastructure folder, step up as well
        else if (currentDir.EndsWith("simpli.Infrastructure") || currentDir.EndsWith("simpli.Infrastructure/"))
        {
            rootDirectory = Path.GetFullPath(Path.Combine(currentDir, ".."));
        }

        // 3. Target the .env file straight in the root workspace folder
        var envFilePath = Path.Combine(rootDirectory, ".env");

        if (File.Exists(envFilePath))
        {
            DotNetEnv.Env.Load(envFilePath);
        }
        else
        {
            throw new FileNotFoundException(
                $"[EF Factory Error]: Still looking for the .env file!\n" +
                $"Checked path: '{envFilePath}'\n" +
                $"Current Working Directory was: '{currentDir}'\n" +
                $"Please verify your .env file is located at the root of 'simpli_v3'.");
        }

        // 4. Read your DevDB connection string from the environment configuration
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DevDB");

        // 5. Guard clause validation check
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"[EF Factory Error]: Found your .env file at '{envFilePath}', but the key 'ConnectionStrings__DevDB' is empty or missing inside it.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}