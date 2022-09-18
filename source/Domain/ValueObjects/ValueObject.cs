namespace Architecture.Domain;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject rigth)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(rigth, null))
        {
            return false;
        }

        return ReferenceEquals(left, null) || left.Equals(rigth);
    }


    protected static bool NotEqualOperator(ValueObject left, ValueObject rigth)
    {
        return !EqualOperator(left, rigth);
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public ValueObject getCopy()
    {
        return MemberwiseClone() as ValueObject;
    }
}
