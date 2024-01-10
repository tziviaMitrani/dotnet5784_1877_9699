﻿using System;
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
            var engineer = s_bl?.Engineer.ReadAll();
            EngineerList = engineer == null ? new() : new(engineer);

        }

        public ObservableCollection<BO.Engineer> EngineerList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.All;

        private void ComboBox_SelectionByEngineerExperience(object sender, SelectionChangedEventArgs e)
        {
            var temp = Experience == BO.EngineerExperience.All ?
            s_bl?.Engineer.ReadAll() :
            s_bl?.Engineer.ReadAll(item => (int)item!.Level == (int)Experience);
            EngineerList = temp == null ? new() : new(temp);

        }

    }
}
