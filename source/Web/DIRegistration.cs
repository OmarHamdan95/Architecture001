using Architecture.Application;
using Architecture.Database;
using Architecture.Database.DataBase;
using Architecture.Database.Queries;
using Architecture.Database.Queries.QueriesCustome;
using Architecture.Database.UnitOfWork;
using Architecture.Domain.Interfaces;
using Architecture.Web.ActionFilter;
using DotNetCore.AspNetCore;
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
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(UnitOfWorkActionFilter));
        }).AddJsonOptions().AddAuthorizationPolicy();
        services.AddApiCorsSupport();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddProfiler();

        // services.AddControllers(config =>
        // {
        //     config.Filters.Add(new UnitOfWorkActionFilter());
        // });

        services.AddContext(configuration);

        services.AddMatchingInterfaces(configuration);



        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserFactory, UserFactory>();
        // services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthFactory, AuthFactory>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAsyncUnitOfWork, UnitOfWork>();


        //services.AddClassesMatchingInterfaces(typeof(IUserService).Assembly, typeof(IUserRepository).Assembly);
        //services.AddClassesMatchingInterfaces(typeof(ILookupsQuery).Assembly, typeof(LookupsQuery).Assembly);
        services.AddQueries();

        Extintion.ConfiguerMapster();

        return services;
    }

    public static void AddApiCorsSupport(this IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy("_allowOrigins", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
    }
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var connectionString = configuration.GetConnectionString(nameof(Context));
        services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseNpgsql(connectionString));
        //services.AddDbContext<Context>(x => x.UseLazyLoadingProxies().UseSqlServer(connectionString));

        services.AddScoped<DBContextBase>(provide => provide.GetService<Context>());
        services.AddScoped<IQueryContext, QueryContext>();
        //services.AddDbContext<IntegrationEventLogContext>(x => x.UseLazyLoadingProxies().UseNpgsql(eventLogConnectionString));
    }

    public static void ConfigureApp(this WebApplication app, IConfiguration configuration)
    {
        // Configure the HTTP request pipeline.
        app.UseException();
        app.UseHttps();

        app.UseRouting();
        app.UseResponseCompression();
        app.UseLocalization();
        app.UseMiniProfiler();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("_allowOrigins");

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseSwagger();
        app.UseSwaggerUI();
        //application.UseSpaAngular("Frontend", "start", "http://localhost:4200");

        app.Run();
    }

    public static void AddQueries(this IServiceCollection service)
    {
        service.AddScoped(typeof(ISearchQuery<,>), typeof(SearchQuery<,>));
        service.AddScoped(typeof(IListQuery<,>), typeof(ListQuery<,>));
        service.AddScoped(typeof(IGetByIdQuery<,>), typeof(GetByIdQuery<,>));
        service.AddScoped<ITreeQuery, TreeQuery>();

        //service.AddScoped<Ilook>()
        //service.AddScoped(typeof(ILookupsQuery), typeof(LookupsQuery));
    }
}
