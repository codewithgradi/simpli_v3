using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using simpli.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//Mapping from scalar UI
app.MapIdentityApi<IdentityUser>();

app.Run();

