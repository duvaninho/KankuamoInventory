using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace KankuamoInventory.Data;

public interface IDbContext
{
	DbSet<T> Set<T>() where T : class;
	EntityEntry Entry(object entity);
	EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
	void SetModified(object entity);
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
