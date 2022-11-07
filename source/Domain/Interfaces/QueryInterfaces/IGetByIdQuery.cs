using System.Linq.Expressions;

namespace Architecture.Domain.Interfaces;

public interface IGetByIdQuery<TEntity, TResult> where TEntity : EntityBase
{
    Task<TResult> QueryAsync((long id, Expression<Func<TEntity, TResult>> selector) input);
    Task<TResult> QueryAsync(long id);
  
}
