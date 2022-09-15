using System.Linq.Expressions;
using System.Reflection;

namespace Architecture.Database.Extension;

 public static class OrderExtensions
    {

        private static IOrderedQueryable<T> OrderBy<T>(
        this IQueryable<T> source,
        string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> source,
            string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }

        private static IOrderedQueryable<T> ThenBy<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }

        private static IOrderedQueryable<T> ThenByDescending<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }

        private static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        public static IQueryable<T> Order<T>(this IQueryable<T> source, string sortBy)
        {
            foreach (var orderPhrase in sortBy.Split(","))
            {
                if (!IsOrderdQuery(source))
                    source = orderPhrase.Split(":")[1] == "ASC" ? source.OrderBy(orderPhrase.Split(":")[0]) : source.OrderByDescending(orderPhrase.Split(":")[0]);
                else
                {
                    var orderdQuery = source as IOrderedQueryable<T>;
                    source = orderPhrase.Split(":")[1] == "ASC" ? orderdQuery.ThenBy(orderPhrase.Split(":")[0]) : orderdQuery.ThenByDescending(orderPhrase.Split(":")[0]);
                }
            }
            return source;
        }

        private static bool IsOrderdQuery<T>(IQueryable<T> src)
        {
            return src?.Expression?.ToString()?.Contains("OrderBy") ?? false;
        }
    }
