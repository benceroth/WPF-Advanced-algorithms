namespace Halal.GUI
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Halal.Algorithms;
    using Halal.IO;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Period = 150;
        private const int DueTime = 1000;
        private readonly Timer timer;
        private bool initialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.timer = new Timer(this.OnTimerElapsed);
            this.timer.Change(DueTime, Period);
        }

        /// <inheritdoc/>
        protected override void OnClosing(CancelEventArgs e)
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
            this.FAV1.ViewModel.Stop();
            this.FAV2.ViewModel.Stop();
            this.TS1.ViewModel.Stop();
            this.TS2.ViewModel.Stop();
            this.WA1.ViewModel.Stop();
            this.WA2.ViewModel.Stop();
        }

        private void Initialize()
        {
            var faProblem = new ImportFunctionApproximation().FromFile("FA.txt");
            var tsProblem = new ImportTravellingSalesman().FromFile("TS.txt");
            var waProblem = new ImportWorkAssignment().FromFile("WA.txt");
            this.FAV1.ViewModel.Algorithm = new GeneticAlgorithmFA(faProblem);
            this.FAV2.ViewModel.Algorithm = new HillClimbingStochasticFA(faProblem);
            this.TS1.ViewModel.Algorithm = new RandomOptimizationTS(tsProblem);
            this.TS2.ViewModel.Algorithm = new HillClimbingStochasticTS(tsProblem);
            this.WA1.ViewModel.Algorithm = new SimulatedAnnealingWA(waProblem);
            this.WA2.ViewModel.Algorithm = new HillClimbingStochasticWA(waProblem);
            this.FAV1.ViewModel.Setup();
            this.FAV2.ViewModel.Setup();
            this.TS1.ViewModel.Setup();
            this.TS2.ViewModel.Setup();
            this.WA1.ViewModel.Setup();
            this.WA2.ViewModel.Setup();
        }

        private async void OnTimerElapsed(object state)
        {
            await this.Dispatcher.BeginInvoke((Action)(() =>
            {
                if (!this.initialized)
                {
                    this.initialized = true;
                    this.Initialize();
                }

                this.FAV1.ViewModel.Draw();
                this.FAV2.ViewModel.Draw();
                this.TS1.ViewModel.Draw();
                this.TS2.ViewModel.Draw();
                this.WA1.ViewModel.Draw();
                this.WA2.ViewModel.Draw();
            }));
        }
    }
}
