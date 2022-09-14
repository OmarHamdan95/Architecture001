using DotNetCore.Domain;

namespace Architecture.Domain;

public class Name : ValueObject
{

    public Name(string ar, string en) : this()
    {
        NameAr = ar;
        NameEn = en;
    }

    protected Name()
    {

    }

    public string NameAr { get; private set; }
    public string NameEn { get; private set; }
    protected override IEnumerable<object> Equals()
    {
        yield return NameAr;
        yield return NameEn;
    }
}
//public sealed record Name(string FirstName, string LastName);
