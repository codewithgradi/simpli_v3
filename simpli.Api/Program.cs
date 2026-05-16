using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using simpli.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("v1");
builder.Services.AddControllers();

DotNetEnv.Env.Load();

builder.Services
.LoadEnvironment(builder.Configuration)
.ConfigureSqlDB(builder.Configuration)
.AddInfrastructureServices(builder.Configuration)
.IdentityConfigurationsScope()
.AllowCors()
.AddMappers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //This maps all endpoints on scalar for visualisation
    app.MapScalarApiReference(opt =>
    {
        opt.WithOpenApiRoutePattern("/openapi/v1.json");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


//Mapping from scalar UI
app.MapIdentityApi<IdentityUser>();

app.Run();

