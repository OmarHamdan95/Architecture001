namespace Architecture.Domain.BaseDto;

public class Result<T> : Result
{

}


public class Result
{
    public IList<string> ErrorMessages { get; set; }
}
