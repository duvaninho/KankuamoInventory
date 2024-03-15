using KankuamoInventory.Core.Contracts;
using KankuamoInventory.Core.Managers;
using KankuamoInventory.Core.Managers.Implementation;
using KankuamoInventory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace KankuamoInventory.Presentation
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static IServiceProvider ServiceProvider { get; private set; }
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			ConfigureServices();
		}

		private void ConfigureServices()
		{
			var services = new ServiceCollection();

			// Register your services here
			services.AddTransient<ITechnologyEquipmentManager, TechnologyEquipmentManager>();// Example
			services.AddDbContext<KankuamoInventoryContext>((_, optionsBuilder) => optionsBuilder
				.UseInMemoryDatabase("KankuamoInventory"));

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IDbContext, KankuamoInventoryContext>();

			// Build the service provider
			ServiceProvider = services.BuildServiceProvider();
		}

		public static T GetService<T>()
		{
			return ServiceProvider.GetService<T>();
		}
	}
}
