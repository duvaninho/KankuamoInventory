namespace KankuamoInventory.Core.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> FindAsync(object id);
    Task AddAsync(T entity);
    void Delete(T entity);
    void Edit(T entity);
    Task<IEnumerable<T>> GetAllAsync();
}
