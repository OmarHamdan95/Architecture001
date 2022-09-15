using Architecture.Application;
using Architecture.Database;
using Architecture.Database.UnitOfWork;
using DotNetCore.AspNetCore;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.IoC;
using DotNetCore.Security;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Web;

public static class DIRegistration
{
    public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHashService();
        services.AddAuthenticationJwtBearer(new JwtSettings(Guid.NewGuid().ToString(), TimeSpan.FromHours(12)));
        services.AddResponseCompression();
        services.AddControllers().AddJsonOptions().AddAuthorizationPolicy();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddProfiler();

        services.AddContext(configuration);
        services.AddScoped<IAsyncUnitOfWork, UnitOfWork>();
        services.AddClassesMatchingInterfaces(typeof(IUserService).Assembly, typeof(IUserRepository).Assembly);

        Extintion.ConfiguerMapster();

        return services;
    }

    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var connectionString = configuration.GetConnectionString(nameof(Context));
        services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseNpgsql(connectionString));
        //services.AddDbContext<IntegrationEventLogContext>(x => x.UseLazyLoadingProxies().UseNpgsql(eventLogConnectionString));
    }
}
