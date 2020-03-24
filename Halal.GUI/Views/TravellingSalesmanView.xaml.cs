namespace Halal.GUI.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using Halal.GUI.ViewModels;

    /// <summary>
    /// Interaction logic for FunctionApproximationView.xaml.
    /// </summary>
    public partial class TravellingSalesmanView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TravellingSalesmanView"/> class.
        /// </summary>
        public TravellingSalesmanView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets ViewModel.
        /// </summary>
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
