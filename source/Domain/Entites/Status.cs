namespace Architecture.Domain;

public class Status : LookupBase
{
    public string? Code { get; private set; }
    public Name Name { get; private set; }
}
