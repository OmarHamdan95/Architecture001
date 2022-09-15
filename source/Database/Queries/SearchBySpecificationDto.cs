using Architecture.Domain.Interfaces;

namespace Architecture.Database.Queries;

public class SearchBySpecificationDto<T>
{

    public SearchBySpecificationDto()
    {

    }

    public SearchBySpecificationDto(ISpecification<T> searchSpecification)
    {
        Criteria = searchSpecification;
    }

    public SearchBySpecificationDto(ISpecification<T> searchSpecification,int pageIndex, int pageSize )
    {
        Criteria = searchSpecification;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    //public int Page { get; set; }
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public string SortBy { get; set; }

    public ISpecification<T> Criteria { get; set; }
}



