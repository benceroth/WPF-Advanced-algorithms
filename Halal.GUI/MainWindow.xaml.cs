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
        private const int Period = 200;
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
            var problem = new ImportFunctionApproximation().FromFile("FA.txt");
            this.FAV1.ViewModel.Algorithm = new HillClimbingStochasticFA(problem);
            this.FAV2.ViewModel.Algorithm = new HillClimbingStochasticFA(problem);
            this.FAV3.ViewModel.Algorithm = new HillClimbingStochasticFA(problem);
            this.FAV4.ViewModel.Algorithm = new HillClimbingStochasticFA(problem);
            this.FAV1.ViewModel.Setup();
            this.FAV2.ViewModel.Setup();
            this.FAV3.ViewModel.Setup();
            this.FAV4.ViewModel.Setup();
            this.FAV1.ViewModel.Start();
            this.FAV2.ViewModel.Start();
            this.FAV3.ViewModel.Start();
            this.FAV4.ViewModel.Start();
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
                this.FAV3.ViewModel.Draw();
                this.FAV4.ViewModel.Draw();
            }));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
            this.FAV1.ViewModel.Stop();
            this.FAV2.ViewModel.Stop();
            this.FAV3.ViewModel.Stop();
            this.FAV4.ViewModel.Stop();
        }
    }
}
