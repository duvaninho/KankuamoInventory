using KankuamoInventory.Core.Managers;
using System.Windows;
using System.Windows.Controls;

namespace KankuamoInventory.Presentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly ITechnologyEquipmentManager _technologyEquipmentManager;

		public MainWindow()
		{
			_technologyEquipmentManager = App.GetService<ITechnologyEquipmentManager>();
			InitializeComponent();
			Application.Current.MainWindow = this;
			Loaded += OnMainWindowLoaded;
		}

		private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
		{
			ChangeView(new MainPage(_technologyEquipmentManager));
		}

		public void ChangeView(Page view)
		{
			MainFrame.NavigationService.Navigate(view);
		}
	}
}
