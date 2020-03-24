namespace Halal.GUI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Halal.Problems.TravellingSalesman;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    /// <inheritdoc/>
    public sealed class TravellingSalesmanViewModel : BaseViewModel<Town, Town>
    {
        private const double MarkerSize = 3;
        private const int FractionalDigits = 4;
        private static readonly MarkerType MarkerType = MarkerType.Circle;
        private static readonly OxyColor Color = OxyColor.FromArgb(120, 0, 50, 200);
        private static readonly OxyColor MarkerFill = OxyColor.FromRgb(255, 0, 0);

        /// <inheritdoc/>
        public override void Setup()
        {
            this.PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            this.PlotModel.Series.Add(new LineSeries { LineStyle = LineStyle.Solid, Color = Color });
            this.PlotModel.Series.Add(new LineSeries { LineStyle = LineStyle.None, MarkerType = MarkerType, MarkerFill = MarkerFill, MarkerSize = MarkerSize });
            ((LineSeries)this.PlotModel.Series.Last()).Points.AddRange(this.GetProblemDataPoints());
            base.Setup();
        }

        /// <inheritdoc/>
        protected override void Plot()
        {
            this.PlotModel.Title = this.GetTitle();
            var series = (LineSeries)this.PlotModel.Series.First();
            series.Points.Clear();
            series.Points.AddRange(this.GetSolutionDataPoints());
        }

        private string GetTitle() => $"{this.Algorithm.Name}:{Environment.NewLine}{this.GetRoundedFitness()}";

        private double GetRoundedFitness() => Math.Round(this.Algorithm.Solution.CalculateFitness(), FractionalDigits);

        private IEnumerable<DataPoint> GetProblemDataPoints() => this.Algorithm.Problem.Select(element => new DataPoint(element.First(), element.Last()));

        private IEnumerable<DataPoint> GetSolutionDataPoints() => this.Algorithm.Solution.Select(element => new DataPoint(element.First(), element.Last()));
    }
}
