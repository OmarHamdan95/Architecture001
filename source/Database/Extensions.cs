using DotNetCore.Domain;
using Microsoft.EntityFrameworkCore;
using SoftCircuits.Wordify;

namespace Architecture.Database;

public static class DbExtensions
{
    public static void ApplyConvention(this ModelBuilder modelBuilder, List<Type> types, string schemaName)
    {
        foreach (var type in types)
        {
            var entityConfig = modelBuilder.Entity(type);

            entityConfig.ToTable(type.ToTableName(),schemaName);

            // if (type.IsAssignableTo(typeof(ISoftDeletable)))
            // {
            //     entityConfig.AddSoftDeleteQueryFilter(type);
            // }

            var typeProperties = type.GetProperties();
            foreach (var property in typeProperties)
            {
                if (property.PropertyType.IsSubclassOf(typeof(ValueObject)))
                    entityConfig.OwnsOne(property.PropertyType, property.Name);
                // if (property.Name == "Id")
                //     entityConfig.HasKey( property.Name);

                // if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                //     entityConfig.Property(property.PropertyType, property.Name).HasConversion<DateUtcConverter>();
            }
        }
    }

    public static string ToTableName(this Type @this)
    {
        var name = @this.Name.Pluralize();
        return name;
    }
}
