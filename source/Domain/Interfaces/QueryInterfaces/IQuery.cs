namespace Architecture.Domain.Interfaces;

public interface IQuery<TResult>
{
    Task<TResult> QueryAsync();
}

public interface IQuery<TInput,TResult>
{
    Task<TResult> QueryAsync(TInput input);
}

public interface IQueryFactory
{
    IQuery<TInput, TResult> CreateQuery<TInput, TResult>();
    TQuery CreateQuery<TQuery>();
}

