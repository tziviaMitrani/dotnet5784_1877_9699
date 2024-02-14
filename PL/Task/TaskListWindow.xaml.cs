using BO;
using PL.Engineer;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        
        public TaskListWindow()
        {
            InitializeComponent();
            var task = s_bl?.Task.ReadAll();
            TaskList = task == null ? new() : new(task);
        }
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
   

        public ObservableCollection<BO.Task> TaskList
        {
            get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
        public BO.Status Status { get; set; } = BO.Status.All;

        private void ComboBox_SelectionByTaskStatus(object sender, SelectionChangedEventArgs e)
        {
            var temp = Status == BO.Status.All ?
            s_bl?.Task.ReadAll() :
            s_bl?.Task.ReadAll(item => (int)item!.Status == (int)Status);
            TaskList = temp == null ? new () : new (temp); 
        }

    private void ShowFormUpdate(object sender, RoutedEventArgs e)
        {
            BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;

            new TaskWindow(task!.Id).ShowDialog();

        }

        private void ShowAddForm(object sender, RoutedEventArgs e)
        {

            new TaskWindow().ShowDialog();
        }

    }
}
