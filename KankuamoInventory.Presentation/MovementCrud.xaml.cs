using KankuamoInventory.Core.Extensions;
using KankuamoInventory.Core.Managers;
using KankuamoInventory.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KankuamoInventory.Presentation
{
	/// <summary>
	/// Interaction logic for MovementCrud.xaml
	/// </summary>
	public partial class MovementCrud : UserControl
	{
		private readonly ITechnologyEquipmentManager _technologyEquipmentManager;
		private readonly TechnologyEquipmentModel? _technologyEquipmentModel;
		public ObservableCollection<EquipmentMovementModel> Movements { get; set; }

		public MovementCrud()
		{
			InitializeComponent();
		}

		public MovementCrud(ITechnologyEquipmentManager technologyEquipmentManager, TechnologyEquipmentModel? technologyEquipmentModel)
		{
			_technologyEquipmentManager = technologyEquipmentManager;
			_technologyEquipmentModel = technologyEquipmentModel;
			InitializeComponent();
			Title.Text = "Movimientos de " + technologyEquipmentModel?.Name;
			LoadMovementTypes();
			GetMovementsAsync().ToSync();
		}

		private void LoadMovementTypes()
		{
			foreach (var type in EquipmentMovementModel.GetEnumTypeValues())
			{
				var comboBoxItem = new ComboBoxItem { Content = type.Name, Tag = type.Value };
				CmbType.Items.Add(comboBoxItem);
			}
		}

		private async Task GetMovementsAsync()
		{
			var movementsResult = await _technologyEquipmentManager
				.GetMovementsByEquipmentIdAsync(_technologyEquipmentModel.Id);

			if (!movementsResult.SuccessfulOperation)
			{
				MessageBox.Show(movementsResult.Message);
				return;
			}
			GridMovements.Items.Clear();
			foreach (EquipmentMovementModel equipmentMovementModel in movementsResult.Data)
			{
				GridMovements.Items.Add(equipmentMovementModel);
			}
		}

		private void Save_OnClick(object sender, RoutedEventArgs e)
		{
			var movement = new EquipmentMovementModel
			{
				Description = TxtDescription.Text,
				MovementDate = DateTime.Now,
				Type = EquipmentMovementModel.GetEnumValueFromText(CmbType.Text)
			};

			var result = _technologyEquipmentManager
				.AddMovementsAsync(_technologyEquipmentModel, movement).ToSync();

			MessageBox.Show(result.Message);
			if (result.SuccessfulOperation)
			{
				GetMovementsAsync().ToSync();
			}

			TxtDescription.Text = string.Empty;
			CmbType.SelectedValue = null;
		}

		private void GoBack_OnClick(object sender, RoutedEventArgs e)
		{
			var mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow?.ChangeView(new MainPage(_technologyEquipmentManager));
		}
	}
}
