using Hangfire;
using Hangfire.MemoryStorage;
using Scalar.AspNetCore;
using simpli.Api.Middlewares;
using simpli.Infrastructure;
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args
});

// CRITICAL FIX: Disable file system watchers for JSON configuration on cloud hosts
builder.Configuration.Sources.Clear();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: false);
DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), "../.env"));

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOpenApi();
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

app.MapScalarApiReference(opt =>
{
    opt.WithTitle("Simpli API Docs")
       .WithTheme(ScalarTheme.DeepSpace)

       // FIX: Use the standard pattern template. 
       // Scalar will automatically replace '{documentName}' with 'v1' internally.
       .WithOpenApiRoutePattern("/openapi/{documentName}.json");

    opt.AddPreferredSecuritySchemes("Bearer");
    opt.Servers = [new ScalarServer("https://api-simpli.onrender.com")];
});

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


//Mapping from scalar UI
app.MapIdentityApi<AppUser>();

app.Run();

