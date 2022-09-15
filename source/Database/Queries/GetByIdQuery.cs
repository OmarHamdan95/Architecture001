using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Architecture.Domain;
using Architecture.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace Architecture.Database.Queries;

public interface IGetByIdQuery<TEntity, TResult> where TEntity : EntityBase
{
    Task<TResult> QueryAsync((long id, Expression<Func<TEntity, TResult>> selector) input);
    Task<TResult> QueryAsync(long id);
    // Task<TResult> QueryAsyncAdapt(long id);
    // Task<TResult> QueryAsync(long id , string expression);
}

public class GetByIdQuery<TEntity, TResult> : QueryBase,
    IQuery<long, TResult>,
    IQuery<(long id, Expression<Func<TEntity, TResult>> selector), TResult>,
    IGetByIdQuery<TEntity, TResult> where TEntity : EntityBase
{
    public GetByIdQuery(IQueryContext readContext) : base(readContext)
    {
    }

    public virtual Task<TResult> QueryAsync(long id)
    {
        return this.Query<TEntity>()
            .Where(q => q.Id == id)
            .ProjectToType<TResult>()
            .FirstOrDefaultAsync();
    }

    // public async Task<TResult> QueryAsyncAdapt(long id)
    // {
    //     return this.Query<TEntity>()
    //         .FirstOrDefault(q => q.Id == id)
    //         //.FirstOrDefault()
    //         .Adapt<TResult>();
    //
    // }

    public virtual Task<TResult> QueryAsync((long id, Expression<Func<TEntity, TResult>> selector) input)
    {
        return this.Query<TEntity>()
            .Where(q => q.Id == input.id)
            .Select(input.selector)
            .FirstOrDefaultAsync();
    }

    // public Task<TResult> QueryAsync(long id, string expression)
    // {
    //     var query = this.Query<TEntity>();
    //
    //     var includes = expression.Split(';');
    //
    //     foreach (string include in includes)
    //         query = query.Include(include);
    //
    //     return query
    //         .Where(q => q.Id == id)
    //         .ProjectToType<TResult>()
    //         .FirstOrDefaultAsync();
    // }
}



