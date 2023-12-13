using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById(object id);

    Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    Task Insert(TEntity obj);
    Task Update(TEntity obj);
    Task Delete(TEntity obj);
}