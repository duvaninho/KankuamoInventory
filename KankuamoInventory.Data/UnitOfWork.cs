using KankuamoInventory.Core;
using KankuamoInventory.Core.Contracts;
using Microsoft.EntityFrameworkCore;
namespace KankuamoInventory.Data;

public class UnitOfWork : IUnitOfWork
{
    private IDbContext _dbContext;
    public UnitOfWork(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Dispose()
    {
        Dispose(true);
    }
    
    public IGenericRepository<T> GenericRepository<T>() where T : class
    {
        return new GenericRepository<T>(_dbContext);
    }

    public Task<int> CommitAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
    
    private void Dispose(bool disposing)
    {
        if (disposing && _dbContext != null)
        {
            ((DbContext)_dbContext).Dispose();
            _dbContext = null;
        }
    }
}
