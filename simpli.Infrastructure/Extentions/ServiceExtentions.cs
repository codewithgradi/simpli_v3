using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using simpli.Application;
namespace simpli.Infrastructure;

public static class ServiceExtentions
{
  public static IServiceCollection ConfigureSqlDB(this IServiceCollection services,
   IConfiguration config)
  {
    var connectionStrings = new ConnnectionStrings();
    config.GetSection("ConnectionStrings").Bind(connectionStrings);

    var otherSettings = new OtherSettings();
    config.GetSection("OtherSettings").Bind(otherSettings);

    services.AddDbContext<AppDbContext>(opt =>
    {
      if (otherSettings.Env == "dev")
      {
        opt.UseSqlServer(config.GetConnectionString("DevDB"));
      }
      else if (otherSettings.Env == "prod")
      {
        opt.UseSqlServer(config.GetConnectionString("ProdDB"));
      }
    });

    return services;
  }
  public static IServiceCollection IdentityConfigurationsScope(this IServiceCollection services)
  {
    services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

    services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

    services.AddAuthorization();

    return services;
  }
  public static IServiceCollection EnvironmentConfig(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<ConnnectionStrings>
    (configuration.GetSection("ConnectionStrings"));

    services.Configure<OtherSettings>
    (configuration.GetSection("OtherSettings"));

    services.AddScoped<ICompanyRepo, CompanyRepo>();
    services.AddScoped<INotification, NotificationRepo>();
    services.AddScoped<IRoomRepo, RoomRepo>();
    services.AddScoped<IStatsRepo, StatsRepo>();
    services.AddScoped<IVisitorRepo, VisitorRepo>();

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
}