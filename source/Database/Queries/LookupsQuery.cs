using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Architecture.Domain;
using Architecture.Domain.BaseDto;
using Architecture.Domain.Extension;
using Architecture.Domain.Interfaces;

namespace Architecture.Database.Queries;

public class LookupsQuery : QueryBase, ILookupsQuery
{
    public LookupsQuery(IQueryContext readContext) : base(readContext)
    {
    }

    public LookupDto Get<T>(Expression<Func<T, bool>> predicate) where T : LookupBase
    {
        return Query<T>()
            .Where(predicate)
            .ProjectToType<LookupDto>()
            .FirstOrDefault();
    }

    protected async Task<int> CountAsync<T>(ISpecification<T> specification) where T : LookupBase
    {
        var query = Query<T>().AsQueryable();

        // modify the IQueryable using the specification's criteria expression
        if (specification.Predicate != null)
        {
            query = query.Where(specification.Predicate);
        }

        return await query.CountAsync();
    }

    public async Task<PagedData<LookupDto>> PageAsync<T>(ISpecification<T> specification, int pageIndex, int pageSize) where T : LookupBase
    {
        var pagedData = new PagedData<LookupDto>();

        var query = specification.Predicate != null ? specification.Prepare(Query<T>()) : Query<T>();

        var queryProjected = query.Skip(pageIndex * pageSize).Take(pageSize);
        pagedData.TotalCount = await CountAsync(specification);
        pagedData.Data = await queryProjected.ProjectToType<LookupDto>().ToListAsync();
        return pagedData;
    }

    public List<LookupDto> Search<T>(string text, int? limt) where T : LookupBase
    {
        var query = getSearchQuery<T>(text, limt);

        var result = query
            .ProjectToType<LookupDto>()
            .ToList();

        return result;
    }

    protected IQueryable<T> getSearchQuery<T>(string text, int? limt) where T : LookupBase
    {
        var query = Query<T>();
        if (string.IsNullOrEmpty(text) == false)
        {
            query = query.Where(l => l.Description.NameAr.Contains(text) ||
                                        l.Description.NameEn.Contains(text));
        }

        if (limt != null)
        {
            query = query.Take(limt.Value);
        }

        return query;
    }

    public async Task<List<LookupDto>> SearchAsync<T>(string text, int? limt) where T : LookupBase
    {
        var query = getSearchQuery<T>(text, limt);

        var result = await query
            .ProjectToType<LookupDto>()
            .ToListAsync();

        return result;
    }


    public async Task<List<LookupDto>> ListAsync<T>() where T : LookupBase
    {
        var query = Query<T>();
        var result = await query
            .ProjectToType<LookupDto>()
            .ToListAsync();

        return result;
    }

    public async Task<List<LookupDto>> ListAsync<T>(ISpecification<T> specification , int take) where T : LookupBase
    {
        var query = specification.Predicate != null ? specification.Prepare(Query<T>()) : Query<T>();
        var result = await query
            .Take(take)
            .ProjectToType<LookupDto>()
            .ToListAsync();

        return result;
    }

    public async Task<List<LookupDto>> AutocompleteAcync<T>(string text, int take) where T : LookupBase
    {
        var query = Query<T>();
        if(text.IsNotNullOrEmpty())
            query = query.Where(x => x.Code.Contains(text) ||
                                x.Description.NameAr.Contains(text) ||
                                x.Description.NameEn.Contains(text)
                            );

        var result = await query
            .Take(take)
            .ProjectToType<LookupDto>()
            .ToListAsync();

        return result;
    }
}
