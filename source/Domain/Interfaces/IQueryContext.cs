namespace Architecture.Domain.Interfaces;

public interface IQueryContext
{
    IQueryable<QueryType> Query<QueryType>() where QueryType : class;
}
