using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Architecture.Domain;
using Architecture.Domain.Interfaces;

namespace Architecture.Database.Queries;

public interface IListQuery<TEntity> where TEntity : EntityBase
{
    Task<List<TEntity>> QueryAsync();
}

public class ListQuery<TEntity> : QueryBase,
    IQuery<List<TEntity>>, IListQuery<TEntity> where TEntity : EntityBase
{
    public ListQuery(IQueryContext readContext) : base(readContext)
    {
    }

    public async Task<List<TEntity>> QueryAsync()
    {
        return await Query<TEntity>().ToListAsync();
    }
}

public interface IListQuery<TEntity, TResult> where TEntity : EntityBase
{
    Task<List<TResult>> QueryAsync((ISpecification<TEntity> specification, Expression<Func<TEntity, TResult>> selector) input);
    Task<List<TResult>> QueryAsync((ISpecification<TEntity> specification, int take) input);
    Task<List<TEntity>> QueryAsync();
    Task<List<TResult>> QueryAsync(ISpecification<TEntity> specification);
}

public class ListQuery<TEntity, TResult> : QueryBase,
    IQuery<ISpecification<TEntity>, List<TResult>>,
    IQuery<(ISpecification<TEntity> specification, int take), List<TResult>>,
    IQuery<(ISpecification<TEntity> specification, Expression<Func<TEntity, TResult>> selector), List<TResult>>, IListQuery<TEntity, TResult> where TEntity : EntityBase
{
    public ListQuery(IQueryContext readContext) : base(readContext)
    {
    }
    public async Task<List<TResult>> QueryAsync(ISpecification<TEntity> specification)
    {
        var query = specification.HasPredicate ? specification.Prepare(Query<TEntity>()) : Query<TEntity>();
        return await query.ProjectToType<TResult>().ToListAsync();
    }


    public async Task<List<TEntity>> QueryAsync()
    {
        return await Query<TEntity>().ToListAsync();
    }

    public async Task<List<TResult>> QueryAsync((ISpecification<TEntity> specification, int take) input)
    {
        var query = input.specification.HasPredicate ? input.specification.Prepare(Query<TEntity>()) : Query<TEntity>();
        return await query.ProjectToType<TResult>().Take(input.take).ToListAsync();
    }

    public async Task<List<TResult>> QueryAsync((ISpecification<TEntity> specification, Expression<Func<TEntity, TResult>> selector) input)
    {
        var query = input.specification.HasPredicate ? input.specification.Prepare(Query<TEntity>()) : Query<TEntity>();
        return await query.Select(input.selector).ToListAsync();
    }
}
