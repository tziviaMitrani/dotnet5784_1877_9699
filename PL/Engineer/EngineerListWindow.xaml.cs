using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerListWindow()
        {
            InitializeComponent();
            var engineerInList = s_bl?.EngineerInList.ReadAll();
            EngineerList = engineerInList == null ? new() : new(engineerInList);

        }

        public ObservableCollection<BO.EngineerInList> EngineerList
        {
            get { return (ObservableCollection<BO.EngineerInList>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.EngineerInList>), typeof(EngineerListWindow), new PropertyMetadata(null));
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.All;

        private void ComboBox_SelectionByEngineerExperience(object sender, SelectionChangedEventArgs e)
        {
            var temp = Experience == BO.EngineerExperience.All ?
            s_bl?.EngineerInList.ReadAll().OrderBy(item=>item.Id) :
            s_bl?.EngineerInList.ReadAll(item => (int)item!.Level == (int)Experience).OrderBy(item => item.Id);
            EngineerList = temp == null ? new() : new(temp);

        }

        private void ShowFormUpdate(object sender, RoutedEventArgs e)
        {
            BO.EngineerInList? engineer = (sender as ListView)?.SelectedItem as BO.EngineerInList;

            new EngineerWindow(engineer!.Id).ShowDialog();
            var engineerInList = s_bl?.EngineerInList.ReadAll();
            EngineerList = engineerInList == null ? new() : new(engineerInList);

        }

        private void ShowAddForm(object sender, RoutedEventArgs e)
        {
      
            new EngineerWindow().ShowDialog();
            var engineerInList = s_bl?.EngineerInList.ReadAll();
            EngineerList = engineerInList == null ? new() : new(engineerInList);
        }
        
    }
}
