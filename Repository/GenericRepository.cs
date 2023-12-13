using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;

namespace Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetById(object id)
    {
        return await _dbSet.FindAsync(id);
    }


    public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
    {
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query).ToListAsync() : query.ToListAsync();
        }
    }

    public async Task Insert(TEntity obj)
    {
        await _dbSet.AddAsync(obj);
    }

    public Task Update(TEntity obj)
    {
        if (_context.Entry(obj).State == EntityState.Detached)
            _dbSet.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task Delete(TEntity obj)
    {
        if (_context.Entry(obj).State == EntityState.Detached) 
            _dbSet.Attach(obj);
        
        _context.Entry(obj).State = EntityState.Deleted;
        return Task.CompletedTask;
    }
}