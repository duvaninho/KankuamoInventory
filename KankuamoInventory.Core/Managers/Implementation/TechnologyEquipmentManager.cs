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
			Message = string.Empty,
		};

		try
		{
			await _unitOfWork.GenericRepository<TechnologyEquipmentModel>().AddAsync(technologyEquipment);
			await _unitOfWork.CommitAsync();
			result.SuccessfulOperation = true;
			result.Message = "Equipo registrado con exito";
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
			Message = string.Empty,
		};

		try
		{
			_unitOfWork.GenericRepository<TechnologyEquipmentModel>().Edit(technologyEquipment);
			await _unitOfWork.CommitAsync();
			result.SuccessfulOperation = true;
			result.Message = "Equipo actualizado con exito";
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
			result.Data = equipments;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			result.SuccessfulOperation = false;
			result.Message = _THERE_WAS_AN_UNEXPECTED_ERROR;
		}

		return result;
	}

	public async Task<ResultModel<string>> AddMovementsAsync(int equipmentId, EquipmentMovementModel movement)
	{
		var result = new ResultModel<string>()
		{
			SuccessfulOperation = true,
			Message = string.Empty
		};

		try
		{
			var equipment = await _unitOfWork.GenericRepository<TechnologyEquipmentModel?>().FindAsync(equipmentId);

			if (equipment is not null)
			{
				equipment.AddMovement(movement);
				result.SuccessfulOperation = true;
				result.Message = "Movimiento agregado con éxito";

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
