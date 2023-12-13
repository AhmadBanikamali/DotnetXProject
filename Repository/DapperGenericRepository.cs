using System.Data;
using System.Linq.Expressions;
using Dapper;

namespace Repository;

class DapperGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDapperContext _context;
    private readonly string _tableName;

    public DapperGenericRepository(ApplicationDapperContext context,string tableName)
    {
        _context = context;
        _tableName = tableName;
    }
    public Task<TEntity?> GetById(object id)
    {
        TEntity? item;

        using (var cn = _context.CreateConnection())
        {
            cn.Open();
            item = cn.Query<TEntity>("SELECT * FROM " + _tableName + " WHERE ID=@ID", new { ID = id }).SingleOrDefault();
        }

        return Task.FromResult(item);
    }

    public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
    {
        IEnumerable<TEntity>? items;

        using (var cn = _context.CreateConnection())
        {
            cn.Open();
            items = cn.Query<TEntity>("SELECT * FROM " + _tableName);
        }

        return Task.FromResult(items.ToList());
    }

    public Task Insert(TEntity obj)
    {
        throw new NotImplementedException();
    }

    public Task Update(TEntity obj)
    {
        throw new NotImplementedException();
    }

    public Task Delete(TEntity obj)
    {
        throw new NotImplementedException();
    }
}