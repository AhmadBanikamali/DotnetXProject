namespace Repository;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IProductRepository ProductRepository { get; }
    IBasketRepository BasketRepository { get; }
    ITokenRepository TokenRepository { get; }
    void CreateTransaction();

    void Commit();

    void Rollback();

    Task Save();
}