namespace Halal.GUI.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Halal.Problems.FunctionApproximation;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    /// <inheritdoc/>
    public sealed class FunctionApproximationViewModel : BaseViewModel<Value, Coefficient>
    {
        private const double MarkerSize = 5;
        private const int FractionalDigits = 4;
        private static readonly MarkerType MarkerType = MarkerType.Circle;
        private static readonly OxyColor MarkerFill = OxyColor.FromRgb(255, 0, 0);

        /// <inheritdoc/>
        public override void Setup()
        {
            this.PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            this.PlotModel.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });
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

        private double CalculateY(double x)
        {
            double[] coefficients = this.Algorithm.Solutions.First().Select(y => y.First()).ToArray();
            return (coefficients[0] * Math.Pow(x - coefficients[1], 3)) + (coefficients[2] * Math.Pow(x - coefficients[3], 2)) + coefficients[4];
        }

        private string GetTitle() => $"{this.Algorithm.Name}:{Environment.NewLine}{this.GetRoundedFitness()}";

        private double GetRoundedFitness() => Math.Round(this.Algorithm.Solution.CalculateFitness(), FractionalDigits);

        private IEnumerable<DataPoint> GetProblemDataPoints() => this.Algorithm.Problem.Select(element => new DataPoint(element.First(), element.Last()));

        private IEnumerable<DataPoint> GetSolutionDataPoints() => this.Algorithm.Problem.Select(element => new DataPoint(element.First(), this.CalculateY(element.First())));
    }
}
