namespace Architecture.Domain;

public class EntityBase
{
    public virtual long Id { get; set; }
    public virtual string CreatedBy { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual string ModifiedBy { get; set; }
    public virtual DateTime ModifiedDate { get; set; }
    public virtual bool IsDeleted { get; set; }
}

public class LookupBase : EntityBase
{
    public string? Code { get; private set; }
    public Name Description { get; set; }
}
