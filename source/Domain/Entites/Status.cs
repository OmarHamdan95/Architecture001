namespace Architecture.Domain;

public class Status : LookupBase
{
    public Status(Name description, string code)
    {
        Description = description;
        Code = code;
    }

    public Status()
    {

    }
}
