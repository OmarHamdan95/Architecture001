namespace Architecture.Domain.BaseDto;

public class PagedData<T>
{
    public IList<T> Data { get; set; }
    public int TotalCount { get; set; }
}
