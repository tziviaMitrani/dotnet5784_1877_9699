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
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        public static readonly DependencyProperty EngineerProperty =
           DependencyProperty.Register("CurrentEngineer", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));
        public EngineerWindow(int Id=0)
        {
            InitializeComponent();
            if (Id == 0)
                CurrentEngineer = new BO.Engineer
                {
                    Id = 0,
                    Name = "",
                    Email = "",
                    Level = 0,
                    Cost = 0,
                    Task = null,
                };
            else
                CurrentEngineer = s_bl.Engineer.Read(Id)!;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
