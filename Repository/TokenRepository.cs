using Domain;
using Repository.DbContexts;

namespace Repository;

public class TokenRepository : GenericRepository<Token>, ITokenRepository
{
    public TokenRepository(ApplicationDbContext context) : base(context)
    {
    }
}