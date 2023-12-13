using Microsoft.EntityFrameworkCore.Storage;
using Repository.DbContexts;

namespace Repository;

public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
{
    public UnitOfWork(IUserRepository userRepository, IProductRepository productRepository,
        ApplicationDbContext dbContext, IBasketRepository basketRepository, ITokenRepository tokenRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
        ProductRepository = productRepository;
        BasketRepository = basketRepository;
        TokenRepository = tokenRepository;
    }

    private readonly ApplicationDbContext _dbContext;
    public IUserRepository UserRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IBasketRepository BasketRepository { get; }
    public ITokenRepository TokenRepository { get; }
    private IDbContextTransaction? _dbContextTransaction;


    public void CreateTransaction()
    {
        _dbContextTransaction = _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        _dbContextTransaction?.Commit();
    }

    public void Rollback()
    {
        _dbContextTransaction?.Rollback();
        _dbContextTransaction?.Dispose();
    }

    public async Task Save()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _dbContext.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}