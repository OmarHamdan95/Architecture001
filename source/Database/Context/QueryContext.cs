using Architecture.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Database;

public class QueryContext : IQueryContext
{
    private readonly Context _context;

    public QueryContext(Context context)
    {
        _context = context;
    }
    public IQueryable<QueryType> Query<QueryType>() where QueryType : class
    {
        return _context.Set<QueryType>().AsNoTrackingWithIdentityResolution();
    }
}
