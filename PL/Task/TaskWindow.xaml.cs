using PL.Engineer;
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
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public TaskWindow(int Id = 0)
        {
            InitializeComponent();
            if (Id == 0)
                CurrentTask = new BO.Task
                {
                    Id = 0,
                    Description = "",
                    Alias = "",
                    //Milestone = null,
                    CreatedAtDate = null,
                    Status = BO.Status.All,
                    Dependencies = new List<BO.TaskInList>() { },
                    ForecastDate = null,
                    StartDate = null,
                    ScheduledDate = null,
                    DeadlineDate = null,
                    CompleteDate = null,
                    Deliverables="",
                    Remarks = "",
                    Engineer = new BO.EngineerInTask(),
                    Complexity = 0
                };
            else
                CurrentTask = s_bl.Task.Read(Id)!;

        }

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public BO.Status Status { get; set; } = BO.Status.All;

        public static readonly DependencyProperty TaskProperty =
           DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));


        private void AddOrUpdateTaskDetails(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button)!.Content.ToString()!;
            //this.ShowDialog();
            try
            {
                if (content == "Add")
                {

                    s_bl.Task.Create(CurrentTask);
                    MessageBox.Show("Done", "Task successfully added", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    s_bl.Task.Update(CurrentTask);
                    MessageBox.Show("Done", "The change has been made", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            this.Close();

        }
    }
}

