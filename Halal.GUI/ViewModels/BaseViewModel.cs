namespace Halal.GUI.ViewModels
{
    using System;
    using System.Threading;
    using Halal.Algorithms;
    using Halal.Problems;
    using OxyPlot;

    /// <summary>
    /// Defines a view model for plotting an algorithm.
    /// </summary>
    /// <typeparam name="TProblemElement">Problem type to be solved.</typeparam>
    /// <typeparam name="TSolutionElement">Solution type for the problem.</typeparam>
    public abstract class BaseViewModel<TProblemElement, TSolutionElement>
        where TProblemElement : IProblemElement
        where TSolutionElement : ISolutionElement
    {
        /// <summary>
        /// Gets a value indicating whether algorithm is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets PlotModel.
        /// </summary>
        public PlotModel PlotModel { get; private set; } = new PlotModel();

        /// <summary>
        /// Gets or sets Algorithm.
        /// </summary>
        public Algorithm<TProblemElement, TSolutionElement> Algorithm { get; set; }

        /// <summary>
        /// Starts plotting and runs algorithm.
        /// </summary>
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

        /// <summary>
        /// Stops plotting and algorithm.
        /// </summary>
        public void Stop()
        {
            this.IsRunning = false;
        }

        /// <summary>
        /// Draws the PlotModel.
        /// </summary>
        public void Draw()
        {
            if (this.IsRunning)
            {
                this.Plot();
                this.PlotModel.InvalidatePlot(true);
            }
        }

        /// <summary>
        /// Regenerate algorithm so starting position can change.
        /// </summary>
        public void Regenerate()
        {
            if (this.Algorithm != null)
            {
                this.Algorithm = (Algorithm<TProblemElement, TSolutionElement>)Activator.CreateInstance(this.Algorithm.GetType(), new[] { this.Algorithm.Problem });
                this.Plot();
                this.PlotModel.InvalidatePlot(true);
            }
        }

        /// <summary>
        /// Setup PlotModel.
        /// </summary>
        public virtual void Setup()
        {
            this.Plot();
            this.PlotModel.InvalidatePlot(true);
        }

        /// <summary>
        /// Plots algorithm on PlotModel.
        /// </summary>
        protected abstract void Plot();
    }
}