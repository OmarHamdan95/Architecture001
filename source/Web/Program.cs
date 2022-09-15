using Architecture.Application;
using Architecture.Database;
using Architecture.Web;
using DotNetCore.AspNetCore;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.IoC;
using DotNetCore.Logging;
using DotNetCore.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Serilog;

var builder = WebApplication.CreateBuilder();
var configuration = builder.Configuration;
Log.Logger = CreateSerilogLogger(configuration);

try
{
    builder.Services.RegisterApplicationDependencies(configuration);

    builder.ConfigureSerilog();

    var app = builder.Build();

    app.ConfigureApp(configuration);

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", typeof(Program).Namespace);
    Log.Logger.Error(ex, "Program terminated unexpectedly ({ApplicationContext})!", typeof(Program).Assembly.FullName);
}
finally
{
    Log.CloseAndFlush();
}

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    return new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}
