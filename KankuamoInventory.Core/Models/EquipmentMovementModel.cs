using KankuamoInventory.Core.Enumerations;
namespace KankuamoInventory.Core.Models;

public class EquipmentMovementModel
{
	private const string _TRANSFER = "Transladado";
	private const string _LOAN = "Prestado";
	private const string _RETURN = "Regresado";
	private const string _MAINTENANCE = "En Manteniminto";
	private const string _DISPOSAL = "Dado de baja";
	public int Id { get; set; }
	public int EquipmentId { get; set; }
	public TechnologyEquipmentModel Equipment { get; set; }
	public DateTime MovementDate { get; set; }
	public MovementType Type { get; set; }
	public string TypeName =>
		Type switch
		{
			MovementType.Transfer => _TRANSFER,
			MovementType.Loan => _LOAN,
			MovementType.Return => _RETURN,
			MovementType.Maintenance => _MAINTENANCE,
			MovementType.Disposal => _DISPOSAL,
			_ => "Desconocido"
		};
	public string Description { get; set; }

	public static MovementType GetEnumValueFromText(string type)
	{
		MovementType value = type switch
		{
			_TRANSFER => MovementType.Transfer,
			_LOAN => MovementType.Loan,
			_RETURN => MovementType.Return,
			_MAINTENANCE => MovementType.Maintenance,
			_DISPOSAL => MovementType.Disposal,
			_ => MovementType.Unknown
		};

		return value;
	}

	public static IEnumerable<EnumTypeValue<MovementType>> GetEnumTypeValues()
	{
		return new List<EnumTypeValue<MovementType>>()
		{
			new EnumTypeValue<MovementType>() { Value = MovementType.Transfer, Name = _TRANSFER },
			new EnumTypeValue<MovementType>() { Value = MovementType.Loan, Name = _LOAN },
			new EnumTypeValue<MovementType>() { Value = MovementType.Return, Name = _RETURN },
			new EnumTypeValue<MovementType>() { Value = MovementType.Maintenance, Name = _MAINTENANCE },
			new EnumTypeValue<MovementType>() { Value = MovementType.Disposal, Name = _DISPOSAL }
		};
	}
}