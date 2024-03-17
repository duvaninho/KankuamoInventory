using KankuamoInventory.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace KankuamoInventory.Data;

public class KankuamoInventoryContext : DbContextBase
{
	public DbSet<EquipmentMovementModel> Movements { get; set; }
	public DbSet<TechnologyEquipmentModel> Equipments { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite("Data Source=KankuamoInventory.sqlite");
	}
}

public class DbContextBase : DbContext, IDbContext
{

	public void SetModified(object entity)
	{
		Entry(entity).State = EntityState.Modified;
	}
}