namespace Halal.GUI.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using Halal.GUI.ViewModels;

    /// <summary>
    /// Interaction logic for FunctionApproximationView.xaml.
    /// </summary>
    public partial class FunctionApproximationView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionApproximationView"/> class.
        /// </summary>
        public FunctionApproximationView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets ViewModel.
        /// </summary>
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

        private void Regenerate_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.Regenerate();
        }
    }
}
