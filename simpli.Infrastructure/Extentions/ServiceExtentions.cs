using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using simpli.Application.Dtos;
using simpli.Application.Services;
using Asp.Versioning;
using simpli.Domain.Exceptions;
namespace simpli.Infrastructure;

public static class ServiceExtentions
{
  public static IServiceCollection AddApiVersionForBackend(this IServiceCollection services)
  {
    services.AddApiVersioning(opt =>
    {
      //Default when not specified
      opt.AssumeDefaultVersionWhenUnspecified = true;
      opt.DefaultApiVersion = new ApiVersion(1, 0);

      opt.ReportApiVersions = true;

      //Tells .Net to look for querystring api-version
      opt.ApiVersionReader = new QueryStringApiVersionReader("api-version");
    });
    return services;
  }

  public static IServiceCollection LoadEnvironment(this IServiceCollection services, IConfiguration configuration)
  {
    DotNetEnv.Env.Load();
    var connectionStrings = new ConnnectionStrings();
    configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
    services.Configure<ConnnectionStrings>(configuration.GetSection("ConnectionStrings"));
    services.Configure<OtherSettings>(configuration.GetSection("OtherSettings"));
    return services;
  }
  public static IServiceCollection ConfigureSqlDB(this IServiceCollection services, IConfiguration config)
  {
    var envType = config["OtherSettings:CurrentEnviroment"]?.ToLower().Trim(' ', '"');

    services.AddDbContext<AppDbContext>(opt =>
    {
      if (envType == "dev")
      {
        var devCs = config["ConnectionStrings:DevDB"];
        if (string.IsNullOrEmpty(devCs)) throw new InvalidOperationException("DevDB connection string is missing.");
        opt.UseNpgsql(devCs).UseSnakeCaseNamingConvention();
      }
      else if (envType == "prod")
      {
        var prodCs = config["ConnectionStrings:ProdDB"];
        if (string.IsNullOrEmpty(prodCs)) throw new InvalidOperationException("ProdDB connection string is missing.");
        opt.UseNpgsql(prodCs).UseSnakeCaseNamingConvention();
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
    services.AddIdentityCore<AppUser>(options =>
    {
      options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

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

    services.AddHttpClient<IEmailService, EmailService>();

    return services;
  }
  public static IServiceCollection AllowCors(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddCors(opt =>
    {
      opt.AddPolicy("AllowNextJs", builder =>
      {
        var frontendUrlDev = configuration["OtherSettings:FrontEndUrl"]?.ToLower().Trim(' ', '"');
        var frontendUrlProd = configuration["OtherSettings:FrontEndUrlProd"]?.ToLower().Trim(' ', '"');
        var backendLiveApiLink = configuration["ConnectionStrings:BackendLiveApiLink"]?.ToLower().Trim(' ', '"');

        if (string.IsNullOrEmpty(frontendUrlDev) || string.IsNullOrEmpty(frontendUrlProd) || string.IsNullOrEmpty(backendLiveApiLink))
        {
          throw new ResourceNotFoundException("There are no front-end urls.");
        }

        builder.WithOrigins([frontendUrlDev, frontendUrlProd, backendLiveApiLink])
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