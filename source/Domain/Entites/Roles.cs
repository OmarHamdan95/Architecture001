namespace Architecture.Domain;

public class Role : LookupBase
{
    public string? Code { get; private set; }
    public Name Name { get; private set; }
}
