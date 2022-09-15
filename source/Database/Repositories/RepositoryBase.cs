using System.Linq.Expressions;
using Architecture.Database.DataBase;
using Architecture.Domain;
using Architecture.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Database.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly DBContextBase _dbContext;

    public RepositoryBase(DBContextBase dbContext)
    {
        _dbContext = dbContext;
    }

    protected virtual IQueryable<T> Query()
    {
        return _dbContext.Set<T>().AsQueryable<T>();
    }

    public virtual async Task<T> GetByIdAsync(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<T> GetByAsync(ISpecification<T> specification)
    {
        var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
        return await query.FirstOrDefaultAsync();
        //return await _dbContext.Set<T>().SingleOrDefaultAsync(specification);
    }

    public virtual async Task<T> GetByAsync(Expression<Func<T, bool>> criteria)
    {
        return await _dbContext.Set<T>().SingleOrDefaultAsync(criteria);
    }

    public virtual async Task<List<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<List<T>> ListAsync(ISpecification<T> specification)
    {
        var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
        return await query.ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        _dbContext.BeginTransaction();
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbContext.BeginTransaction();
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _dbContext.BeginTransaction();
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
