using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using simpli.Api.Middlewares;
using simpli.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "../.env"));

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOpenApi("v1");
builder.Services.AddRouting(opt => { opt.LowercaseUrls = true; });
builder.Services.AddTransient<GlobalExceptionMiddleware>();
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
//for background email processing
builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services
.AddApiVersionForBackend()
.LoadEnvironment(builder.Configuration)
.ConfigureSqlDB(builder.Configuration)
.AddInfrastructureServices(builder.Configuration)
.IdentityConfigurationsScope()
.AllowCors(builder.Configuration)
.AddMappers();


var app = builder.Build();

app.MapOpenApi();

//This maps all endpoints on scalar for visualisation on dev or prod env
app.MapScalarApiReference(opt =>
{
    opt.WithOpenApiRoutePattern("/openapi/v1.json");
});



app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


//Mapping from scalar UI
app.MapIdentityApi<AppUser>();

app.Run();

