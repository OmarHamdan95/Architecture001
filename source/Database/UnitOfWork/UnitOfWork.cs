using Architecture.Database.DataBase;
using Architecture.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Architecture.Database.UnitOfWork;

public class UnitOfWork : IAsyncUnitOfWork
{
    private readonly DBContextBase _dbContext;
    private readonly ILogger<UnitOfWork> _logger;

    public Guid GetCurrentTransactionId => _dbContext.GetCurrentTransaction().TransactionId;

    public UnitOfWork(DBContextBase dbContext , ILogger<UnitOfWork> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Dispose()
    {
        this._dbContext.Dispose();
    }

    public async Task EndAsync(CancellationToken cancellationToken = default)
    {
        if(_dbContext.GetCurrentTransaction() ==null) return;
        try
        {
            var transactionId = _dbContext.Database.CurrentTransaction.TransactionId;
            await _dbContext.SaveChangesAsync();
            await _dbContext.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Transaction");
            throw;
        }
    }

    public async Task BeginAsync(CancellationToken cancellationToken = default)
    {
        _dbContext.BeginTransaction();
        await Task.CompletedTask;
    }

    public async Task Rollback(CancellationToken cancellationToken = default)
    {
        await _dbContext.RollbackTransactionAsync();
    }
}
