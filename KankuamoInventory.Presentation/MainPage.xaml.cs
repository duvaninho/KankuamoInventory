using KankuamoInventory.Core.Managers;
using KankuamoInventory.Core.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace KankuamoInventory.Presentation
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		private readonly ITechnologyEquipmentManager _technologyEquipmentManager;
		public ObservableCollection<TechnologyEquipmentModel> Equipments { get; set; }

		public MainPage(ITechnologyEquipmentManager technologyEquipmentManager)
		{
			_technologyEquipmentManager = technologyEquipmentManager;
			InitializeComponent();
			FillDataGrid();
		}

		private void FillDataGrid()
		{
			var result = _technologyEquipmentManager
				.GetTechnologyEquipmentsAsync()
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();

			if (!result.SuccessfulOperation)
			{
				MessageBox.Show(result.Message);
				return;
			}

			foreach (TechnologyEquipmentModel technologyEquipmentModel in result.Data)
			{
				GridEquipments.Items.Add(technologyEquipmentModel);
			}
		}

		private void BtnCreateEquipment_OnClick(object sender, RoutedEventArgs e)
		{
			var crudEquipment = new EquipmentCrud(_technologyEquipmentManager);
			Content = crudEquipment;
		}

		private void SeeMovements_OnClick(object sender, RoutedEventArgs e)
		{
			var technologyEquipmentModel = GridEquipments.SelectedItem as TechnologyEquipmentModel;
			var movements = new MovementCrud(_technologyEquipmentManager, technologyEquipmentModel);
			Content = movements;
		}

		private void Modify_OnClick(object sender, RoutedEventArgs e)
		{
			var technologyEquipmentModel = GridEquipments.SelectedItem as TechnologyEquipmentModel;
			var crudEquipment = new EquipmentCrud(_technologyEquipmentManager, technologyEquipmentModel);
			Content = crudEquipment;
		}
	}
}
