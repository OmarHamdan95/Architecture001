namespace Architecture.Domain;

public class Role : LookupBase
{
    public Role(Name description, string code)
    {
        Description = description;
        Code = code;
    }

    public Role()
    {

    }
}
