namespace Halal.GUI.ViewModels
{
    using System;
    using System.Linq;
    using Halal.Problems.FunctionApproximation;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    public sealed class FunctionApproximationViewModel : BaseViewModel<Value, Coefficient>
    {
        public override void Setup()
        {
            this.PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            this.PlotModel.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });
            this.PlotModel.Series.Add(new LineSeries { LineStyle = LineStyle.None, MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(255, 0, 0), MarkerSize = 5 });
            ((LineSeries)this.PlotModel.Series.Last()).Points.AddRange(this.Algorithm.Problem.Select(element => new DataPoint(element.First(), element.Last())));
            base.Setup();
        }

        protected override void Plot()
        {
            var series = (LineSeries)this.PlotModel.Series.First();
            series.Points.Clear();
            series.Points.AddRange(this.Algorithm.Problem.Select(element => new DataPoint(element.First(), this.CalculateY(element.First()))));
            this.PlotModel.Title = this.Algorithm.Name + ":\r\n" + Math.Round(this.Algorithm.Solutions.First().CalculateFitness(), 4).ToString();
        }

        private double CalculateY(double x)
        {
            double[] coefficients = this.Algorithm.Solutions.First().Select(y => y.First()).ToArray();
            return (coefficients[0] * Math.Pow(x - coefficients[1], 3)) + (coefficients[2] * Math.Pow(x - coefficients[3], 2)) + coefficients[3];
        }
    }
}
