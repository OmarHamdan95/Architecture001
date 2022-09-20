using System.Reflection;
using Architecture.Database.Constants;
using Architecture.Database.DataBase;
using Architecture.Domain;
using Architecture.Domain.BaseDto;
using DotNetCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Database;

public sealed class Context : DBContextBase
{
    public Context(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //System.Diagnostics.Debugger.Launch();
        //System.Diagnostics.Debugger.Launch();
        base.OnModelCreating(builder);

        var domainTypes = Assembly
            .GetAssembly(typeof(Domain._IAssemblyMark))
            .GetTypes()
            .Where(
                myType =>
                    myType.IsClass && !myType.IsSealed
                                   && !myType.IsAbstract && myType.IsSubclassOf(typeof(EntityBase))
            )
            .ToList();


        builder.ApplyConvention(domainTypes, DatabaseConstants.DB_SCHEMA_NAME);

        builder.ApplyConfigurationsFromAssembly(typeof(Database._IAssemblyMark).Assembly);

        var viewDomainTypes = GetEntityViews();
        builder.AddViewEntityTypes(viewDomainTypes, DatabaseConstants.DB_SCHEMA_NAME,false);

        this.Seed(builder);
    }

    public void Seed(ModelBuilder builder)
    {

    }

    private static List<Type> GetEntityViews()
    {
        var viewDomainTypes = Assembly.GetAssembly(typeof(Domain._IAssemblyMark))
            .GetTypes().Where(mt => mt.IsClass && !mt.IsAbstract && mt.IsSubclassOf(typeof(ViewEntityBase)))
            .ToList();

        return viewDomainTypes;
    }
}
