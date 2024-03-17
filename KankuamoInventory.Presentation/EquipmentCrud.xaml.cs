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
		private const string _CREATE_EQUIPMENT_TITLE = "Crear Equipo";
		private readonly ITechnologyEquipmentManager _technologyEquipmentManager;
		private TechnologyEquipmentModel? _selectedItem;
		public EquipmentCrud(ITechnologyEquipmentManager technologyEquipmentManager)
		{
			_technologyEquipmentManager = technologyEquipmentManager;
			InitializeComponent();
			Title.Text = _CREATE_EQUIPMENT_TITLE;
			LoadCmbStateElements();
		}

		private void LoadCmbStateElements()
		{
			foreach (var state in TechnologyEquipmentModel.GetEquipmentStates())
			{
				var comboBoxItem = new ComboBoxItem { Content = state.Name, Tag = state.Value };
				CmbState.Items.Add(comboBoxItem);
			}

			if (_selectedItem is not null)
			{
				CmbState.SelectedValue = _selectedItem.State;
			}
		}

		public EquipmentCrud(ITechnologyEquipmentManager technologyEquipmentManager, TechnologyEquipmentModel? technologyEquipmentModel)
		{
			_technologyEquipmentManager = technologyEquipmentManager;
			InitializeComponent();
			Title.Text = "Modificar Equipo";
			SetItemToUpdate(technologyEquipmentModel);
			LoadCmbStateElements();
		}

		private void GoBack_OnClick(object sender, RoutedEventArgs e)
		{
			var mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow?.ChangeView(new MainPage(_technologyEquipmentManager));
		}

		private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
		{
			if (!DpAcquisitionDate.SelectedDate.HasValue)
			{
				MessageBox.Show("La fecha de adquisición es requerida");
				return;
			}

			var typeValue = CmbState.SelectedValue is EquipmentState selectedValue ? selectedValue : EquipmentState.Available;

			var state = typeValue;

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
				MessageBox.Show(resultCreation.Message);
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
			CmbState.SelectedValue = null;
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
				string textValueFromEnum = TechnologyEquipmentModel.GetTextValueFromEnum(_selectedItem.State);
				//TODO: Set ComboBox value
			}
		}
	}
}
