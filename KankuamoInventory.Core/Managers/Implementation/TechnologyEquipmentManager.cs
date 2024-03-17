using KankuamoInventory.Core.Contracts;
using KankuamoInventory.Core.Models;
namespace KankuamoInventory.Core.Managers.Implementation;

public class TechnologyEquipmentManager : ITechnologyEquipmentManager
{
	private const string _THERE_WAS_AN_UNEXPECTED_ERROR = "Ha ocurrido un error inesperado";
	private readonly IUnitOfWork _unitOfWork;
	public TechnologyEquipmentManager(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResultModel<string>> CreateTechnologyEquipment(TechnologyEquipmentModel technologyEquipment)
	{
		var result = new ResultModel<string>()
		{
			SuccessfulOperation = true,
			Message = "Equipo registrado con exito",
		};

		try
		{
			await _unitOfWork.GenericRepository<TechnologyEquipmentModel>().AddAsync(technologyEquipment);
			await _unitOfWork.CommitAsync();
			result.SuccessfulOperation = true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			result.SuccessfulOperation = false;
			result.Message = _THERE_WAS_AN_UNEXPECTED_ERROR;
		}

		return result;
	}

	public async Task<ResultModel<string>> UpdateTechnologyEquipment(TechnologyEquipmentModel technologyEquipment)
	{
		var result = new ResultModel<string>()
		{
			SuccessfulOperation = true,
			Message = "Equipo actualizado con exito",
		};

		try
		{
			_unitOfWork.GenericRepository<TechnologyEquipmentModel>().Edit(technologyEquipment);
			await _unitOfWork.CommitAsync();
			result.SuccessfulOperation = true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			result.SuccessfulOperation = false;
			result.Message = _THERE_WAS_AN_UNEXPECTED_ERROR;
		}

		return result;
	}

	public async Task<ResultModel<IEnumerable<EquipmentMovementModel>>> GetMovementsByEquipmentIdAsync(int equipmentId)
	{
		var result = new ResultModel<IEnumerable<EquipmentMovementModel>>()
		{
			SuccessfulOperation = true,
			Message = string.Empty,
			Data = Enumerable.Empty<EquipmentMovementModel>()
		};

		try
		{
			var equipments = await _unitOfWork
				.GenericRepository<EquipmentMovementModel>()
				.FindByAsync(t => t.EquipmentId == equipmentId);
			result.Data = equipments.OrderByDescending(t => t.Id);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			result.SuccessfulOperation = false;
			result.Message = _THERE_WAS_AN_UNEXPECTED_ERROR;
		}

		return result;
	}

	public async Task<ResultModel<IEnumerable<TechnologyEquipmentModel>>> GetTechnologyEquipmentsAsync()
	{
		var result = new ResultModel<IEnumerable<TechnologyEquipmentModel>>()
		{
			SuccessfulOperation = true,
			Message = string.Empty,
			Data = Enumerable.Empty<TechnologyEquipmentModel>()
		};

		try
		{
			var equipments = await _unitOfWork.GenericRepository<TechnologyEquipmentModel>().GetAllAsync();
			result.Data = equipments.OrderByDescending(t => t.Id);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			result.SuccessfulOperation = false;
			result.Message = _THERE_WAS_AN_UNEXPECTED_ERROR;
		}

		return result;
	}

	public async Task<ResultModel<string>> AddMovementsAsync(TechnologyEquipmentModel? equipment, EquipmentMovementModel movement)
	{
		var result = new ResultModel<string>()
		{
			SuccessfulOperation = true,
			Message = "Movimiento agregado con éxito"
		};

		try
		{
			if (equipment is not null)
			{
				equipment.AddMovement(movement);
				result.SuccessfulOperation = true;

				await _unitOfWork.CommitAsync();
			}
			else
			{
				result.Message = "El equipo no pudo ser encontrado";
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			result.SuccessfulOperation = false;
			result.Message = _THERE_WAS_AN_UNEXPECTED_ERROR;
		}

		return result;
	}
}
