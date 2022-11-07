namespace Architecture.Domain.Interfaces;

public interface IAsyncUnitOfWork : IDisposable
{
    Task EndAsync(CancellationToken cancellationToken = default);
    Task BeginAsync(CancellationToken cancellationToken = default);
    Task Rollback(CancellationToken cancellationToken = default);
}
