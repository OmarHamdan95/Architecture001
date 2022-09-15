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
        //TypeAdapterConfig.GlobalSettings.Default.MaxDepth(3);
        TypeAdapterConfig.GlobalSettings.Default.IgnoreMember((member, side) =>
            side == MemberSide.Destination && member.SetterModifier == AccessModifier.Private);

        // TypeAdapterConfig.GlobalSettings.ForType<User, UserResultDto>()
        //     .Map(dest => dest.UserAddresses, src => src.UserAddresses);

        //TypeAdapterConfig.GlobalSettings.ForType<applicationEntity.Application, LookupDto>()
        //    .Map(dest => dest.Description, src => src.Name);

        //TypeAdapterConfig.GlobalSettings.ForType<Permission, LookupDto>()
        //    .Map(dest => dest.Description, src => src.Name);
    }

    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureLogging(logging => { logging.ClearProviders(); })
            .UseSerilog();
    }

    public static void ConfigureApp(this WebApplication app, IConfiguration configuration)
    {
        // Configure the HTTP request pipeline.
        app.UseException();
        app.UseHttps();

        app.UseRouting();
        app.UseResponseCompression();
        app.UseMiniProfiler();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseSwagger();
        app.UseSwaggerUI();
        //application.UseSpaAngular("Frontend", "start", "http://localhost:4200");

        app.Run();
    }
}
