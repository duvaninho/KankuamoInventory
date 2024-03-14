using System.Linq.Expressions;
using KankuamoInventory.Core;
using KankuamoInventory.Core.Contracts;
using Microsoft.EntityFrameworkCore;
namespace KankuamoInventory.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected IDbContext Db;
    protected readonly DbSet<T> Dbset;

    public GenericRepository(IDbContext context)
    {
        Db = context;
        Dbset = context.Set<T>();
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(Dbset.AsEnumerable());
    }

    public virtual Task<T> FindAsync(object id)
    {
        return Dbset.FindAsync(id).AsTask();
    }

    protected IQueryable<T> FindByAsQueryable(Expression<Func<T, bool>> predicate)
    {
        return Dbset.Where(predicate);
    }

    protected IQueryable<T> AsQueryable()
    {
        return Dbset.AsQueryable();
    }
    public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
        IEnumerable<T> query = Dbset.Where(predicate).AsEnumerable();
        return query;
    }
    public virtual IQueryable<T> FindBy(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = Dbset;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                 (new[]
                 {
                     ','
                 }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }
        else
        {
            return query;
        }
    }

    public T FindFirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        T query = Dbset.FirstOrDefault(predicate);
        return query;
    }
    public virtual async Task AddAsync(T entity)
    {
        await Dbset.AddAsync(entity);
    }

    public virtual void Delete(T entity)
    {
        Dbset.Remove(entity);
    }

    public virtual void Edit(T entity)
    {
        Db.SetModified(entity);
    }
    public virtual void DeleteRange(List<T> entities)
    {
        Dbset.RemoveRange(entities);
    }
    public virtual void AddRange(List<T> entities)
    {
        Dbset.AddRange(entities);
    }
}
