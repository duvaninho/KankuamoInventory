using System.Windows;
using System.Windows.Controls;

namespace KankuamoInventory.Presentation;

public partial class CrudEquipment : Page
{
    public CrudEquipment()
    {
        InitializeComponent();
    }
    private void SaveClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Content = new MainWindow();
    }
    private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}

