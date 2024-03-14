using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KankuamoInventory.Core.Managers;
using KankuamoInventory.Core.Managers.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace KankuamoInventory.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnCreateEquipment_OnClick(object sender, RoutedEventArgs e)
        {
            CrudEquipment crudEquipment = new CrudEquipment();
            Content = crudEquipment;
        }
    }
}
