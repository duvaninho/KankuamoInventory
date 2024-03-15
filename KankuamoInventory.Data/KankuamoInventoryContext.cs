using KankuamoInventory.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace KankuamoInventory.Data;

public class KankuamoInventoryContext : DbContextBase
{
	public KankuamoInventoryContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<EquipmentMovementModel> Movements { get; set; }
	public DbSet<TechnologyEquipmentModel> Equipments { get; set; }
}

public class DbContextBase : DbContext, IDbContext
{
	public DbContextBase(DbContextOptions options) : base(options)
	{

	}

	public void SetModified(object entity)
	{
		Entry(entity).State = EntityState.Modified;
	}
}