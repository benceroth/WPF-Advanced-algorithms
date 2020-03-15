using Halal.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Halal.GUI.Views
{
    /// <summary>
    /// Interaction logic for FunctionApproximationView.xaml
    /// </summary>
    public partial class FunctionApproximationView : UserControl
    {
        public FunctionApproximationView()
        {
            InitializeComponent();
        }

        public FunctionApproximationViewModel ViewModel => this.DataContext as FunctionApproximationViewModel;

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Start();
            this.StartButton.Visibility = Visibility.Hidden;
            this.StopButton.Visibility = Visibility.Visible;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Stop();
            this.StartButton.Visibility = Visibility.Visible;
            this.StopButton.Visibility = Visibility.Hidden;
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Regenerate();
        }
    }
}
