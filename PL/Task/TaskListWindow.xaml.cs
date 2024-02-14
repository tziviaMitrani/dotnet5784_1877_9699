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
            var taskInList = s_bl?.TaskInList.ReadAll();
            TaskList = taskInList == null ? new() : new(taskInList);
        }
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public ObservableCollection<BO.TaskInList> TaskList
        {
            get { return (ObservableCollection<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.All;

        private void ComboBox_SelectionByTaskStatus(object sender, SelectionChangedEventArgs e)
        {
            var temp = Experience == BO.EngineerExperience.All ?
            s_bl?.TaskInList.ReadAll().OrderBy(item => item.Id) :
            s_bl?.TaskInList.ReadAll(item => (int)item!.Difficulty == (int)Experience).OrderBy(item => item.Id);
            TaskList = temp == null ? new() : new(temp);
        }


        //private void ComboBox_SelectionByEngineerExperience(object sender, SelectionChangedEventArgs e)
        //{
        //    var temp = Experience == BO.EngineerExperience.All ?
        //    s_bl?.Task.ReadAll() :
        //    s_bl?.Task.ReadAll(item => (int)item!.Difficulty == (int)Experience);

        //}
        private void ShowFormUpdate(object sender, RoutedEventArgs e)
        {
            BO.TaskInList? task = (sender as ListView)?.SelectedItem as BO.TaskInList;

            new TaskWindow(task!.Id).ShowDialog();
            var taskInList = s_bl?.TaskInList.ReadAll();
            TaskList = taskInList == null ? new() : new(taskInList);

        }

        private void ShowAddForm(object sender, RoutedEventArgs e)
        {

            new TaskWindow().ShowDialog();
            var taskInList = s_bl?.TaskInList.ReadAll();
            TaskList = taskInList == null ? new() : new(taskInList);
        }

    }
}
