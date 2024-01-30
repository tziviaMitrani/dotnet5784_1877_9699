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

        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.All;

        public static readonly DependencyProperty EngineerProperty =
           DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
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

        private void AddOrUpdateEngineerDetails(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button)!.Content.ToString()!;
            //this.ShowDialog();
            try
            {
                if (content == "Add")
                {

                    s_bl.Engineer.Create(CurrentEngineer);
                    MessageBox.Show("Done", "Engineer successfully added", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    s_bl.Engineer.Update(CurrentEngineer);
                    MessageBox.Show("Done", "The change has been made", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }catch (Exception ex)
            {

            }
      
            this.Close();

        }
    }
}
