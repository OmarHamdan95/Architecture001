using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Architecture.Domain;
using Architecture.Domain.BaseDto;
using Architecture.Domain.Interfaces;


namespace Architecture.Database.Queries
{
    public interface ILookupsQuery
    {
        LookupDto Get<T>(Expression<Func<T, bool>> predicate) where T : LookupBase;
        Task<PagedData<LookupDto>> PageAsync<T>(ISpecification<T> specification, int pageIndex, int pageSize) where T : LookupBase;
        List<LookupDto> Search<T>(string text, int? limt) where T : LookupBase;
        Task<List<LookupDto>> SearchAsync<T>(string text, int? limt) where T : LookupBase;

        Task<List<LookupDto>> ListAsync<T>() where T : LookupBase;
        Task<List<LookupDto>> ListAsync<T>(ISpecification<T> specification , int take) where T : LookupBase;

        Task<List<LookupDto>> AutocompleteAcync<T>(string text, int take) where T : LookupBase;

    }
}
