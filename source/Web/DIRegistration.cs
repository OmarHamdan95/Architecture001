// using Architecture.Database;
//
// namespace Architecture.Web;
//
// public static class DIRegistration
// {
//     public static void AddContext(this IServiceCollection services, IConfiguration configuration)
//     {
//         AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//
//         var connectionString = databaseConfiguration.GetConnectionString(nameof(Context));
//         services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseNpgsql(connectionString));
//         services.AddDbContext<IntegrationEventLogContext>(x => x.UseLazyLoadingProxies().UseNpgsql(eventLogConnectionString));
//     }
// }
