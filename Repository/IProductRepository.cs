using Domain;
using Repository.DbContexts;

namespace Repository;

public interface IProductRepository : IGenericRepository<Product>
{
}

public interface IBasketRepository : IGenericRepository<Basket>
{
}

public class BasketRepository : GenericRepository<Basket>, IBasketRepository
{
    public BasketRepository(ApplicationDbContext context) : base(context)
    {
    }
}

public interface ITokenRepository : IGenericRepository<Token>
{
}