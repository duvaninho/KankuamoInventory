using KankuamoInventory.Core.Managers;
using KankuamoInventory.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace KankuamoInventory.Presentation;

public class TechnologyEquipmentViewModel : INotifyPropertyChanged
{
	private ObservableCollection<TechnologyEquipmentModel> _equipmentRecords;
	public ObservableCollection<TechnologyEquipmentModel> EquipmentRecords
	{
		get => _equipmentRecords;
		set
		{
			_equipmentRecords = value;
			OnPropertyChanged();
		}
	}

	private readonly ITechnologyEquipmentManager _equipmentService;

	public TechnologyEquipmentViewModel(ITechnologyEquipmentManager equipmentService)
	{
		_equipmentService = equipmentService;
		LoadEquipmentRecords();
	}

	public async Task LoadEquipmentRecords()
	{
		var technologyEquipments = await _equipmentService.GetTechnologyEquipmentsAsync();
		EquipmentRecords = new ObservableCollection<TechnologyEquipmentModel>(technologyEquipments.Data);
	}

	public event PropertyChangedEventHandler PropertyChanged;
	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
