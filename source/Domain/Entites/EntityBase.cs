namespace Architecture.Domain;

public class EntityBase
{
    public long Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
}

public class LookupBase : EntityBase
{
}
