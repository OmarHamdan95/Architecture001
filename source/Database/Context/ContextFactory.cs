using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Architecture.Database;

public sealed class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<Context>();
        const string DbConnectionString = "Host=localhost;Port=5432;Database=Architecture;Username=architecture_user;Password=P@ssw0rd;";

        optionsBuilder.UseNpgsql(DbConnectionString);

        //optionsBuilder.UseNpgsql(DbConnectionString);

        return new Context(optionsBuilder.Options);
    }
}
