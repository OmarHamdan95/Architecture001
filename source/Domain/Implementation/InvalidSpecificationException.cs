namespace Architecture.Domain.Implementation;

public class InvalidSpecificationException: Exception
{
    public InvalidSpecificationException(string message)
        : base(message)
    {
    }
}
