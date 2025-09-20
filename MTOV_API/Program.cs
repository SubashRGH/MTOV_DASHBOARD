using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using MTOV_AppSettings;
using MTOV_JwtTokenProvider;
using RECRM_API.Extensions;
using RECRM_API.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.Configure<AppProperty>(builder.Configuration.GetSection("AppProperty"));
builder.Services.AddSingleton<AppProperty>(sp =>
    sp.GetRequiredService<IOptions<AppProperty>>().Value);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("./logs/exceptions.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddCoreDependencies();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<InternalRequestFilter>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<InternalRequestFilter>(); // Register globally
});
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Services.AddServiceDependencies(builder.Configuration);

builder.Services.Configure<JwtTokenProviderOptionsBuilder>(
    builder.Configuration.GetSection("JwtTokenProviderOptionsBuilder"));

builder.Services.AddHealthChecks();

var app = builder.Build();
app.AddAppDependecies();

app.MapHealthChecks("/health");

app.Run();