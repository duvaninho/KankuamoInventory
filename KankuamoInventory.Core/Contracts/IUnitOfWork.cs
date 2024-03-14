namespace KankuamoInventory.Core.Contracts;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : class;
    Task<int> CommitAsync();
}