using Halal.Algorithms;
using Halal.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Halal.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Period = 100;
        private const int DueTime = 1000;
        private readonly Timer timer;
        private bool initialized;
        public MainWindow()
        {
            InitializeComponent();
            this.timer = new Timer(this.OnTimerElapsed);
            this.timer.Change(DueTime, Period);
        }

        private void Initialize()
        {
            var faProblem = new ImportFunctionApproximation().FromFile("FA.txt");
            var tsProblem = new ImportTravellingSalesman().FromFile("TS.txt");
            var waProblem = new ImportWorkAssignment().FromFile("WA.txt");
            this.FAV1.ViewModel.Algorithm = new SimulatedAnnealingFA(faProblem);
            this.FAV2.ViewModel.Algorithm = new HillClimbingStochasticFA(faProblem);
            this.TS1.ViewModel.Algorithm = new SimulatedAnnealingTS(tsProblem);
            this.TS2.ViewModel.Algorithm = new HillClimbingStochasticTS(tsProblem);
            this.WA1.ViewModel.Algorithm = new HillClimbingStochasticWA(waProblem);
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
            await Dispatcher.BeginInvoke((Action)(() =>
            {
                if (!initialized)
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
    }
}
