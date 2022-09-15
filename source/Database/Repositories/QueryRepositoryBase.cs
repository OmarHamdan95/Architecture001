using System.Linq.Expressions;
using Architecture.Database.Extension;
using Architecture.Domain.BaseDto;
using Architecture.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Database.Repositories;

public class QueryRepositoryBase<T> : IQueryRepositoryBase<T> where T : class
{
    protected readonly DbContext _dbContext;

    public QueryRepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected virtual IQueryable<T> Query()
    {
        return _dbContext.Set<T>().AsQueryable<T>();
    }

    protected virtual IQueryable<QueryType> Query<QueryType>() where QueryType : class
    {
        return _dbContext.Set<QueryType>().AsQueryable<QueryType>();
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        if (specification.HasPredicate)
            query = query.Where(specification.Predicate);

        return await query.CountAsync();
    }

    public async Task<T> GetByAsync(Expression<Func<T, bool>> criteria)
    {
        return await _dbContext.Set<T>().SingleOrDefaultAsync(criteria);
    }

    public async Task<T> GetByAsync(ISpecification<T> specification)
    {
        var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
        return await query.FirstOrDefaultAsync();
    }

    public async Task<ProjectionType> GetByAsync<ProjectionType>(Expression<Func<T, bool>> criteria)
    {
        return await _dbContext.Set<T>().Where(criteria).ProjectToType<ProjectionType>().SingleAsync();
    }

    public async Task<ProjectionType> GetByAsync<ProjectionType>(ISpecification<T> specification)
    {
        var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
        return await query.ProjectToType<ProjectionType>().FirstOrDefaultAsync();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<List<ProjectionType>> ListAllAsync<ProjectionType>()
    {
        return await _dbContext.Set<T>().ProjectToType<ProjectionType>().ToListAsync();
    }

    public async Task<List<T>> ListAsync(ISpecification<T> specification)
    {
        var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
        return await query.ToListAsync();
    }

    public async Task<List<ProjectionType>> ListAsync<ProjectionType>(ISpecification<T> specification)
    {
        var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
        return await query.ProjectToType<ProjectionType>().ToListAsync();
    }

    public async Task<PagedData<T>> PageAsync(ISpecification<T> specification, int pagIndex, int pageSize,
        string sortBy = null)
    {
        var pagedData = new PagedData<T>();
        var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
        if (!string.IsNullOrEmpty(sortBy))
            query = query.Order<T>(sortBy);

        var queryResult = query.Skip(pagIndex * pageSize).Take(pageSize);
        pagedData.TotalCount = await CountAsync(specification);
        pagedData.Data = await queryResult.ToListAsync();
        return pagedData;
    }

    public async Task<PagedData<ProjectionType>> PageAsync<ProjectionType>(ISpecification<T> specification, int pagIndex,
        int pageSize, string sortBy = null)
    {
        PagedData<ProjectionType> pagedData = new PagedData<ProjectionType>();
        var query = specification.HasPredicate ? specification.Prepare(Query()) : Query();
        if (!string.IsNullOrEmpty(sortBy))
            query = query.Order<T>(sortBy);

        var queryResult = query.Skip(pagIndex * pageSize).Take(pageSize).ProjectToType<ProjectionType>();
        pagedData.TotalCount = await CountAsync(specification);
        pagedData.Data = await queryResult.ToListAsync();
        return pagedData;
    }
}
