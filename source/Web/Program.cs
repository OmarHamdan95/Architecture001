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

var builder = WebApplication.CreateBuilder();

builder.Host.UseSerilog();

builder.Services.AddHashService();
builder.Services.AddAuthenticationJwtBearer(new JwtSettings(Guid.NewGuid().ToString(), TimeSpan.FromHours(12)));
builder.Services.AddResponseCompression();
builder.Services.AddControllers().AddJsonOptions().AddAuthorizationPolicy();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProfiler();
//builder.Services.AddSpaStaticFiles("Frontend");
//builder.Services.AddContext<Context>(options => options.UseSqlServer(builder.Services.GetConnectionString(nameof(Context))));
builder.Services.AddDbContext<Context>(x =>
    x.UseLazyLoadingProxies().UseNpgsql(builder.Services.GetConnectionString(nameof(Context))));
// builder.Services.AddDbContextPool<Context>(o =>
// {
//     o.ReplaceService<IQueryCompilationContextFactory, QueryCompilationFactory>();
// });
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<Context>>();
builder.Services.AddClassesMatchingInterfaces(typeof(IUserService).Assembly, typeof(IUserRepository).Assembly);

var application = builder.Build();

application.UseException();
application.UseHttps();

application.UseRouting();
application.UseResponseCompression();
application.UseMiniProfiler();
application.UseAuthentication();
application.UseAuthorization();

application.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

application.UseSwagger();
application.UseSwaggerUI();
//application.UseSpaAngular("Frontend", "start", "http://localhost:4200");

application.Run();
