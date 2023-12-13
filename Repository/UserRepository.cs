using Domain;
using Repository.DbContexts;

namespace Repository;

public class UserRepository : GenericRepository<ApplicationUser>,IUserRepository
{
    public UserRepository(ApplicationDbContext context):base(context)
    {
        
    }
}