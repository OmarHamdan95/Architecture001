using System.Linq.Expressions;
using Architecture.Domain;

namespace Architecture.Database.Extension;

public static class DbExtension
{
    public static IQueryable<TEntity> WhereEven<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, long>> property)
        where TEntity: EntityBase
    {
        var expression = Expression.Equal(
            Expression.Modulo(
                property.Body,
                Expression.Constant(2)),
            Expression.Constant(0));

        var methodCallExpression = Expression.Call(typeof (Queryable),
            "where",
            new Type[] {source.ElementType},
            source.Expression,
            Expression.Lambda<Func<TEntity, bool>>(expression, property.Parameters));

        return source.Provider.CreateQuery<TEntity>(methodCallExpression);
    }


    public static IQueryable<TEntity> StringStartWith<TEntity>(this IQueryable<TEntity> source ,
        Expression<Func<TEntity, string>> expression, string filter )
    {
        var  methodCallExpression = Expression.Lambda<Func<TEntity, bool>>(
            Expression.Call(
                expression.Body,
                "starts_with",
                null,
                Expression.Constant(filter)),
            expression.Parameters);

        return source.Provider.CreateQuery<TEntity>(methodCallExpression);
    }
}
