using System.Linq.Expressions;

namespace KankuamoInventory.Core.Contracts;

public interface IGenericRepository<T> where T : class
{
	Task<T> FindAsync(object id);
	Task AddAsync(T entity);
	void Delete(T entity);
	void Edit(T entity);
	Task<IEnumerable<T>> GetAllAsync();
	Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
}
