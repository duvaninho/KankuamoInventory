using KankuamoInventory.Core.Enumerations;
using KankuamoInventory.Core.Managers;
using KankuamoInventory.Core.Models;
using System.Windows;
using System.Windows.Controls;

namespace KankuamoInventory.Presentation
{
	/// <summary>
	/// Interaction logic for EquipmentCrud.xaml
	/// </summary>
	public partial class EquipmentCrud : UserControl
	{
		private const string? _AVAILABLE = "Disponible";
		private const string? _IN_USE = "En uso";
		private const string? _UNDER_REPAIR = "Bajo Reparación";
		private const string? _DISPOSED = "Dado de baja";
		private const string _CREATE_EQUIPMENT_TITLE = "Crear Equipo";
		private readonly ITechnologyEquipmentManager _technologyEquipmentManager;
		private TechnologyEquipmentModel? _selectedItem;
		public EquipmentCrud(ITechnologyEquipmentManager technologyEquipmentManager)
		{
			_technologyEquipmentManager = technologyEquipmentManager;
			InitializeComponent();
			Title.Text = _CREATE_EQUIPMENT_TITLE;
		}

		public EquipmentCrud(ITechnologyEquipmentManager technologyEquipmentManager, TechnologyEquipmentModel? technologyEquipmentModel)
		{
			_technologyEquipmentManager = technologyEquipmentManager;
			InitializeComponent();
			Title.Text = "Modificar Equipo";
			SetItemToUpdate(technologyEquipmentModel);
		}

		private void GoBack_OnClick(object sender, RoutedEventArgs e)
		{
			var mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow?.ChangeView(new MainPage(_technologyEquipmentManager));
		}

		private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
		{
			ComboBoxItem typeItem = (ComboBoxItem)CmbState.SelectedItem;
			string value = typeItem?.Content?.ToString() ?? _AVAILABLE;

			EquipmentState state = value switch
			{
				_AVAILABLE => EquipmentState.Available,
				_IN_USE => EquipmentState.InUse,
				_UNDER_REPAIR => EquipmentState.UnderRepair,
				_DISPOSED => EquipmentState.Disposed,
				_ => EquipmentState.Available
			};

			var equipment = new TechnologyEquipmentModel
			{
				Name = TxtName.Text,
				SerialNumber = TxtSerialNumber.Text,
				Description = TxtDescription.Text,
				AcquisitionDate = DpAcquisitionDate.SelectedDate.Value,
				State = state
			};

			ResultModel<string> resultCreation;
			if (Title.Text == _CREATE_EQUIPMENT_TITLE)
			{
				resultCreation = _technologyEquipmentManager.CreateTechnologyEquipment(equipment).ConfigureAwait(false).GetAwaiter().GetResult();
			}
			else
			{
				_selectedItem.Name = TxtName.Text;
				_selectedItem.SerialNumber = TxtSerialNumber.Text;
				_selectedItem.Description = TxtDescription.Text;
				_selectedItem.AcquisitionDate = DpAcquisitionDate.SelectedDate.Value;
				_selectedItem.State = state;
				resultCreation = _technologyEquipmentManager.UpdateTechnologyEquipment(_selectedItem).ConfigureAwait(false).GetAwaiter().GetResult();
				if (!resultCreation.SuccessfulOperation)
				{
					MessageBox.Show(resultCreation.Message);
				}
				GoBack_OnClick(default, default);
			}

			TxtName.Clear();
			TxtSerialNumber.Clear();
			TxtDescription.Clear();
			DpAcquisitionDate.SelectedDate = null;
			//TODO: Clear ComboBox
		}

		public void SetItemToUpdate(TechnologyEquipmentModel? selectedItem)
		{
			_selectedItem = selectedItem;
			if (_selectedItem is not null)
			{
				TxtDescription.Text = _selectedItem.Description;
				TxtName.Text = _selectedItem.Name;
				TxtSerialNumber.Text = _selectedItem.SerialNumber;
				DpAcquisitionDate.SelectedDate = _selectedItem.AcquisitionDate;
				string textValueFromEnum = GetTextValueFromEnum(_selectedItem.State);
				//TODO: Set ComboBox value
			}
		}

		private string GetTextValueFromEnum(EquipmentState state)
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
	}
}
