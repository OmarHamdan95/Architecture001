using Architecture.Database.Repositories;
using Architecture.Domain.Interfaces;
using Architecture.Domain.Middlewares;
using DotNetCore.AspNetCore;
using Mapster;
using Serilog;
using StackExchange.Profiling.Storage;

namespace Architecture.Web;

public static class Extintion
{
    public static void AddProfiler(this IServiceCollection service)
    {
        service.AddMiniProfiler(option =>
        {
            option.RouteBasePath = "/api/profiler";
            option.ShowControls = true;
            option.PopupShowTrivial = true;
            option.StackMaxLength = 1000;
            (option.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
            option.SqlFormatter = new StackExchange.Profiling.SqlFormatters.SqlServerFormatter();
        }).AddEntityFramework();
    }

    public static void ConfiguerMapster()
    {
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
       // TypeAdapterConfig.GlobalSettings.Default.MaxDepth(1);
        TypeAdapterConfig.GlobalSettings.Default.IgnoreMember((member, side) =>
            side == MemberSide.Destination && member.SetterModifier == AccessModifier.Private);
    }

    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureLogging(logging => { logging.ClearProviders(); })
            .UseSerilog();
    }



    public static IApplicationBuilder UseLocalization(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<LocalizationMiddleware>();
    }

    public static void AddMatchingInterfaces(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        //services.AddScoped<typeof(IAsyncRepository<>), typeof(RepositoryBase<>) > ();
    }
}
