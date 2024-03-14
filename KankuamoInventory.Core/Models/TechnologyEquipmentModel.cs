using KankuamoInventory.Core.Enumerations;
namespace KankuamoInventory.Core.Models;

public class TechnologyEquipmentModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public EquipmentState State { get; set; }
    // Other properties as needed

    // Relationship with EquipmentMovements
    public List<EquipmentMovementModel> Movements { get; set; }
    public void AddMovement(EquipmentMovementModel movement)
    {
        State = movement.Type switch
        {
            MovementType.Transfer => EquipmentState.InUse,
            MovementType.Loan => EquipmentState.InUse,
            MovementType.Return => EquipmentState.Available,
            MovementType.Maintenance => EquipmentState.UnderRepair,
            MovementType.Disposal => EquipmentState.Disposed,
            _ => throw new ArgumentException("Invalid movement type.")
        };
        
        if (Movements == null)
        {
            Movements = new List<EquipmentMovementModel>();
        }
        
        Movements.Add(movement);
    }
}