using System.Linq.Expressions;
using Architecture.Domain.BaseDto;

namespace Architecture.Domain.Interfaces;

public interface IQueryRepositoryBase<T> where T : class
{

    Task<int> CountAsync(ISpecification<T> specification);
    Task<T> GetByAsync(Expression<Func<T, bool>> criteria);
    Task<T> GetByAsync(ISpecification<T> specification);
    Task<ProjectionType> GetByAsync<ProjectionType>(Expression<Func<T, bool>> criteria);
    Task<ProjectionType> GetByAsync<ProjectionType>(ISpecification<T> specification);
    Task<T> GetByIdAsync(long id);
    Task<List<T>> ListAllAsync();
    Task<List<ProjectionType>> ListAllAsync<ProjectionType>();
    Task<List<T>> ListAsync(ISpecification<T> specification);
    Task<List<ProjectionType>> ListAsync<ProjectionType>(ISpecification<T> specification);
    Task<PagedData<T>> PageAsync(ISpecification<T> specification, int pagIndex, int pageSize, string sortBy = null);
    Task<PagedData<ProjectionType>> PageAsync<ProjectionType>(ISpecification<T> specification, int pagIndex, int pageSize, string sortBy = null);
}
