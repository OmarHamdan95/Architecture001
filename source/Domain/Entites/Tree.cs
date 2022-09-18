using Architecture.Domain.BaseDto;

namespace Architecture.Domain;

public class Tree : EntityBase
{
    public Tree()
    {
    }

    public Tree(string code, Name description, long? parentId)
    {
        Description = description;
        Code = code;
        ParentId = parentId;
    }

    public virtual Name? Description { get; private set; }
    public virtual string? Code { get; private set; }
    public virtual Tree? Parent { get; private set; }
    public virtual long? ParentId { get; private set; }
    public virtual List<FlateParentTree> FlateParent { get; set; }
}

public class FlateParentTree : ViewEntityBase
{
    public virtual Tree Tree { get; set; }
    public virtual long? TreeId { get; set; }
    public virtual Tree Parent { get; set; }
    public virtual long? ParentId { get; set; }
    public virtual long? GrandParentId { get; set; }
}
