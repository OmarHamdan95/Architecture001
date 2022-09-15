using System;
using System.Linq;
using System.Threading.Tasks;
using Architecture.Domain.Interfaces;

namespace Architecture.Database.Queries
{
    public abstract class QueryBase //: IQuery<TInput, TResult>
    {
        protected readonly IQueryContext _readContext;

        public QueryBase(IQueryContext readContext)
        {
            _readContext = readContext;
        }

        protected virtual IQueryable<QueryType> Query<QueryType>() where QueryType : class
        {
            return _readContext.Query<QueryType>();
        }

    }


    public class QueryFactory : IQueryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IQuery<TInput, TResult> CreateQuery<TInput, TResult>()
        {
            return (IQuery<TInput, TResult>)_serviceProvider.GetService(typeof(IQuery<TInput, TResult>));
        }

        public TQuery CreateQuery<TQuery>()
        {
            return (TQuery) _serviceProvider.GetService(typeof(TQuery));
        }
    }
}
