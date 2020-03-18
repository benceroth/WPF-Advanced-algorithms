using Halal.Algorithms;
using Halal.IO;
using Halal.Problems;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Halal.GUI.ViewModels
{
    public abstract class BaseViewModel<TProblemElement, TSolutionElement>
        where TProblemElement : IProblemElement
        where TSolutionElement : ISolutionElement
    {
        public void Draw()
        {
            if (this.IsRunning)
            {
                this.Plot();
                this.PlotModel.InvalidatePlot(true);
            }
        }

        public Algorithm<TProblemElement, TSolutionElement> Algorithm { get; set; }

        public bool IsRunning { get; protected set; }

        public PlotModel PlotModel { get; private set; } = new PlotModel();

        public virtual void Setup()
        {
            this.Plot();
            this.PlotModel.InvalidatePlot(true);
        }

        public void Start()
        {
            if (!this.IsRunning)
            {
                this.IsRunning = true;
                new Thread(() =>
                {
                    while (IsRunning)
                    {
                        this.Algorithm.DoOneIteration();
                    }
                })
                {
                    IsBackground = false,
                    Priority = ThreadPriority.BelowNormal,
                }.Start();
            }
        }

        public void Stop()
        {
            this.IsRunning = false;
        }

        public void Regenerate()
        {
            if (this.Algorithm != null)
            {
                this.Algorithm = (Algorithm<TProblemElement, TSolutionElement>)Activator.CreateInstance(this.Algorithm.GetType(), new[] { this.Algorithm.Problem });
                this.Plot();
                this.PlotModel.InvalidatePlot(true);
            }
        }

        protected abstract void Plot();
    }
}