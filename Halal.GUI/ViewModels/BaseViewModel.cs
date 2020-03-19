namespace Halal.GUI.ViewModels
{
    using System;
    using System.Threading;
    using Halal.Algorithms;
    using Halal.Problems;
    using OxyPlot;

    public abstract class BaseViewModel<TProblemElement, TSolutionElement>
        where TProblemElement : IProblemElement
        where TSolutionElement : ISolutionElement
    {
        public bool IsRunning { get; private set; }

        public PlotModel PlotModel { get; private set; } = new PlotModel();

        public Algorithm<TProblemElement, TSolutionElement> Algorithm { get; set; }

        public void Start()
        {
            if (!this.IsRunning)
            {
                this.IsRunning = true;
                new Thread(() =>
                {
                    while (this.IsRunning)
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

        public void Draw()
        {
            if (this.IsRunning)
            {
                this.Plot();
                this.PlotModel.InvalidatePlot(true);
            }
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

        public virtual void Setup()
        {
            this.Plot();
            this.PlotModel.InvalidatePlot(true);
        }

        protected abstract void Plot();
    }
}