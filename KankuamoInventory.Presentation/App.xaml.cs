using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KankuamoInventory.Core.Contracts;
using KankuamoInventory.Core.Managers;
using KankuamoInventory.Core.Managers.Implementation;
using KankuamoInventory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KankuamoInventory.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<KankuamoInventoryContext>((_, optionsBuilder) => optionsBuilder
                .UseInMemoryDatabase("KankuamoInventory"));

            // Build the service provider
            _serviceProvider = services.BuildServiceProvider();
        }

        private T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
