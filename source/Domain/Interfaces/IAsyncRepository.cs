using System.Linq.Expressions;

namespace Architecture.Domain.Interfaces;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<T> GetByIdAsync(long id);
    Task<T> GetByAsync(ISpecification<T> specification);
    Task<T> GetByAsync(Expression<Func<T, bool>> criteria);
    Task<List<T>> ListAllAsync();
    Task<List<T>> ListAsync(ISpecification<T> specification);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
