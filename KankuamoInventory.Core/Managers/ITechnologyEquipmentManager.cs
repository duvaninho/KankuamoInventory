using KankuamoInventory.Core.Models;
namespace KankuamoInventory.Core.Managers;

public interface ITechnologyEquipmentManager
{
    Task<ResultModel<string>> CreateTechnologyEquipment(TechnologyEquipmentModel technologyEquipment);
    Task<ResultModel<IEnumerable<TechnologyEquipmentModel>>> GetTechnologyEquipmentsAsync();
    Task<ResultModel<string>> AddMovementsAsync(int equipmentId, EquipmentMovementModel movement);
}
