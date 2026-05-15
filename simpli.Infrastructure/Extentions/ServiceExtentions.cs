using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using simpli.Application;
using simpli.Application.Dtos;
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
  public static IServiceCollection ConfigureSqlDB(this IServiceCollection services,
   IConfiguration config)
  {

    var envType = config["OtherSettings:CurrentEnviroment"];
    services.AddDbContext<AppDbContext>(opt =>
    {
      if (envType == "dev")
      {
        opt.UseSqlServer(config["ConnectionStrings:DevDB"]);
      }
      else if (envType == "prod")
      {
        opt.UseSqlServer(config["ConnectionStrings:ProdDB"]);
      }
    });

    return services;
  }
  public static IServiceCollection IdentityConfigurationsScope(this IServiceCollection services)
  {
    services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

    services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

    services.AddAuthorization();

    return services;
  }
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AdditionalUserClaimsPrincipalFactory>();
    services.Configure<ConnnectionStrings>(configuration.GetSection("ConnectionStrings"));
    services.Configure<OtherSettings>(configuration.GetSection("OtherSettings"));
    services.AddScoped<ICompanyRepo, CompanyRepo>();
    services.AddScoped<INotification, NotificationRepo>();
    services.AddScoped<IRoomRepo, RoomRepo>();
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
  public static IServiceCollection AddMappers(this IServiceCollection services)
  {
    services.AddSingleton<VisitorMappers>();
    services.AddSingleton<CompanyMappers>();
    services.AddSingleton<NotificationMappers>();
    services.AddSingleton<RoomMappers>();
    return services;
  }
}