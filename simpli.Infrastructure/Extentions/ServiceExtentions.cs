using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using simpli.Application;
using simpli.Application.Dtos;
using simpli.Application.Services;
namespace simpli.Infrastructure;

public static class ServiceExtentions
{
  public static IServiceCollection LoadEnvironment(this IServiceCollection services, IConfiguration configuration)
  {
    DotNetEnv.Env.Load();
    var connectionStrings = new ConnnectionStrings();
    configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
    services.Configure<ConnnectionStrings>(configuration.GetSection("ConnectionStrings"));
    return services;
  }
  public static IServiceCollection ConfigureSqlDB(this IServiceCollection services, IConfiguration config)
  {
    // Clean up spaces or lingering literal quotation marks from the .env file values
    var envType = config["OtherSettings:CurrentEnviroment"]?.ToLower().Trim(' ', '"');

    services.AddDbContext<AppDbContext>(opt =>
    {
      if (envType == "dev")
      {
        var devCs = config["ConnectionStrings:DevDB"];
        if (string.IsNullOrEmpty(devCs)) throw new InvalidOperationException("DevDB connection string is missing.");
        opt.UseSqlServer(devCs);
      }
      else if (envType == "prod")
      {
        var prodCs = config["ConnectionStrings:ProdDB"];
        if (string.IsNullOrEmpty(prodCs)) throw new InvalidOperationException("ProdDB connection string is missing.");
        opt.UseSqlServer(prodCs);
      }
      else
      {
        throw new InvalidOperationException(
            $"SQL Server could not be configured. The environment target read as '{envType}'. " +
            "Ensure builder.Configuration.AddEnvironmentVariables() is active.");
      }
    });

    return services;
  }
  public static IServiceCollection IdentityConfigurationsScope(this IServiceCollection services)
  {
    // 1. Configure the core identity parameters without standard cookie-bloat
    services.AddIdentityCore<AppUser>(options =>
    {
      options.User.RequireUniqueEmail = true;
    })
    // 2. Firmly link your full database context class
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

    // 3. Inject the specific API endpoint plumbing (Bearer handlers)
    // This connects seamlessly back to AppUser and your configured AppDbContext stores
    services.AddIdentityApiEndpoints<AppUser>()
        .AddEntityFrameworkStores<AppDbContext>();

    services.AddAuthorization();

    return services;
  }
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AdditionalUserClaimsPrincipalFactory>();
    services.Configure<ConnnectionStrings>(configuration.GetSection("ConnectionStrings"));
    services.Configure<OtherSettings>(configuration.GetSection("OtherSettings"));

    services.AddScoped<ICompanyRepo, CompanyRepo>();
    services.AddScoped<INotificationRepo, NotificationRepo>();
    services.AddScoped<IRoomRepo, RoomRepo>();
    services.AddScoped<IVisitorRepo, VisitorRepo>();

    services.AddScoped<CompanyService>();
    services.AddScoped<NotificationService>();
    services.AddScoped<VisitorService>();
    services.AddScoped<RoomServices>();

    return services;
  }
  public static IServiceCollection AllowCors(this IServiceCollection services)
  {
    services.AddCors(opt =>
    {
      opt.AddPolicy("AllowNextJs", builder =>
      {
        builder.WithOrigins(["http://localhost:3000", "Prod frontend here"])
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
      });
    });

    return services;
  }
  public static IServiceCollection AddMappers(this IServiceCollection services)
  {
    services.AddSingleton<VisitorMappers>();
    services.AddSingleton<CompanyMappers>();
    services.AddSingleton<NotificationMappers>();
    services.AddSingleton<RoomMappers>();
    return services;
  }
}