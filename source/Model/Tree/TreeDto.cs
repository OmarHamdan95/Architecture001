namespace Architecture.Model.Tree;

public class TreeDto
{
    public long? Id { get; set; }
    public NameDto? Description { get; set; }
    public string? Code { get; set; }
    public TreeDto Parent { get; set; }
    public long? ParentId { get; set; }
    public List<TreeDto> FlateParent { get; set; }
}
