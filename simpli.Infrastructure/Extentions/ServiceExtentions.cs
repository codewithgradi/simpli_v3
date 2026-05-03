using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtentions
{
  public static void ConfigureSqlDB(IServiceCollection services, IConfiguration config)
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
  }
  public static void IdentityConfigurationsScope(IServiceCollection services)
  {
    services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

    services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

    services.AddAuthorization();
  }

}