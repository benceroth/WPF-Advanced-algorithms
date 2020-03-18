using Halal.Problems.TravellingSalesman;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halal.GUI.ViewModels
{
    public sealed class TravellingSalesmanViewModel : BaseViewModel<Town, Town>
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
            series.Points.AddRange(this.Algorithm.Solutions.First().Select(element => new DataPoint(element.First(), element.Last())));
            this.PlotModel.Title = this.Algorithm.Name + ":\r\n" + Math.Round(this.Algorithm.Solutions.First().CalculateFitness(), 4).ToString();
        }
    }
}
