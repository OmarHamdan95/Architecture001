using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Architecture.Database.DataBase;

public class DBContextBase: DbContext
{
    protected IDbContextTransaction _transaction;
    protected readonly object _createTransactionLock;
    protected string? SchemaName = null;
    protected bool _enableMigrationDebug = false;

    public DBContextBase(DbContextOptions options) : base(options)
    {
        if(_enableMigrationDebug)
            System.Diagnostics.Debugger.Launch();

        // _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        // _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService)); ;
        _createTransactionLock = new object();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if(_enableMigrationDebug)
            System.Diagnostics.Debugger.Launch();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public IDbContextTransaction GetCurrentTransaction()
    {
        return _transaction;
    }

    public virtual void BeginTransaction()
    {
        if (_transaction == null)
        {
            lock (_createTransactionLock)
            {
                if (_transaction == null)
                {
                    _transaction = Database.BeginTransaction();
                }
            }
        }
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null) return;

        try
        {
            await SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollbackTransaction();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransaction()
    {
        if (_transaction == null) return;

        try
        {
            await _transaction?.RollbackAsync();
        }
        finally
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
