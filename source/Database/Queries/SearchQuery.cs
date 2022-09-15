using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Architecture.Database.Extension;
using Architecture.Domain;
using Architecture.Domain.BaseDto;
using Architecture.Domain.Extension;
using Architecture.Domain.Interfaces;

namespace Architecture.Database.Queries;


public interface ISearchQuery<TEntity, TResult> :
    IQuery<SearchBySpecificationDto<TEntity>, PagedData<TResult>>,
    IQuery<(SearchBySpecificationDto<TEntity> searchBySpecification, Expression<Func<TEntity, TResult>> selector), PagedData<TResult>>
    where TEntity : EntityBase
{
    //Task<PagedData<TResult>> QueryAsync(SearchBySpecificationDto<TEntity> searchBySpecification);
}

public class SearchQuery<TEntity, TResult> : QueryBase, ISearchQuery<TEntity, TResult>

    where TEntity : EntityBase
{
    public SearchQuery(IQueryContext readContext) : base(readContext)
    {
    }

    public async Task<PagedData<TResult>> QueryAsync(SearchBySpecificationDto<TEntity> searchBySpecification)
    {
        PagedData<TResult> pagedData = new PagedData<TResult>();
        var specification = searchBySpecification.Criteria;

        var query = specification.HasPredicate ? specification.Prepare(Query<TEntity>()) : Query<TEntity>();


        #region order by
        if (searchBySpecification.SortBy.IsNotNullOrEmpty())
            query = OrderExtensions.Order(query, searchBySpecification.SortBy);
        #endregion

        var queryProjected = query.Skip(searchBySpecification.PageIndex * searchBySpecification.PageSize)
                                .Take(searchBySpecification.PageSize).ProjectToType<TResult>();

        pagedData.TotalCount = await query.CountAsync();
        pagedData.Data = queryProjected.ToList();
        return pagedData;
    }

    public async Task<PagedData<TResult>> QueryAsync((SearchBySpecificationDto<TEntity> searchBySpecification, Expression<Func<TEntity, TResult>> selector) input)
    {
        PagedData<TResult> pagedData = new PagedData<TResult>();
        var specification = input.searchBySpecification.Criteria;

        var query = specification.HasPredicate ? specification.Prepare(Query<TEntity>()) : Query<TEntity>();

        #region order by
        if (input.searchBySpecification.SortBy.IsNotNullOrEmpty())
            query = OrderExtensions.Order(query, input.searchBySpecification.SortBy);
        #endregion

        var queryProjected = query.Skip(input.searchBySpecification.PageIndex * input.searchBySpecification.PageSize)
                                .Take(input.searchBySpecification.PageSize).Select(input.selector);

        pagedData.TotalCount = await query.CountAsync();
        pagedData.Data = queryProjected.ToList();
        return pagedData;
    }
}



