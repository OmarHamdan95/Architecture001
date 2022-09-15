using System.Globalization;
using System.Text;
using Architecture.Domain.Interfaces;
using SoftCircuits.Wordify;

namespace Architecture.Domain.Extension;

public static class Extensions
{
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this) =>
        @this.IsNullOrEmpty<T>().Not();

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this) =>
        @this == null || @this.Count<T>() == 0;

    public static bool Not(this bool @this) => !@this;
    public static bool Not<T>(this T @this, Func<T, bool> predicate) => !predicate(@this);

    public static bool IsNull<T>(this T @this) => (object)@this == null;

    public static bool IsNotNull<T>(this T @this) => (object)@this != null;

    public static bool In<T>(this T @this, params T[] list) =>
        ((IEnumerable<T>)list).Contains<T>(@this);

    public static bool NotIn<T>(this T @this, params T[] list) =>
        !((IEnumerable<T>)list).Contains<T>(@this);

    public static bool NotContains<TSource>(this IEnumerable<TSource> source, TSource value) =>
        !source.Contains<TSource>(value);

    public static bool Is<T>(this object @this) => @this is T;

    public static T As<T>(this object @this) => (T)@this;

    public static bool And(this bool @this, bool right) => @this & right;

    public static bool And(this bool @this, Func<bool> action) => @this && action();

    public static bool AndNot(this bool @this, bool right) => @this && !right;

    public static bool AndNot(this bool @this, Func<bool> action) => @this && !action();

    public static bool Or(this bool @this, bool right) => @this | right;

    public static bool Or(this bool @this, Func<bool> action) => @this || action();

    public static bool OrNot(this bool @this, bool right) => @this || !right;

    public static bool OrNot(this bool @this, Func<bool> action) => @this || !action();

    public static bool Xor(this bool @this, bool right) => @this ^ right;

    public static bool Xor(this bool @this, Func<bool> action) => @this ^ action();

    public static bool IsBefore(this DateTime @this, DateTime toCompareWith) => @this < toCompareWith;

    public static bool IsBefore(this DateTime? @this, DateTime toCompareWith)
    {
        DateTime? nullable = @this;
        DateTime dateTime = toCompareWith;
        return nullable.HasValue && nullable.GetValueOrDefault() < dateTime;
    }

    public static bool IsAfter(this DateTime @this, DateTime toCompareWith) => @this > toCompareWith;

    public static bool IsAfter(this DateTime? @this, DateTime toCompareWith)
    {
        DateTime? nullable = @this;
        DateTime dateTime = toCompareWith;
        return nullable.HasValue && nullable.GetValueOrDefault() > dateTime;
    }

    public static DateTime EndOfDay(this DateTime @this)
    {
        DateTime dateTime = @this.Date;
        dateTime = dateTime.AddDays(1.0);
        return dateTime.AddSeconds(-1.0);
    }

    public static DateTime? EndOfDay(this DateTime? @this)
    {
        if (!@this.HasValue)
            return new DateTime?();
        DateTime dateTime = @this.GetValueOrDefault().Date;
        dateTime = dateTime.AddDays(1.0);
        return new DateTime?(dateTime.AddSeconds(-1.0));
    }

    public static DateTime StartOfDay(this DateTime @this) => @this.Date.Date;

    public static DateTime? StartOfDay(this DateTime? @this) => @this?.Date.Date;

    public static bool IgnoreCaseEqual(this string @this, string compareOperand) =>
        @this.Equals(compareOperand, StringComparison.OrdinalIgnoreCase);

    // public static ISpecification<TEntity> IsActive<TEntity>(this ISpecification<TEntity> specification)
    //     where TEntity : IActivatable
    // {
    //     return specification.And(x => (x.ValidFrom == null ||
    //          DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc) >= x.ValidFrom) &&
    //          (x.ValidTo == null || DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc) <=
    //          x.ValidTo));
    // }

    public static string ToTableName(this Type @this)
    {
        var name = @this.Name.Pluralize();
        return name;
    }
    public static string ToUpperUnderscoreTableName(this Type @this)
    {
        var name = @this.Name.Pluralize();

        var builder = new StringBuilder(name.Length + Math.Min(2, name.Length / 5));
        var previousCategory = default(UnicodeCategory?);

        for (var currentIndex = 0; currentIndex < name.Length; currentIndex++)
        {
            var currentChar = name[currentIndex];
            if (currentChar == '_')
            {
                builder.Append('_');
                previousCategory = null;
                continue;
            }

            var currentCategory = char.GetUnicodeCategory(currentChar);
            switch (currentCategory)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                    if (
                        previousCategory == UnicodeCategory.SpaceSeparator
                        || previousCategory == UnicodeCategory.LowercaseLetter
                        || previousCategory != UnicodeCategory.DecimalDigitNumber
                        && previousCategory != null
                        && currentIndex > 0
                        && currentIndex + 1 < name.Length
                        && char.IsLower(name[currentIndex + 1])
                    )
                    {
                        builder.Append('_');
                    }

                    currentChar = char.ToLower(currentChar);
                    break;

                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (previousCategory == UnicodeCategory.SpaceSeparator)
                    {
                        builder.Append('_');
                    }

                    break;

                default:
                    if (previousCategory != null)
                    {
                        previousCategory = UnicodeCategory.SpaceSeparator;
                    }

                    continue;
            }

            builder.Append(currentChar);
            previousCategory = currentCategory;
        }

        return builder.ToString().ToUpper();
    }
}
