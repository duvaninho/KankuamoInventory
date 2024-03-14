using KankuamoInventory.Core;
using KankuamoInventory.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace KankuamoInventory.Data;

public class KankuamoInventoryContext : DbContext
{
    public KankuamoInventoryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<EquipmentMovementModel> Movements { get; set; }
    public DbSet<TechnologyEquipmentModel> Equipments { get; set; }
}