namespace Architecture.Domain;


public interface IEntityBase
{
    public  long Id { get; set; }
}
public class EntityBase : IEntityBase
{
    public virtual long Id { get; set; }
    public virtual string CreatedBy { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual string ModifiedBy { get; set; }
    public virtual DateTime ModifiedDate { get; set; }
    public virtual bool IsDeleted { get; set; }
}

public abstract class LookupBase : EntityBase
{
    public LookupBase()
    {

    }

    protected LookupBase(long id)
    {
        Id = id;
    }
    public string Code { get; set; }
    public Name Description { get; set; }
    public virtual DateTime? ValidFrom { get; set; }
    public virtual DateTime? ValidTo { get; set; }

    public virtual bool IsActive
    {
        get { return (ValidFrom == null || DateTime.Now >= ValidFrom) && (ValidTo == null || DateTime.Now <= ValidTo); }
    }
    public override bool Equals(object obj)
    {
        var otherValue = obj as LookupBase;

        if (otherValue == null)
            return false;

        var typeMatches = GetType().Equals(obj.GetType());

        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public int CompareTo(object other) => Id.CompareTo(((LookupBase)other).Id);
}
