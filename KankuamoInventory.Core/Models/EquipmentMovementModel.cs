using KankuamoInventory.Core.Enumerations;
namespace KankuamoInventory.Core.Models;

public class EquipmentMovementModel
{
    public int Id { get; set; }
    public int EquipmentId { get; set; }
    public TechnologyEquipmentModel EquipmentModel { get; set; }
    public DateTime MovementDate { get; set; }
    public MovementType Type { get; set; }
    public string Description { get; set; }
}