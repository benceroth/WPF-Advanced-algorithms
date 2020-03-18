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
    public partial class TravellingSalesmanView : UserControl
    {
        public TravellingSalesmanView()
        {
            InitializeComponent();
        }

        public TravellingSalesmanViewModel ViewModel => this.DataContext as TravellingSalesmanViewModel;

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

        private void Regenerate_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Regenerate();
        }
    }
}
