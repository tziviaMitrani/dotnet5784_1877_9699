namespace PL;
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
using PL.Engineer;
using PL.Dependency;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ButtonEngineer_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }
    private void ButtonMessageBox(object sender, RoutedEventArgs e)
    {
        MessageBoxResult mbResulte=
                    MessageBox.Show("Do you want to reset?","Pay attention",MessageBoxButton.OKCancel,MessageBoxImage.Question);
        if(mbResulte == MessageBoxResult.OK)
            DalTest.Initialization.Do();
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {

    }
}
