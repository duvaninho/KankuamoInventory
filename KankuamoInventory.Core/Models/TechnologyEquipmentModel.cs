using KankuamoInventory.Core.Enumerations;
namespace KankuamoInventory.Core.Models;

public class TechnologyEquipmentModel
{
	private const string? _AVAILABLE = "Disponible";
	private const string? _IN_USE = "En uso";
	private const string? _UNDER_REPAIR = "Bajo reparación";
	private const string? _DISPOSED = "Dado de baja";

	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string SerialNumber { get; set; }
	public DateTime AcquisitionDate { get; set; }
	public EquipmentState State { get; set; }
	public string StateName => GetTextValueFromEnum(State);
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

	public static string GetTextValueFromEnum(EquipmentState state)
	{
		string value = (state switch
		{
			EquipmentState.Available => _AVAILABLE,
			EquipmentState.InUse => _IN_USE,
			EquipmentState.UnderRepair => _UNDER_REPAIR,
			EquipmentState.Disposed => _DISPOSED,
			_ => _AVAILABLE
		})!;

		return value;
	}

	public static EquipmentState GetEnumValueFromText(string state)
	{
		EquipmentState value = state switch
		{
			_AVAILABLE => EquipmentState.Available,
			_IN_USE => EquipmentState.InUse,
			_UNDER_REPAIR => EquipmentState.UnderRepair,
			_DISPOSED => EquipmentState.Disposed,
			_ => EquipmentState.Available
		};

		return value;
	}

	public static IEnumerable<EnumTypeValue<EquipmentState>> GetEquipmentStates()
	{
		return new List<EnumTypeValue<EquipmentState>>
		{
			new EnumTypeValue<EquipmentState> {Value = EquipmentState.Available, Name = _AVAILABLE},
			new EnumTypeValue<EquipmentState> {Value = EquipmentState.InUse, Name = _IN_USE},
			new EnumTypeValue<EquipmentState> {Value = EquipmentState.UnderRepair, Name = _UNDER_REPAIR},
			new EnumTypeValue<EquipmentState> {Value = EquipmentState.Disposed, Name = _DISPOSED}
		};
	}
}

public class EnumTypeValue<T> where T : Enum
{
	public T Value { get; set; }
	public string Name { get; set; }
}